﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MachekhinZodiak;

namespace MachekhinPerson
{
    internal class Person : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        private ZodiakCalculator _calculator;

        public Person(string name, string surname, string email, DateTime birthDate) 
        {
            _name = new string(name);
            _surname = new string(surname);
            _email = new string(email);
            _birthDate = birthDate;
            _calculator = new ZodiakCalculator();
            _calculator.PropertyChanged += OnZodiakCalculateorPropertyChange;
            _calculator.Date=birthDate;
        }

        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.Today) { }

        public Person(string name, string surname, DateTime birthDate) : this(name, surname, string.Empty, birthDate) { }

        ~Person() { Trace.WriteLine("person is destroyed 1984 ___________________"); }

        public int Age
        {
            get { return _calculator.Age; }
        }

        public bool IsAdult
        {
            get { return _calculator.Age>=18; }
        }

        public string SunSign
        {
            get { return _calculator.CurrentZodiakSign; }
        }

        public string ChineeseSign
        {
            get { return _calculator.CurrentChineeseZodiakSign; }
        }

        public bool IsBirthday
        {
            get { return _calculator.IsBirthdayToday; }
        }

        public DateTime BirthDate
        {
            get { return _calculator.Date; }
            set { _calculator.Date = value; }
        }

        public bool IsValid
        {
            get { return _calculator.IsDateValid; }
        }

        private void OnZodiakCalculateorPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case (nameof(ZodiakCalculator.Date)):
                    _birthDate = _calculator.Date;
                    OnPropertyChanged(nameof(this.BirthDate));
                    break;
                case (nameof(ZodiakCalculator.IsBirthdayToday)):
                    OnPropertyChanged(nameof(this.IsBirthday));
                    break;
                case (nameof(ZodiakCalculator.Age)):
                    OnPropertyChanged(nameof(this.Age));
                    break;
                case (nameof(ZodiakCalculator.IsDateValid)):
                    OnPropertyChanged(nameof(this.IsValid));
                    break;
                case (nameof(ZodiakCalculator.CurrentZodiakSign)):
                    OnPropertyChanged(nameof(this.SunSign));
                    break;
                case (nameof(ZodiakCalculator.CurrentChineeseZodiakSign)):
                    OnPropertyChanged(nameof(this.ChineeseSign));
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
