using GoodFood.ViewModel;
using System.Windows;
namespace GoodFood.View {
  public partial class LogInPage : Window {
    public LogInPage() {
      InitializeComponent();
      this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      DataContext = new LogInViewModel();
    }
  }
}
