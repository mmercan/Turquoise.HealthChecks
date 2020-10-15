using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using k8s.Models;
using Turquoise.GRPC.GRPCServices;
using Turquoise.Models.Mongo;

namespace Turquoise.GRPC.Converters
{
    public class DeploymentListReplyConverter
    {
        public static DeploymentListReply ConvertToDeploymentListReply(IList<V1Deployment> deployments)
        {
            //mapper.Map<DeploymentListReply>(deployments);

            DeploymentListReply deploy = new DeploymentListReply();

            foreach (var item in deployments)
            {
                var dep = ConvertToDeploymentReply(item);
                deploy.Deployments.Add(dep);
            }
            deploy.UpdatedTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
            return deploy;

        }

        public static DeploymentReply ConvertToDeploymentReply(V1Deployment item)
        {
                var dep = new DeploymentReply();
                dep.Name = item.Metadata.Name;
                dep.Namespace = item.Metadata.Namespace();
                // dep.Image = item.Spec.Template.Spec.Containers.FirstOrDefault().Image;
                dep.Status = ConvertV1DeploymentStatus(item.Status);
                dep.Spec = ConvertV1DeploymentSpec(item);
                dep.Labels.AddRange(item.Metadata.Labels.Select(lab => { return new Pair { Key = lab.Key, Value = lab.Value }; }));
                dep.Annotations.AddRange(item.Metadata.Annotations.Select(lab => { return new Pair { Key = lab.Key, Value = lab.Value }; }));
                if (item.CreationTimestamp().HasValue)
                {
                    dep.CreationTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.CreationTimestamp().Value);
                }

                var downscalecrontab = item.Metadata.Annotations.FirstOrDefault(p => p.Key =="taka/downscale-crontab");
                var upscalecrontab = item.Metadata.Annotations.FirstOrDefault(p => p.Key =="taka/upscale-crontab");
                if(downscalecrontab.Value != null){
                    var Schedule = CronExpressionDescriptor.ExpressionDescriptor.GetDescription(downscalecrontab.Value);
                    dep.CronDescriptionScaleDown = Schedule;
                    dep.DownscaleCrontab = downscalecrontab.Value;
                }

                if(upscalecrontab.Value != null){
                    var Schedule = CronExpressionDescriptor.ExpressionDescriptor.GetDescription(upscalecrontab.Value);
                    dep.CronDescriptionScaleUp = Schedule;
                    dep.UpscaleCrontab = upscalecrontab.Value;
                }


                var upscaleReplica = item.Metadata.Annotations.FirstOrDefault(p => p.Key =="taka/upscale-replica");
                if(upscaleReplica.Value!=null){
                    dep.UpscaleReplica =upscaleReplica.Value;
                }
                var downscaleReplica = item.Metadata.Annotations.FirstOrDefault(p => p.Key =="taka/downscale-replica");
                if(downscaleReplica.Value!=null)
                {
                    dep.DownscaleReplica = downscaleReplica.Value;
                }
                var timezone =item.Metadata.Annotations.FirstOrDefault(p => p.Key =="taka/scale-timezone");
                if(timezone.Value!=null)
                {
                  dep.CrontabTimezone =  timezone.Value;
                }
                
               return dep;
        }

        public static DeploymentSpecReply ConvertV1DeploymentSpec(V1Deployment dep)
        {
            var reply = new DeploymentSpecReply();
            reply.ProgressDeadlineSeconds = dep.Spec.ProgressDeadlineSeconds;
            reply.Replicas = dep.Spec.Replicas;
            reply.RevisionHistoryLimit = dep.Spec.RevisionHistoryLimit;
            reply.SelectorString = string.Join(",", dep.Spec.Selector.MatchLabels.OrderBy(p => p.Key).Select(p => p.Key + "=" + p.Value).ToArray());
            reply.Image = dep.Spec.Template.Spec.Containers[0].Image;

            return reply;
        }
        public static DeploymentStatusReply ConvertV1DeploymentStatus(V1DeploymentStatus status)
        {
            DeploymentStatusReply reply = new DeploymentStatusReply();
            reply.AvailableReplicas = status.AvailableReplicas;
            reply.AvailableReplicas = status.AvailableReplicas;
            reply.CollisionCount = status.CollisionCount;
            reply.ReadyReplicas = status.ReadyReplicas;
            reply.Replicas = status.Replicas;
            reply.UnavailableReplicas = status.UnavailableReplicas;
            reply.UpdatedReplicas = status.UpdatedReplicas;

            var overallstat = status.Conditions.Any(p => p.Status == "False");
            if (overallstat) { reply.OverallStatus = "False"; }
            else { reply.OverallStatus = "True"; }
            reply.Condition.AddRange(ConvertV1DeploymentStatus(status.Conditions));
            return reply;

        }

        public static List<DeploymentStatusConditionReply> ConvertV1DeploymentStatus(IList<V1DeploymentCondition> conditions)
        {
            List<DeploymentStatusConditionReply> reply = new List<DeploymentStatusConditionReply>();

            foreach (var item in conditions)
            {
                var conditionReply = new DeploymentStatusConditionReply();
                reply.Add(conditionReply);
                if (item.LastTransitionTime.HasValue)
                {
                    conditionReply.LastTransitionTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.LastTransitionTime.Value);
                }
                if (item.LastUpdateTime.HasValue)
                {
                    conditionReply.LastUpdateTime = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.LastUpdateTime.Value);
                }

                conditionReply.Message = item.Message;
                conditionReply.Reason = item.Reason;
                conditionReply.Status = item.Status;
                conditionReply.Type = item.Type;
            }
            return reply;
        }
   
   
        public static DeploymentScaleHistoryListReply ConvertListDeploymentScaleHistory(List<DeploymentScaleHistory> histories)
        {
            DeploymentScaleHistoryListReply replies = new DeploymentScaleHistoryListReply();
            foreach (var item in histories)
            {
                DeploymentScaleHistoryReply reply = new DeploymentScaleHistoryReply();
                reply.Uid =item.Uid;
                reply.Name = item.Name;
                reply.Namespace = item.Namespace;
               
                if(!string.IsNullOrWhiteSpace(item.Schedule)){
                reply.Schedule = item.Schedule;
                }
                if(!string.IsNullOrWhiteSpace(item.Timezone)){
                    reply.Timezone = item.Timezone;
                }
                
                reply.ScaledUtc = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(item.ScaledUtc);
                reply.OldScaleNumber = item.OldScaleNumber;
                reply.NewScaleNumber = item.NewScaleNumber;
                
                if(!string.IsNullOrWhiteSpace(item.Status)){
                    reply.Status = item.Status;
                }
                replies.Histories.Add(reply);
            }
            return replies;
        }
    }
}