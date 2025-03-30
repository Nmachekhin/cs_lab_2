using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MachekhinPerson;

namespace MachekhinZodiak
{
    internal class ViewModel
    {
        private Person _person;
        public event EventHandler ShowIncorrectDateMessage;
        public event EventHandler ClearAllCalculatedFields;
        public event EventHandler<int> AgeUpdate;
        public event EventHandler<string> DisplayBirthdayMessage;
        public event EventHandler<string> DisplayZodiakSign;
        public event EventHandler<string> DisplayChineeseZodiakSign;
        public event EventHandler<string> DisplayAdultText;
        public event EventHandler<string> DisplayNameText;
        public event EventHandler<string> DisplaySurnameText;
        public event EventHandler<string> DisplayEmailText;
        public event EventHandler RevealProperties;
        public event EventHandler<DateTime> DatePickerUpdate;
        public event EventHandler<bool> UpdateProceedButtonStatus;
        public event EventHandler<bool> UpdateInputsPanelStatus;
        private DateTime _selectedDate;

        public ViewModel()
        {
            _selectedDate = DateTime.Today;
        }


        public DateTime Date
        {
            get { return _selectedDate; }
            set { _selectedDate=value; }
        }

        public bool IsDateValid
        {
            get { if (_person == null) return false; else return _person.IsValid; }
        }



        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            switch (e.PropertyName)
            {

                case (nameof(Person.BirthDate)):
                    Date = _person.BirthDate;
                    break;
                case (nameof(Person.Email)):
                    DisplayEmailText.Invoke(this, _person.Email);
                    break;
                case (nameof(Person.Name)):
                    DisplayNameText.Invoke(this, _person.Name);
                    break;
                case (nameof(Person.Surname)):
                    DisplaySurnameText.Invoke(this, _person.Surname);
                    break;
                case (nameof(Person.IsBirthday)):
                    if (_person.IsBirthday) DisplayBirthdayMessage.Invoke(this, "🎉🎉🎉Happy birthday!🎉🎉🎉");
                    else DisplayBirthdayMessage.Invoke(this, String.Empty);
                    break;
                case (nameof(Person.Age)):
                    AgeUpdate.Invoke(this, _person.Age);
                    if (_person.IsAdult)
                    {
                        DisplayAdultText.Invoke(this, "Adult");
                    }
                    else DisplayAdultText.Invoke(this, "Not adult yet");
                    break;
                case (nameof(Person.IsValid)):

                    if (!_person.IsValid)
                    {
                        ClearAllCalculatedFields.Invoke(this, EventArgs.Empty);
                        ShowIncorrectDateMessage.Invoke(this, EventArgs.Empty);
                        DatePickerUpdate.Invoke(this, _selectedDate);
                        DismissPerson();
                    }
                    else
                    {
                        DisplayEmailText.Invoke(this, _person.Email);
                        DisplayNameText.Invoke(this, _person.Name);
                        DisplaySurnameText.Invoke(this, _person.Surname);
                        RevealProperties.Invoke(this, EventArgs.Empty);
                        _selectedDate = _person.BirthDate;
                    }
                    break;
                case (nameof(Person.SunSign)):
                    DisplayZodiakSign.Invoke(this, _person.SunSign);
                    break;
                case (nameof(Person.ChineeseSign)):
                    DisplayChineeseZodiakSign.Invoke(this, _person.ChineeseSign);
                    break;
            }
        }


        private void DismissPerson()
        {
            if (_person!=null)_person.PropertyChanged -= OnModelPropertyChanged;
            _person = null;
        }

        public async void ProseedButtonClick(string name, string surname, string email, DateTime birthDate)
        {
            UpdateInputsPanelStatus.Invoke(this, false);
            ClearAllCalculatedFields.Invoke(this, EventArgs.Empty);
            DismissPerson();
            _person=new Person(name, surname, email);
            _person.PropertyChanged += OnModelPropertyChanged;
            await _person.UpdateDate(birthDate);
            UpdateInputsPanelStatus.Invoke(this, true);
        }


        public void CheckInputsStatus(string name, string surname, string email, string date, bool btnStatus)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(surname) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(date))
            {
                if (!btnStatus) UpdateProceedButtonStatus.Invoke(this, true);
            }
            else if (btnStatus) UpdateProceedButtonStatus.Invoke(this, false);
        }
    }
}
