using GoodFood.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace GoodFood {
  public static class DB {
    //private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    //private static string connectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
    private static string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    private static DbContextOptionsBuilder<RestaurantContext> optionsBuilderRestaurant = new DbContextOptionsBuilder<RestaurantContext>();
    private static DbContextOptions<RestaurantContext> optionsRestaurant = optionsBuilderRestaurant.UseSqlServer(connectionString).Options;
    private static DbContextOptionsBuilder<UserContext> optionsBuilderUser = new DbContextOptionsBuilder<UserContext>();
    private static DbContextOptions<UserContext> optionsUser = optionsBuilderUser.UseSqlServer(connectionString).Options;
    private static DbContextOptionsBuilder<RatingContext> optionsBuilderRating = new DbContextOptionsBuilder<RatingContext>();
    private static DbContextOptions<RatingContext> optionsRating = optionsBuilderRating.UseSqlServer(connectionString).Options;
    private static DbContextOptionsBuilder<BookingContext> optionsBuilderBooking = new DbContextOptionsBuilder<BookingContext>();
    private static DbContextOptions<BookingContext> optionsBooking = optionsBuilderBooking.UseSqlServer(connectionString).Options;

    public static BindingList<User> GetUsers() {
      BindingList<User> users = new BindingList<User>();
      try {
        UserContext db = new UserContext(optionsUser);
        db.User.Load();
        users = db.User.Local.ToBindingList();
        return users;
      }
      catch(Exception e) {
        MessageBox.Show(e.Message);
        return users;
      }
    }
    public static BindingList<Booking> GetBookings() {
      BindingList<Booking> bookings = new BindingList<Booking>();
      try {
        BookingContext db = new BookingContext(optionsBooking);
        db.Booking.Load();
        bookings = db.Booking.Local.ToBindingList();
        return bookings;
      }
      catch(Exception e) {
        MessageBox.Show(e.Message);
        return bookings;
      }
    }
    public static BindingList<Rating> GetRatings() {
      BindingList<Rating> ratings = new BindingList<Rating>();
      try {
        RatingContext db = new RatingContext(optionsRating);
        db.Rating.Load();
        ratings = db.Rating.Local.ToBindingList();
        return ratings;
      }
      catch(Exception e) {
        MessageBox.Show(e.Message);
        return ratings;
      }
    }
    public static void AddBooking(int rest_id, int user_id, DateTime date, int table) {
      try {
        BookingContext db = new BookingContext(optionsBooking);
        Booking booking = new Booking(rest_id, user_id, date, table);
        db.Booking.Add(booking);
        db.SaveChanges();
      }
      catch(Exception e) {
        MessageBox.Show(e.InnerException.Message);
      }
    }
    public static void AddUser(string email, string hashPass, string name, string surname) {
      try {
        UserContext db = new UserContext(optionsUser);
        User user = new User(email, hashPass, name, surname, false);
        db.User.Add(user);
        db.SaveChanges();
      }
      catch(Exception e) {
        MessageBox.Show(e.InnerException.Message);
      }
    }
    public static void AddRestaurant(string name, int number_of_tables, int start, int end, string image, string type) {
      try {
        RestaurantContext db = new RestaurantContext(optionsRestaurant);
        Restaurant rest = new Restaurant(name, number_of_tables, start, end, image, type);
        db.Restaurant.Add(rest);
        db.SaveChanges();
      }
      catch(Exception e) {
        MessageBox.Show(e.InnerException.Message);
      }
    }
    public static void AddRating(int rest_id, int user_id, int rate) {
      try {
        RatingContext db = new RatingContext(optionsRating);
        Rating rating = new Rating(rate, user_id, rest_id);
        db.Rating.Add(rating);
        db.SaveChanges();
      }
      catch(Exception e) {
        MessageBox.Show(e.InnerException.Message);
      }
    }
    public static BindingList<Restaurant> GetRestaurants() {
      BindingList<Restaurant> restaurants = new BindingList<Restaurant>();
      try {
        RestaurantContext db = new RestaurantContext(optionsRestaurant);
        db.Restaurant.Load();
        restaurants = db.Restaurant.Local.ToBindingList();
        var tasks = new List<Task>();
        Parallel.ForEach(restaurants, el => {
          tasks.Add(Task.Run(() => {
            el.Restaurant_Picture = new Picture();
            el.Restaurant_Picture.PictureString = el.Restaurant_String_Image;
            el.Restaurant_ImageSource = ImageConverter.ImageSourceFromBitmap(el.Restaurant_Picture.Source);
            el.Restaurant_ImageSource.Freeze();
          }));
        });
        Task t = Task.WhenAll(tasks);
        t.Wait();
        return restaurants;
      }

      catch(Exception e) {
        MessageBox.Show(e.Message);
        return restaurants;
      }
    }
    public static void EditRating(Rating rating) {
      try {
        RatingContext db = new RatingContext(optionsRating);
        db.Rating.Update(rating);
        db.SaveChanges();
      }
      catch(Exception e) {
        MessageBox.Show(e.Message);
      }
    }
    public static void DeleteBooking(Booking booking) {
      try {
        BookingContext db = new BookingContext(optionsBooking);
        db.Booking.Remove(booking);
        db.SaveChanges();
      }
      catch(Exception e) {
        MessageBox.Show(e.Message);
      }
    }
  }
}
