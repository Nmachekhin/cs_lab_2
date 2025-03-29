using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Controls;

namespace MachekhinZodiak
{
    internal class ZodiakCalculator : INotifyPropertyChanged
    {
        private DateTime _date;
        private int _age;
        private bool _dateValid = false;
        private bool _isBirthdayToday = false;
        private ZodiakSign _currentSign=null;
        private string _chineeseCurrentSign = string.Empty;
        private static ZodiakSign[] s_zodiakSignsTimespan = {
            new ZodiakSign(new DateTime(4, 3, 21), new DateTime(4, 4, 19), "Aries"),
            new ZodiakSign(new DateTime(4, 4, 20), new DateTime(4, 5, 20), "Taurus"),
            new ZodiakSign(new DateTime(4, 5, 21), new DateTime(4, 6, 20), "Gemini"),
            new ZodiakSign(new DateTime(4, 6, 21), new DateTime(4, 7, 22), "Cancer"),
            new ZodiakSign(new DateTime(4, 7, 23), new DateTime(4, 8, 22), "Leo"),
            new ZodiakSign(new DateTime(4, 8, 23), new DateTime(4, 9, 22), "Virgo"),
            new ZodiakSign(new DateTime(4, 9, 23), new DateTime(4, 10, 22), "Libra"),
            new ZodiakSign(new DateTime(4, 10, 23), new DateTime(4, 11, 21), "Scorpio"),
            new ZodiakSign(new DateTime(4, 11, 22), new DateTime(4, 12, 21), "Sagittarius"),
            new ZodiakSign(new DateTime(4, 12, 22), new DateTime(5, 1, 19), "Capricorn"),
            new ZodiakSign(new DateTime(4, 1, 20), new DateTime(4, 2, 18), "Aquarius"),
            new ZodiakSign(new DateTime(4, 2, 19), new DateTime(4, 3, 20), "Pisces")
        };


        public ZodiakCalculator()
        {
            Date = DateTime.Today;
        }

        public int Age
        {
            get => _age;
        }

        private void CalculateAge()
        {
            DateTime today = DateTime.Today;
            _age = (DateTime.Today.Year - Date.Year);
            if (DateTime.Today.Month < Date.Month) _age--;
            if (DateTime.Today.Month == Date.Month && DateTime.Today.Day > Date.Day && _age>0) _age--;
            OnPropertyChanged(nameof(Age));
        }

        public bool IsBirthdayToday
        {
            get { return _isBirthdayToday; }
        }

        public bool IsDateValid
        {
            get => _dateValid;
        }

        public string CurrentZodiakSign
        {
            get
            {
                if (_currentSign == null)
                    return "";
                return _currentSign.SignName;
            }
        }

        public string CurrentChineeseZodiakSign
        {
            get
            {
                string signCopy = new String(_chineeseCurrentSign);
                return signCopy;
            }
        }

        private void CalculateZodiakSign()
        {
            DateTime yearlessDate;
            if (_date.Month==1 && _date.Day<=19) yearlessDate = new DateTime(5, _date.Month, _date.Day);
            else yearlessDate = new DateTime(4, _date.Month, _date.Day);
            for(int i =0; i<12; i++)
            {
                if (s_zodiakSignsTimespan[i].CheckBirthDate(yearlessDate))
                    _currentSign = s_zodiakSignsTimespan[i];
            }
            OnPropertyChanged(nameof(CurrentZodiakSign));
        }

        private void CalculateChineeseZodiakSign()
        {
            _chineeseCurrentSign = ChineeseZodiakSign.GetSign(_date);
            OnPropertyChanged(nameof(CurrentChineeseZodiakSign));
        }

        public DateTime Date
        {
            get { return _date; }
            set 
            {
                if (DateValidator(value))
                {
                    _date = value;
                    _dateValid = true;
                    _isBirthdayToday = _date.Day == DateTime.Today.Day && _date.Month == DateTime.Today.Month;
                    CalculateAge();
                    CalculateZodiakSign();
                    CalculateChineeseZodiakSign();
                }
                else
                {
                    _date = DateTime.Today;
                    _dateValid = false;
                    _isBirthdayToday = false;
                    _age = 0;
                    _currentSign = null;
                    _chineeseCurrentSign = "";
                }
                OnPropertyChanged(nameof(IsDateValid));
                OnPropertyChanged(nameof(IsBirthdayToday));
                OnPropertyChanged(nameof(Date));
                

            }
        }


        private bool DateValidator(DateTime date)
        {
            DateTime today = DateTime.Today;
            return today >= date && (today.Year - date.Year) <= 135;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
