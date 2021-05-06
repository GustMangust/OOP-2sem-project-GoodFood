using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GoodFood.ViewModel
{
    class MainViewModel:ViewModelBase
    {
		private ViewModelBase selectedViewModel = new AddRestaurantViewModel();
		private bool is_admin;
		public bool Is_admin 
		{
			get 
			{
				return is_admin;
			}
			set 
			{
				is_admin = value;
				RaisePropertyChanged("Is_admin");
			}
		}
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
		private Visibility visibility;

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

		public ICommand<object> updateViewModel => new DelegateCommand<object>(UpdateViewModel);
		private void UpdateViewModel(object param) 
		{
			if (param.ToString() == "AddRestaurant")
				SelectedViewModel = new AddRestaurantViewModel();
			else if (param.ToString() == "AllRestaurants")
				SelectedViewModel = new AllRestaurantsViewModel(this);
		}
		public MainViewModel(bool is_admin) 
		{
			Is_admin = is_admin;
			if (!Is_admin) 
			{
				Visibility = Visibility.Collapsed;
			}
		}
	}
	
}
