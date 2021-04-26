using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GoodFood.ViewModel
{
    class MainViewModel:ViewModelBase
    {
		private ViewModelBase selectedViewModel = new AddRestaurantViewModel();

		public ViewModelBase SelectedViewModel
		{
			get 
			{
				return selectedViewModel;
			}
			set 
			{
				selectedViewModel = value;
				RaisePropertyChanged("SelectedViewModel");
			}
		}
		public ICommand<object> updateViewModel => new DelegateCommand<object>(UpdateViewModel);
		private void UpdateViewModel(object param) 
		{
			if (param.ToString() == "AddRestaurant")
				SelectedViewModel = new AddRestaurantViewModel();
			else if (param.ToString() == "AllRestaurants")
				SelectedViewModel = new AllRestaurantsViewModel();
		}
	}
	
}
