using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder
{
    public class MainContext : ViewModelBase
    {

        private ReminderItem _inputItem = new ReminderItem();
        public ReminderItem InputItem
        {
            get => _inputItem;
            set
            {
                _inputItem = value;
                OnPropertyChanged(nameof(InputItem));
            }
        }

        private ObservableCollection<ReminderItem> _reminderItems = new ObservableCollection<ReminderItem>();
        public ObservableCollection<ReminderItem> ReminderItems
        {
            get => _reminderItems;
            set
            {
                _reminderItems = value;
                OnPropertyChanged(nameof(ReminderItems));
            }
        }

        public void SubmitReminder()
        {
            ReminderItems.Add(InputItem);
            InputItem = new ReminderItem();
        }   
    }
}
