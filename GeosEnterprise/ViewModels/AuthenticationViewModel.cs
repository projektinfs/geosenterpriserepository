﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using System.Windows;
using System.Windows.Input;
using GeosEnterprise.Commands;
using System.ComponentModel;
using System.Windows.Controls;

namespace GeosEnterprise.ViewModels
{
    public class AuthenticationViewModel : INotifyPropertyChanged
    {

        public ICommand SignInCommand { get; private set; }

        private bool _IsAuthenticated;
        public bool IsAuthenticated
        {
            get { return _IsAuthenticated; }
            set
            {
                _IsAuthenticated = value;
                NotifyPropertyChanged("IsAuthenticated");
            }
        }

        private string _MessageForUser;
        public string MessageForUser
        {
            get { return _MessageForUser; }
            set
            {
                _MessageForUser = value;
                NotifyPropertyChanged("MessageForUser");
            }
        }

        private string _IsVisible;
        public string IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                NotifyPropertyChanged("IsVisible");
            }
        }
        private static bool _IsAdmin;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                _IsAdmin = value;
                NotifyPropertyChanged("IsAdmin");
            }
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticationViewModel()
        {
            if (EmployeeRepository.GetByEmail("admin@admin.pl") == null)
            {

                Employee employee = new Employee
                {
                    Email = "admin@admin.pl",
                    Password = "admin123",
                    Position = "Administrator",
                    Name = "admin",
                    Surname = "admin",
                    Adress = new Adress
                    {
                        City = "admin",
                        Voivodeship = "admin",
                        District = "admin",
                        PostCode = "admin",
                        Street = "admin",
                        BuildingNumber = "admin",
                        AppartamentNumber = "admin",
                    },
                    EmployeeContact = new EmployeeContact
                    {
                        Phone = "admin",
                        Fax = "admin",
                        Www = "admin",
                    }

                };

                EmployeeRepository.Add(employee);
            }

            IsVisible = "Visible";
            IsAuthenticated = false;
            SignInCommand = new SignInCommand(
                SignIn,
                (object parameters) => true
            );

        }

        private void SignIn(object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            Password = passwordBox.Password;

            if (System.Diagnostics.Debugger.IsAttached && Config.IgnoreAuthentication)
            {
                Authorization.AcctualEmployee = EmployeeRepository.GetByEmail("admin@admin.pl");
                Authorization.AcctualUser = "admin@admin.pl";
                IsAuthenticated = true;
                IsVisible = "Hidden";
                return;
            }

            if (EmployeeRepository.ValidateData(Email, Password) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                Authorization.AcctualUser = Email;
                Authorization.AcctualEmployee = EmployeeRepository.GetByEmail(Email);
                IsAuthenticated = true;
                if (Authorization.AcctualEmployee.Position == "Administrator") IsAdmin = true; else IsAdmin = false;
                MessageForUser = "";
                IsVisible = "Hidden";
            }
            else
            {
                MessageForUser = "Błędny login lub hasło !";
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
