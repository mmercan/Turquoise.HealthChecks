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
    public class HealthCheckSchedulerRepository<T> where T : new()
    {
        public ObservableCollection<IHealthCheckScheduledTask<T>> Items { get => items; set => items = value; }
        public List<HealthCheckSchedulerTaskWrapper<T>> ScheduledTasks { get; }

        private ILogger<HealthCheckSchedulerRepository<T>> logger;
        private ObservableCollection<IHealthCheckScheduledTask<T>> items;

        public HealthCheckSchedulerRepository(ILogger<HealthCheckSchedulerRepository<T>> logger)
        {
            this.logger = logger;
            Items = new ObservableCollection<IHealthCheckScheduledTask<T>>();
            ScheduledTasks = new List<HealthCheckSchedulerTaskWrapper<T>>();

            this.logger = logger;
            foreach (IHealthCheckScheduledTask<T> item in Items)
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
                foreach (IHealthCheckScheduledTask<T> x in e.NewItems) { addItem(x); };
            };
            if (e.OldItems != null)
            {
                foreach (IHealthCheckScheduledTask<T> y in e.OldItems) { deleteItem(y); }
            }
            if (e.Action == NotifyCollectionChangedAction.Move) { }
        }

        private void addItem(IHealthCheckScheduledTask<T> item)
        {
            var referenceTime = DateTime.UtcNow;
            //   logger.LogCritical("scheduledTask Added " + item.Name);

            var scheduledTask = new HealthCheckSchedulerTaskWrapper<T>
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

        private void editItem(IHealthCheckScheduledTask<T> item)
        {

        }

        private void deleteItem(IHealthCheckScheduledTask<T> item)
        {
            var itemtodelete = ScheduledTasks.FirstOrDefault(e => e.Uid == item.Uid);
            if (itemtodelete != null)
            {
                ScheduledTasks.Remove(itemtodelete);
            }
        }

    }
}