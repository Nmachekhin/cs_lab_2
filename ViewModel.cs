using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MachekhinZodiak
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private ZodiakCalculator _datesToZodiakCalculator;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ShowIncorrectDateMessage;
        public event EventHandler ClearAllCalculatedFields;
        public event EventHandler<int> AgeUpdate;
        public event EventHandler<string> DisplayBirthdayMessage;
        public event EventHandler<string> DisplayZodiakSign;
        public event EventHandler<string> DisplayChineeseZodiakSign;
        private DateTime _selectedDate;

        public ViewModel()
        { 
            _datesToZodiakCalculator = new ZodiakCalculator();
            _datesToZodiakCalculator.PropertyChanged += OnModelPropertyChanged;
            Date = _datesToZodiakCalculator.Date;
        }


        public DateTime Date
        {
            get { return _selectedDate; }
            set { _selectedDate=value; }
        }

        public bool IsDateValid
        {
            get { return _datesToZodiakCalculator.IsDateValid; }
        }



        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case (nameof(ZodiakCalculator.Date)):
                    Date = _datesToZodiakCalculator.Date;
                    OnPropertyChanged(nameof(Date));
                    break;
                case (nameof(ZodiakCalculator.IsBirthdayToday)):
                    if (_datesToZodiakCalculator.IsBirthdayToday) DisplayBirthdayMessage.Invoke(this, "🎉🎉🎉Happy birthday!🎉🎉🎉");
                    else DisplayBirthdayMessage.Invoke(this, String.Empty);
                    break;
                case (nameof(ZodiakCalculator.Age)):
                    AgeUpdate.Invoke(this, _datesToZodiakCalculator.Age);
                    break;
                case (nameof(ZodiakCalculator.IsDateValid)):
                    if (!IsDateValid)
                    {
                        ClearAllCalculatedFields.Invoke(this, EventArgs.Empty);
                        ShowIncorrectDateMessage.Invoke(this, EventArgs.Empty);
                    }
                    break;
                case (nameof(ZodiakCalculator.CurrentZodiakSign)):
                    DisplayZodiakSign.Invoke(this, _datesToZodiakCalculator.CurrentZodiakSign);
                    break;
                case (nameof(ZodiakCalculator.CurrentChineeseZodiakSign)):
                    DisplayChineeseZodiakSign.Invoke(this, _datesToZodiakCalculator.CurrentChineeseZodiakSign);
                    break;
            }
        }

        public void ConfirmDateButtonClick(object sender, RoutedEventArgs e)
        {
            _datesToZodiakCalculator.Date = Date;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
