using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GoodFood.ViewModel {
  class UserBookingsViewModel : ViewModelBase {
    public class BookingFormated {
      public int BookingId { get; set; }
      public string RestaurantName { get; set; }
      public string TableNumber { get; set; }
      public string BookingTime { get; set; }
      public string BookingDate { get; set; }
      public BookingFormated(int bookingId,
                             string restName,
                             string tableNum,
                             string bookingTime,
                             string bookingDate) {
        BookingId = bookingId;
        RestaurantName = restName;
        TableNumber = tableNum;
        BookingTime = bookingTime;
        BookingDate = bookingDate;
      }
    }
    public ICommand startAimingBooking => new DelegateCommand(StartAimingBooking);
    public ICommand stopAimingBooking => new DelegateCommand(StopAimingBooking);
    public ICommand deleteBooking => new DelegateCommand(DeleteBooking);
    private Visibility _buttonVisibility = Visibility.Hidden;
    private Visibility _emptyVisibility = Visibility.Collapsed;
    private Visibility _notEmptyVisibility = Visibility.Visible;
    private Visibility _notEmptyExpiredVisibility = Visibility.Visible;
    private ObservableCollection<BookingFormated> _currentBookings;
    public Visibility EmptyVisibility {
      get {
        return _emptyVisibility;
      }
      set {
        _emptyVisibility = value;
        RaisePropertyChanged("EmptyVisibility");
      }
    }
    public Visibility NotEmptyVisibility {
      get {
        return _notEmptyVisibility;
      }
      set {
        _notEmptyVisibility = value;
        RaisePropertyChanged("NotEmptyVisibility");
      }
    }
    public Visibility NotEmptyExpiredVisibility {
      get {
        return _notEmptyExpiredVisibility;
      }
      set {
        _notEmptyExpiredVisibility = value;
        RaisePropertyChanged("NotEmptyExpiredVisibility");
      }
    }
    public Visibility ButtonVisibility {
      get {
        return _buttonVisibility;
      }
      set {
        _buttonVisibility = value;
        RaisePropertyChanged("ButtonVisibility");
      }
    }
    public BookingFormated SelectedBooking { get; set; }
    public List<BookingFormated> ExpiredBookings { get; set; }
    public ObservableCollection<BookingFormated> CurrentBookings {
      get {
        return _currentBookings;
      }
      set {
        _currentBookings = value;
        RaisePropertyChanged("CurrentBookings");
      }
    } 
    public MainViewModel MainViewModel { get; set; }
    
    public UserBookingsViewModel(MainViewModel mainViewModel) {
      MainViewModel = mainViewModel;
      UpdateBookings();
    }
    private void UpdateBookings() {
      ExpiredBookings = new List<BookingFormated>();
      List<Booking> bookings = new List<Booking>(DB.GetBookings());
      ObservableCollection<BookingFormated> currentBookingsBuf = new ObservableCollection<BookingFormated>();
      foreach(Booking booking in bookings) {
        if(booking.User_ID == MainViewModel.User.User_ID) {
          string time = booking.DateTime.TimeOfDay.ToString().Substring(0, 5);
          string date = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(booking.DateTime.Month) + ", " + booking.DateTime.Day;
          string restName = DB.GetRestaurants().FirstOrDefault(x => x.Rest_ID == booking.Rest_ID).Name;
          if(booking.DateTime > DateTime.Now) {
            currentBookingsBuf.Add(new BookingFormated(booking.Booking_ID, restName, booking.Number_of_table.ToString(), time, date));
          } else {
            ExpiredBookings.Add(new BookingFormated(booking.Booking_ID, restName, booking.Number_of_table.ToString(), time, date));
          }
        }
      }
      CurrentBookings = currentBookingsBuf;
      if(CurrentBookings.Count == 0) {
        EmptyVisibility = Visibility.Visible;
        NotEmptyVisibility = Visibility.Collapsed;
      } else {
        EmptyVisibility = Visibility.Collapsed;
        NotEmptyVisibility = Visibility.Visible;
      }
      if(ExpiredBookings.Count == 0) {
        NotEmptyExpiredVisibility = Visibility.Collapsed;
      } else {
        NotEmptyExpiredVisibility = Visibility.Visible;
      }
    }
    private void DeleteBooking() {
      int restId = DB.GetRestaurants().FirstOrDefault(x => x.Name == SelectedBooking.RestaurantName).Rest_ID;
      int month = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList().IndexOf(SelectedBooking.BookingDate.Substring(0, SelectedBooking.BookingDate.IndexOf(','))) + 1;
      int day = Convert.ToInt32(SelectedBooking.BookingDate.Substring(SelectedBooking.BookingDate.IndexOf(' '), SelectedBooking.BookingDate.Length - SelectedBooking.BookingDate.IndexOf(' ')));
      int hour;
      if(SelectedBooking.BookingTime[0] == '0') {
        hour = Convert.ToInt32(SelectedBooking.BookingTime.Substring(1, 1));
      } else
        hour = Convert.ToInt32(SelectedBooking.BookingTime.Substring(0, 2));
      DateTime dateTime = new DateTime(2021, month, day, hour, 0, 0);
      int tableNumber = Convert.ToInt32(SelectedBooking.TableNumber.Trim(' ', '№'));
      Booking booking = new Booking(SelectedBooking.BookingId,restId,MainViewModel.User.User_ID,dateTime,tableNumber);
      DB.DeleteBooking(booking);
      UpdateBookings();
    }
    private void StartAimingBooking() {  
      ButtonVisibility = Visibility.Visible;
    }
    private void StopAimingBooking() {
      ButtonVisibility = Visibility.Hidden;
    }
  }
}
