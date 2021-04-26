using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GoodFood.ViewModel
{
    class AllRestaurantsViewModel:ViewModelBase
    {
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
                RaisePropertyChanged("Name");
            }
        }
        private string number_of_tables;
        public string Number_of_tables
        {
            get
            {
                return number_of_tables;
            }
            set
            {
                number_of_tables = value;
                RaisePropertyChanged("Number_of_tables");
            }
        }
        private string start_time;
        public string Start_time
        {
            get
            {
                return start_time;
            }
            set
            {
                start_time = value;
                RaisePropertyChanged("Start_time");
            }
        }
        private string end_time;
        public string End_time
        {
            get
            {
                return end_time;
            }
            set
            {
                end_time = value;
                RaisePropertyChanged("End_time");
            }
        }
        private ObservableCollection<Restaurant> restaurants;
        public ObservableCollection<Restaurant> Restaurants 
        {
            get 
            {
                return restaurants;
            }
            set 
            {
                restaurants= value;
                RaisePropertyChanged("Restaurants");
            }
        }
        private Restaurant selectedRestaurant;
        public Restaurant SelectedRestaurant
        {
            get
            {
                return selectedRestaurant;
            }
            set
            {
                selectedRestaurant = value;
                RaisePropertyChanged("SelectedRestaurant");
            }
        }
        public AllRestaurantsViewModel() 
        {
            Restaurants = new ObservableCollection<Restaurant>(DB.GetRestaurants());
        }
    }
}
