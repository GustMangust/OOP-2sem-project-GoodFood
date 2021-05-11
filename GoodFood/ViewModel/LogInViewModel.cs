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
    class LogInViewModel:ViewModelBase
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
        private Visibility visibility  = Visibility.Hidden;
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
        private void Close() 
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        public ICommand<object> signIn => new DelegateCommand<object>(SignIn);
        private void SignIn(object param) 
        {
             var passwordBox = param as PasswordBox;
             Password = passwordBox.Password;
             if (Validation.IsEmailValid(Email) && Validation.IsPasswordValid(Password)) 
             {
                BindingList<User> users = DB.GetUsers();
                
                    foreach (var user in users)
                    {
                        if (Email == user.Email && Encryption.Encrypt(Password) == user.Password)
                        {

                                MainWindow win = new MainWindow();
                                win.DataContext = new MainViewModel(user);
                                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                win.Show();
                                Close();
                        }
                    }
                
             }
             Visibility = Visibility.Visible;
        }
        public ICommand openSignUpPage => new DelegateCommand(OpenSignUpPage);
        private void OpenSignUpPage() 
        {
            SignUpPage win = new SignUpPage();
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.Show();
            Close();
        }
    }
}
