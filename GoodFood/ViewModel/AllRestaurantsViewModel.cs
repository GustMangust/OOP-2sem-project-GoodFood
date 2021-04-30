using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GoodFood.ViewModel
{
    class AllRestaurantsViewModel:ViewModelBase
    {
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
        private ObservableCollection<Rating> ratings;
        public ObservableCollection<Rating> Ratings
        {
            get
            {
                return ratings;
            }
            set
            {
                ratings = value;
                RaisePropertyChanged("Ratings");
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
        private ObservableCollection<Restaurant> SortedRestaurants { get; set; } = new ObservableCollection<Restaurant>();
        private ObservableCollection<Restaurant> BufferRestaurants { get; set; } = new ObservableCollection<Restaurant>();
        private ObservableCollection<string> types_of_cuisine;
        public ObservableCollection<string> Types_of_cuisine
        { 
            get 
            {
                return types_of_cuisine;
            }
            set 
            {
                types_of_cuisine = value;
                RaisePropertyChanged("Types_of_cuisine");
            }
        }
        private string sortByTypes;

        public string SortByTypes
        {
            get 
            {
                return sortByTypes; 
            }
            set 
            {
                Restaurants = BufferRestaurants;
                sortByTypes = value;
                foreach(var rest in Restaurants) 
                {
                    if(rest.Type_of_cuisine == value.Substring(value.IndexOf(" ")+1)) 
                    {
                        SortedRestaurants.Add(rest);
                    }
                }
                Restaurants = new ObservableCollection<Restaurant>( SortedRestaurants);
                SortedRestaurants.Clear();
                RaisePropertyChanged("SortByTypes");
            }
        }

        public AllRestaurantsViewModel()
        {
            
            BufferRestaurants = new ObservableCollection<Restaurant>(DB.GetRestaurants());
            Restaurants = BufferRestaurants;
            Ratings = new ObservableCollection<Rating>(DB.GetRatings());
            Types_of_cuisine = new ObservableCollection<string>();
            
            foreach (var rest in Restaurants) 
            {
                int index = 0;
                int sum_of_rates = 0;
                foreach(var rate in Ratings) 
                {
                    if(rest.Rest_ID == rate.Rest_ID) 
                    {
                        sum_of_rates += rate.Rate;
                        index++;
                    }
                }
                if(index!=0)
                    rest.Rating = Math.Round((decimal)sum_of_rates / (decimal)index,1);
                Types_of_cuisine.Add(rest.Type_of_cuisine);
            }
            Types_of_cuisine = new ObservableCollection<string>( Types_of_cuisine.Distinct<string>());
        }
    }
}
