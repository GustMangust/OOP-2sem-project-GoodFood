using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GoodFood.ViewModel
{
    class AllRestaurantsViewModel:ViewModelBase, IDataErrorInfo
    {
        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "Time":
                        if (!Validation.IsTimeValid(Time))
                            result = "Неверный формат или неверные символы";
                        break;
                }
                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);
                RaisePropertyChanged("ErrorCollection");
                return result;
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
        private string time;

        public string Time
        {
            get 
            {
                return time; 
            }
            set 
            { 
                time = value;
                Restaurants = BufferRestaurants;
                foreach (var rest in Restaurants)
                {
                    if(value!=null && value !="")
                        if (Convert.ToInt32(value.Trim('0')) >=rest.Start_time && Convert.ToInt32(value.Trim('0')) <=rest.End_time)
                        {
                            SortedRestaurants.Add(rest);
                        }
                }
                Restaurants = new ObservableCollection<Restaurant>(SortedRestaurants);
                RaisePropertyChanged("Time");
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
                Restaurants = new ObservableCollection<Restaurant>(SortedRestaurants);
                SortedRestaurants.Clear();
                RaisePropertyChanged("SortByTypes");
            }
        }
        public AllRestaurantsViewModel(MainViewModel vm)
        {
            vm.SelectedViewModel = new AddRestaurantViewModel();
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
