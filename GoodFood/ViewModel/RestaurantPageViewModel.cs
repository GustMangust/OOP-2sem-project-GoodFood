using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GoodFood.ViewModel {
  class RestaurantPageViewModel : ViewModelBase {
    public MainViewModel mainViewModel { get; set; }
    public Restaurant SelectedRestaurant { get; set; }
    public User SelectedUser { get; set; }
    public ImageSource Image { get; set; }
    public List<string> Reserved_tables { get; set; }
    public string Name { get; set; }
    public string Type_of_cuisine { get; set; }
    public string Start_time { get; set; }
    public string End_time { get; set; }
    private List<string> list_of_time;

    public List<string> List_of_time {
      get {
        return list_of_time;
      }
      set {
        list_of_time = value;
        RaisePropertyChanged("List_of_time");
      }
    }
    private List<int> list_of_rates;

    public List<int> List_of_rates {
      get { return list_of_rates; }
      set { list_of_rates = value; RaisePropertyChanged("List_of_rates"); }
    }
    private string selected_rate;

    public string Selected_rate {

      get { return selected_rate; }
      set {
        selected_rate = value;
        RaisePropertyChanged("Selected_rate");
      }
    }

    private string selected_time;
    public string Selected_time {
      get { return selected_time; }
      set {
        selected_time = value;
        List_of_tables = FillTables();
        RaisePropertyChanged("Selected_time");
      }
    }

    private string selected_month;
    public string Selected_month {
      get {
        return selected_month;
      }
      set {
        List<string> list_of_days_buf = new List<string>();
        for(int i = 1; i <= DateTime.DaysInMonth(2021, DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(value) + 1); i++) {
          list_of_days_buf.Add("" + i);
        }
        List_of_days = list_of_days_buf;
        selected_month = value;
        List_of_tables = FillTables();
        RaisePropertyChanged("Selected_month");
      }
    }
    public int Free_tables { get; set; }
    private string selected_day;
    public string Selected_day {
      get { return selected_day; }
      set {
        selected_day = value;
        List_of_tables = FillTables();
        RaisePropertyChanged("Selected_day");
      }
    }
    private string selected_table;

    public string Selected_table {
      get { return selected_table; }
      set { selected_table = value; RaisePropertyChanged("Selected_table"); }
    }
    private List<string> list_of_tables;

    public List<string> List_of_tables {
      get { return list_of_tables; }
      set { list_of_tables = value; RaisePropertyChanged("List_of_tables"); }
    }

    private List<string> list_of_months;
    public List<string> List_of_months {
      get {
        return list_of_months;
      }
      set {
        list_of_months = value;
        RaisePropertyChanged("List_of_months");
      }
    }

    private List<string> list_of_days;
    public List<string> List_of_days {
      get {
        return list_of_days;
      }
      set {
        list_of_days = value;
        RaisePropertyChanged("List_of_days");
      }
    }
    private int FreeTables(DateTime datetime) {
      List<Booking> list = new List<Booking>(DB.GetBookings());
      Free_tables = SelectedRestaurant.Number_of_tables;
      foreach(Booking b in list) {
        if(b.DateTime == datetime && SelectedRestaurant.Rest_ID == b.Rest_ID) {
          Free_tables--;
        }
      }
      return Free_tables;
    }
    private List<string> FillTables() {
      List<string> List_of_tables_buf = new List<string>();
      for(int i = 1; i <= SelectedRestaurant.Number_of_tables; i++)
        List_of_tables_buf.Add("№" + i);
      if(Selected_day != null && Selected_month != null && Selected_time != null) {
        string time = Selected_time.Substring(0, 2);
        if(time[0] == '0')
          time = "" + time[1];
        DateTime datetime = new DateTime(2021, DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(Selected_month) + 1, Convert.ToInt32(Selected_day), Convert.ToInt32(time), 0, 0);
        List<Booking> list = new List<Booking>(DB.GetBookings());
        List<string> reserved_tables = new List<string>();
        foreach(Booking b in list) {
          if(b.DateTime == datetime && SelectedRestaurant.Rest_ID == b.Rest_ID) {
            reserved_tables.Add("№" + b.Number_of_table);
          }
        }
        foreach(string n in reserved_tables) {
          List_of_tables_buf.Remove(n);
        }
      }
      return List_of_tables_buf;
    }
    private bool IsUserBook(DateTime datetime) {
      List<Booking> list = new List<Booking>(DB.GetBookings());
      foreach(Booking b in list) {
        if(b.DateTime == datetime && SelectedRestaurant.Rest_ID == b.Rest_ID && b.User_ID == SelectedUser.User_ID) {
          return true;
        }
      }
      return false;
    }
    public ICommand restaurantBooking => new DelegateCommand(RestaurantBooking);
    private void RestaurantBooking() {
      string time = Selected_time.Substring(0, 2);
      if(time[0] == '0')
        time = "" + time[1];
      DateTime datetime = new DateTime(2021, DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(Selected_month) + 1, Convert.ToInt32(Selected_day), Convert.ToInt32(time), 0, 0);
      if(datetime >= DateTime.Now && FreeTables(datetime) > 0 && !IsUserBook(datetime) && FillTables().Count > 0) {
        DB.AddBooking(SelectedRestaurant.Rest_ID, SelectedUser.User_ID, datetime, Convert.ToInt32(Selected_table.Trim('№')));
        MessageBox.Show("Успешно!");
        mainViewModel.SelectedViewModel = new AllRestaurantsViewModel(mainViewModel);
      } else
        MessageBox.Show("Неверные данные или вы уже бронировали столик на это время!");
    }
    public ICommand restaurantRating => new DelegateCommand(RestaurantRating);
    private void RestaurantRating() {
      if(Selected_rate != null && Selected_rate != "") {
        List<Rating> list = new List<Rating>(DB.GetRatings());
        foreach(Rating rating in list) {
          if(rating.User_ID == SelectedUser.User_ID && rating.Rest_ID == SelectedRestaurant.Rest_ID) {
            rating.Rate = Convert.ToInt32(Selected_rate);
            DB.EditRating(rating);
            MessageBox.Show("Обновлено!");
            return;
          }
        }
        DB.AddRating(SelectedRestaurant.Rest_ID, SelectedUser.User_ID, Convert.ToInt32(Selected_rate));
        MessageBox.Show("Оценка добавлена!");
      }
    }
    public RestaurantPageViewModel(MainViewModel mvm, AllRestaurantsViewModel arvm) {
      mainViewModel = mvm;
      SelectedUser = mainViewModel.User;
      SelectedRestaurant = arvm.SelectedRestaurant;
      Image = SelectedRestaurant.Restaurant_ImageSource;
      Name = SelectedRestaurant.Name;
      Type_of_cuisine = SelectedRestaurant.Type_of_cuisine;
      if(SelectedRestaurant.Start_time.ToString().Length == 1)
        Start_time = "0" + SelectedRestaurant.Start_time;
      else
        Start_time = "" + SelectedRestaurant.Start_time;
      if(SelectedRestaurant.End_time.ToString().Length == 1)
        End_time = "0" + SelectedRestaurant.End_time;
      else
        End_time = "" + SelectedRestaurant.End_time;
      List_of_time = new List<string>();
      if(SelectedRestaurant.End_time - SelectedRestaurant.Start_time > 0) {
        for(int i = SelectedRestaurant.Start_time; i < SelectedRestaurant.End_time; i++) {
          if(i.ToString().Length == 1) {
            List_of_time.Add("0" + i + ":00");
          } else
            List_of_time.Add(i + ":00");
        }
      } else {
        for(int i = SelectedRestaurant.Start_time; i < 24; i++) {
          if(i.ToString().Length == 1) {
            List_of_time.Add("0" + i + ":00");
          } else
            List_of_time.Add(i + ":00");
        }
        for(int i = 0; i < SelectedRestaurant.End_time; i++) {
          if(i.ToString().Length == 1) {
            List_of_time.Add("0" + i + ":00");
          } else
            List_of_time.Add(i + ":00");
        }
      }
      List_of_months = new List<string>(DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToArray());
      List_of_rates = new List<int>() { 1, 2, 3, 4, 5 };
      List_of_tables = new List<string>();

    }
  }
}
