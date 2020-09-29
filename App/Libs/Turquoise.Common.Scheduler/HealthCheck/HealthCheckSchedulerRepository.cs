using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Logging;
using Turquoise.Models.Scheduler;
using Turquoise.Models.Scheduler.Cron;

namespace Turquoise.Common.Scheduler.HealthCheck
{
    public class HealthCheckSchedulerRepository<T> where T : new()
    {
        public ObservableCollection<IScheduledTask<T>> Items { get => items; set => items = value; }
        public List<SchedulerTaskWrapper<T>> ScheduledTasks { get; }

        private ILogger<HealthCheckSchedulerRepository<T>> logger;
        private ObservableCollection<IScheduledTask<T>> items;

        public HealthCheckSchedulerRepository(ILogger<HealthCheckSchedulerRepository<T>> logger)
        {
            this.logger = logger;
            Items = new ObservableCollection<IScheduledTask<T>>();
            ScheduledTasks = new List<SchedulerTaskWrapper<T>>();

            this.logger = logger;
            foreach (IScheduledTask<T> item in Items)
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
                foreach (IScheduledTask<T> x in e.NewItems) { addItem(x); };
            };
            if (e.OldItems != null)
            {
                foreach (IScheduledTask<T> y in e.OldItems) { deleteItem(y); }
            }
            if (e.Action == NotifyCollectionChangedAction.Move) { }
        }

        private void addItem(IScheduledTask<T> item)
        {
            var referenceTime = DateTime.UtcNow;
            //   logger.LogCritical("scheduledTask Added " + item.Name);

            var scheduledTask = new SchedulerTaskWrapper<T>
            {
                Uid = item.Uid,
                Schedule = CrontabSchedule.Parse(item.Schedule),
                Task = item,
                NextRunTime = referenceTime,
                Item = item.Item
            };

            ScheduledTasks.Add(scheduledTask);
            logger.LogCritical(scheduledTask.Task.Name + " : " + scheduledTask.Schedule.ToString() + " ===> " + scheduledTask.Schedule.GetNextOccurrence(referenceTime).ToString("MM/dd/yyyy H:mm"));
        }

        private void editItem(IScheduledTask<T> item)
        {

        }

        private void deleteItem(IScheduledTask<T> item)
        {
            var itemtodelete = ScheduledTasks.FirstOrDefault(e => e.Uid == item.Uid);
            if (itemtodelete != null)
            {
                ScheduledTasks.Remove(itemtodelete);
            }
        }

    }
}