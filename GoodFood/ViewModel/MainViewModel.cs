using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace GoodFood.ViewModel
{
    class MainViewModel:ViewModelBase
    {
		private ViewModelBase selectedViewModel;
		public User User { get; set; }
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
		private Visibility buttonOpenMenuVisibility = Visibility.Visible;
		private Visibility buttonCloseMenuVisibility = Visibility.Collapsed;

		public Visibility ButtonOpenMenuVisibility
		{
			get
			{
				return buttonOpenMenuVisibility;
			}
			set
			{
				buttonOpenMenuVisibility = value;
				RaisePropertyChanged("ButtonOpenMenuVisibility");
			}
		}
		public Visibility ButtonCloseMenuVisibility
		{
			get
			{
				return buttonCloseMenuVisibility;
			}
			set
			{
				buttonCloseMenuVisibility = value;
				RaisePropertyChanged("ButtonCloseMenuVisibility");
			}
		}
		public ICommand buttonCloseMenu => new DelegateCommand(ButtonCloseMenu);
		public void ButtonCloseMenu()
		{
			ButtonOpenMenuVisibility = Visibility.Visible;
			ButtonCloseMenuVisibility = Visibility.Collapsed;
		}

		public ICommand buttonOpenMenu => new DelegateCommand(ButtonOpenMenu);
		public void ButtonOpenMenu()
		{
			ButtonOpenMenuVisibility = Visibility.Collapsed;
			ButtonCloseMenuVisibility = Visibility.Visible;
		}
		public ICommand<object> updateViewModel => new DelegateCommand<object>(UpdateViewModel);
		private void UpdateViewModel(object param) 
		{
			if (param.ToString() == "AddRestaurant")
				SelectedViewModel = new AddRestaurantViewModel(this);
			else if (param.ToString() == "AllRestaurants")
				SelectedViewModel = new AllRestaurantsViewModel(this);
		}
		public MainViewModel(User user) 
		{
			User = user;
			SelectedViewModel = new AllRestaurantsViewModel(this);
			if (!User.Is_admin) 
			{
				Visibility = Visibility.Collapsed;
			}
		}
	}
	
}
