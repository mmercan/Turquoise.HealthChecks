using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Logging;
using Turquoise.Scheduler.HealthCheckScheduler;
using Turquoise.Scheduler.HealthCheckScheduler.Cron;
using Turquoise.Scheduler.Helpers;

namespace Turquoise.Scheduler.Services
{

    public class HealthCheckRepo
    {

        public ObservableCollection<HealthCheckServiceItem> Items { get; set; }
        public List<HealthCheckSchedulerTaskWrapper> ScheduledTasks { get; }

        private ILogger<HealthCheckRepo> logger;

        public HealthCheckRepo(ILogger<HealthCheckRepo> logger)
        {
            this.logger = logger;
            Items = new ObservableCollection<HealthCheckServiceItem>();
            ScheduledTasks = new List<HealthCheckSchedulerTaskWrapper>();

            this.logger = logger;
            foreach (HealthCheckServiceItem item in Items)
            {
                addItem(item);
            }
            Items.CollectionChanged += new NotifyCollectionChangedEventHandler(collectionChanged);
            var staticitems = testCronData.GetData();
            foreach (var item in staticitems)
            {
                Items.Add(item);
            }
        }



        private void collectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (HealthCheckServiceItem x in e.NewItems) { addItem(x); };
            };
            if (e.OldItems != null)
            {
                foreach (HealthCheckServiceItem y in e.OldItems) { deleteItem(y); }
            }
            if (e.Action == NotifyCollectionChangedAction.Move) { }
        }
        private void addItem(HealthCheckServiceItem item)
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

        private void editItem(HealthCheckServiceItem item)
        {

        }

        private void deleteItem(HealthCheckServiceItem item)
        {
            var itemtodelete = ScheduledTasks.FirstOrDefault(e => e.Uid == item.Uid);
            if (itemtodelete != null)
            {
                ScheduledTasks.Remove(itemtodelete);
            }
        }

    }


}