using GoodFood.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoodFood.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class SignUpPage : Window
    {
        public SignUpPage()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}
