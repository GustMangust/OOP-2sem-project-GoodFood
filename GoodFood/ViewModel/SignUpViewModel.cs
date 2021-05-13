using DevExpress.Mvvm;
using GoodFood.Model;
using GoodFood.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GoodFood.ViewModel
{
    class SignUpViewModel:ViewModelBase
    {
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertiesChanged("Login");
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertiesChanged("Password");
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertiesChanged("Name");
            }
        }
        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                RaisePropertiesChanged("Surname");
            }
        }
        private Visibility visibility = Visibility.Hidden;
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                RaisePropertiesChanged("Visibility");
            }
        }
        public ICommand back => new DelegateCommand(Back);
        private void Back()
        {
            LogInPage win = new LogInPage();
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.Show();
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand<object> signUp => new DelegateCommand<object>(SignUp);
        private void SignUp(object param) 
        {
            var passwordBox = param as PasswordBox;
            Password = passwordBox.Password;
            if (Validation.IsEmailValid(Email) && Validation.IsNameSurnameValid(Name) && Validation.IsNameSurnameValid(Surname) && Validation.IsPasswordValid(Password))
            {
                BindingList<User> users = DB.GetUsers();
                foreach (User user in users)
                {
                    if (user.Email == Email)
                    {
                        Visibility = Visibility.Visible;
                        return;
                    }
                }
                DB.AddUser(Email, Encryption.Encrypt(Password), Name, Surname);
                Back();
            }
            else Visibility = Visibility.Visible;
        }
    }
}
