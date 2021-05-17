using GoodFood.ViewModel;
using System.Windows;

namespace GoodFood.View {
  /// <summary>
  /// Логика взаимодействия для RegistrationPage.xaml
  /// </summary>
  public partial class SignUpPage : Window {
    public SignUpPage() {
      InitializeComponent();
      DataContext = new SignUpViewModel();
    }
  }
}
