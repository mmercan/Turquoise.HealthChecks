using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Turquoise.Common.Scheduler;
using Turquoise.Common.Scheduler.Cron;


namespace Turquoise.Common.Scheduler
{
    public class HealthCheckSchedulerRepository
    {
        public ObservableCollection<IHealthCheckScheduledTask> Items { get => items; set => items = value; }
        public List<HealthCheckSchedulerTaskWrapper> ScheduledTasks { get; }

        private ILogger<HealthCheckSchedulerRepository> logger;
        private ObservableCollection<IHealthCheckScheduledTask> items;

        public HealthCheckSchedulerRepository(ILogger<HealthCheckSchedulerRepository> logger)
        {
            this.logger = logger;
            Items = new ObservableCollection<IHealthCheckScheduledTask>();
            ScheduledTasks = new List<HealthCheckSchedulerTaskWrapper>();

            this.logger = logger;
            foreach (IHealthCheckScheduledTask item in Items)
            {
                addItem(item);
            }
            Items.CollectionChanged += new NotifyCollectionChangedEventHandler(collectionChanged);
            // var staticitems = testCronData.GetData();
            // foreach (var item in staticitems)
            // {
            //     Items.Add(item);
            // }
        }


        private void collectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (IHealthCheckScheduledTask x in e.NewItems) { addItem(x); };
            };
            if (e.OldItems != null)
            {
                foreach (IHealthCheckScheduledTask y in e.OldItems) { deleteItem(y); }
            }
            if (e.Action == NotifyCollectionChangedAction.Move) { }
        }

        private void addItem(IHealthCheckScheduledTask item)
        {
            var referenceTime = DateTime.UtcNow;
            //   logger.LogCritical("scheduledTask Added " + item.Name);

            var scheduledTask = new HealthCheckSchedulerTaskWrapper
            {
                Uid = item.Uid,
                Schedule = CrontabSchedule.Parse(item.Schedule),
                Task = item,
                NextRunTime = referenceTime
            };

            ScheduledTasks.Add(scheduledTask);
            logger.LogCritical(scheduledTask.Task.Name + " : " + scheduledTask.Schedule.ToString() + " ===> " + scheduledTask.Schedule.GetNextOccurrence(referenceTime).ToString("MM/dd/yyyy H:mm"));
        }

        private void editItem(IHealthCheckScheduledTask item)
        {

        }

        private void deleteItem(IHealthCheckScheduledTask item)
        {
            var itemtodelete = ScheduledTasks.FirstOrDefault(e => e.Uid == item.Uid);
            if (itemtodelete != null)
            {
                ScheduledTasks.Remove(itemtodelete);
            }
        }

    }
}