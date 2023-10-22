using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager
{
    internal class Event
    {
        protected string description;
        protected DateTime startDate;
        protected DateTime endDate;
        protected TimeSpan notifierTime;
        protected bool isNotifierEnabled;

        public Event(string _description, DateTime _startDate, DateTime _endDate)
        {
            description = _description;
            startDate = _startDate;
            endDate = _endDate;
        }
        public Event(string _description, DateTime _startDate, DateTime _endDate, TimeSpan _notifierTime)
        {
            description = _description;
            startDate = _startDate;
            endDate = _endDate;
            notifierTime = _notifierTime;
            ActivateNotifier();
        }

        public string Description { get { return description; } set { description = value; } }
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        public TimeSpan NotifierTime { get { return notifierTime; } set { notifierTime = value; if (!isNotifierEnabled) ActivateNotifier(); } }
        public bool IsNotifierEnabled { get { return isNotifierEnabled; } } 

        public string InfoString {  
            get 
            {
                return ("\n----------------------------\n" +
                    "[" + this.Description + "]" +
                    "\nНачало:" + this.StartDate +
                    "\nОкончание" + this.EndDate +
                    "\n----------------------------");  
            } 
        }
        public void DeactivateNotifier()
        {
            isNotifierEnabled = false;
        }

        protected void ShowNotification()
        {
            Console.WriteLine("\n!!!   Напоминание   !!!\n");
            Console.WriteLine(this.InfoString);
        }
        
        protected void ActivateNotifier()
        {
            isNotifierEnabled = true;

            Thread notifier = new Thread(WaitForNotification);

            notifier.Start();
        }

        void WaitForNotification()
        {

            while (isNotifierEnabled)
            {
                Thread.Sleep(1000);
                
                if (DateTime.Now >= startDate - notifierTime)
                {
                    ShowNotification();
                    isNotifierEnabled = false;
                }
            }
        }
    }
}
