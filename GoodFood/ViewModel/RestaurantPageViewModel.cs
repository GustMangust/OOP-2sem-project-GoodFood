using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GoodFood.ViewModel
{
    class RestaurantPageViewModel:ViewModelBase
    {
        public ImageSource Image { get; set; }
        public string Name { get; set; }
        public string Type_of_cuisine { get; set; }
        public string Start_time { get; set; }
        public string End_time { get; set; }
        private List<string> list_of_time;

        public List<string> List_of_time
        {
            get 
            {
                return list_of_time; 
            }
            set 
            {
                list_of_time = value;
                RaisePropertyChanged("List_of_time");
            }
        }
        private string selected_time;

        public string Selected_time
        {
            get { return  selected_time; }
            set {  selected_time = value; RaisePropertyChanged("Selected_time"); }
        }

        private string selected_month;
        public string Selected_month
        {
            get { return selected_month; }
            set 
            {
                List<string> list_of_days_buf = new List<string>();
                for(int i = 1;i<= DateTime.DaysInMonth(2021, DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(value) + 1); i++) 
                {
                    list_of_days_buf.Add("" + i); 
                }
                List_of_days = list_of_days_buf;
                selected_month = value;
                RaisePropertyChanged("Selected_month");
            }
        }

        private string selected_day;
        public string Selected_day
        {
            get { return  selected_day; }
            set {  selected_day = value; RaisePropertyChanged("Selected_month"); }
        }

        private List<string> list_of_months;
        public List<string> List_of_months
        {
            get
            {
                return list_of_months;
            }
            set
            {
                list_of_months = value;
                RaisePropertyChanged("List_of_months");
            }
        }

        private List<string> list_of_days;

        public List<string> List_of_days
        {
            get
            {
                return list_of_days;
            }
            set
            {
                list_of_days = value;
                RaisePropertyChanged("List_of_days");
            }
        }
        public ICommand restaurantBooking => new DelegateCommand(RestaurantBooking);
        private void RestaurantBooking()
        {
            
            //MessageBox.Show("" + DateTime.DaysInMonth(2021, DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(Selected_month) + 1));           
            DateTime time = new DateTime(2021, DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(Selected_month) + 1, Convert.ToInt32(Selected_day));
            time.AddHours(Convert.ToInt32(Selected_time.Trim('0')));
            MessageBox.Show((time.Date < DateTime.Now.Date)+"");
        }
        public RestaurantPageViewModel(MainViewModel mvm, AllRestaurantsViewModel arvm) 
        {
            Image = arvm.SelectedRestaurant.Restaurant_ImageSource;
            Name = arvm.SelectedRestaurant.Name;
            Type_of_cuisine = arvm.SelectedRestaurant.Type_of_cuisine;
            if(arvm.SelectedRestaurant.Start_time.ToString().Length == 1)
                Start_time ="0" + arvm.SelectedRestaurant.Start_time;
            else Start_time = ""+arvm.SelectedRestaurant.Start_time;
            if (arvm.SelectedRestaurant.End_time.ToString().Length == 1)
                End_time ="0"+ arvm.SelectedRestaurant.End_time;
            else End_time = ""+ arvm.SelectedRestaurant.End_time;
            List_of_time = new List<string>();
            if(arvm.SelectedRestaurant.End_time - arvm.SelectedRestaurant.Start_time > 0) 
            {
                for(int i = arvm.SelectedRestaurant.Start_time;i< arvm.SelectedRestaurant.End_time; i++) 
                {
                    if (i.ToString().Length == 1) 
                    {
                        List_of_time.Add("0"+i+":00");
                    }else List_of_time.Add( i + ":00");
                }
            }else {
                for(int i = 0;i< arvm.SelectedRestaurant.End_time; i++) 
                {
                    if (i.ToString().Length == 1)
                    {
                        List_of_time.Add("0" + i + ":00");
                    }
                    else List_of_time.Add("0" + i + ":00");
                }
                for (int i = arvm.SelectedRestaurant.Start_time; i < 24; i++)
                {
                    if (i.ToString().Length == 1)
                    {
                        List_of_time.Add("0" + i + ":00");
                    }
                    else List_of_time.Add( i + ":00");
                }
            }
            List_of_months = new List<string>(DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToArray());
        }
    }
}
