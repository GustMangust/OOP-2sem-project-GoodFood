using GoodFood.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace GoodFood
{
    public static class DB
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static DbContextOptionsBuilder<RestaurantContext> optionsBuilderRestaurant = new DbContextOptionsBuilder<RestaurantContext>();
        private static DbContextOptions<RestaurantContext> optionsRestaurant = optionsBuilderRestaurant.UseSqlServer(connectionString).Options;
        private static DbContextOptionsBuilder<UserContext> optionsBuilderUser = new DbContextOptionsBuilder<UserContext>();
        private static DbContextOptions<UserContext> optionsUser = optionsBuilderUser.UseSqlServer(connectionString).Options;
        private static DbContextOptionsBuilder<RatingContext> optionsBuilderRating = new DbContextOptionsBuilder<RatingContext>();
        private static DbContextOptions<RatingContext> optionsRating = optionsBuilderRating.UseSqlServer(connectionString).Options;
        private static DbContextOptionsBuilder<BookingContext> optionsBuilderBooking = new DbContextOptionsBuilder<BookingContext>();
        private static DbContextOptions<BookingContext> optionsBooking = optionsBuilderBooking.UseSqlServer(connectionString).Options;

        public static BindingList<User> GetUsers() 
        {
            BindingList<User> users = new BindingList<User>();
            try 
            {
                UserContext db = new UserContext(optionsUser);
                db.User.Load();
                users = db.User.Local.ToBindingList();
                return users;
            }
            catch(Exception e) 
            {
                MessageBox.Show(e.Message);
                return users;
            }
        }
        public static BindingList<Booking> GetBookings() 
        {
            BindingList<Booking> bookings = new BindingList<Booking>();
            try 
            {
                BookingContext db = new BookingContext(optionsBooking);
                db.Booking.Load();
                bookings = db.Booking.Local.ToBindingList();
                return bookings;
            }
            catch(Exception e) 
            {
                MessageBox.Show(e.Message);
                return bookings;
            }
        }
        public static BindingList<Rating> GetRatings() 
        {
            BindingList<Rating> ratings = new BindingList<Rating>();
            try 
            {
                RatingContext db = new RatingContext(optionsRating);
                db.Rating.Load();
                ratings = db.Rating.Local.ToBindingList();
                return ratings;
            }
            catch(Exception e) 
            {
                MessageBox.Show(e.Message);
                return ratings;
            }
        }
        public static void AddUser(string email, string hashPass, string name,string surname) 
        {
            try
            {
                UserContext db = new UserContext(optionsUser);
                User user = new User(email, hashPass, name, surname,false);
                db.User.Add(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }
        public static void AddRestaurant(string name, int number_of_tables, int start,int end, byte[] image,string type) 
        {
            try
            {
                RestaurantContext db = new RestaurantContext(optionsRestaurant);
                MessageBox.Show(name);
                Restaurant rest = new Restaurant(name, number_of_tables, start, end, image, type);
                db.Restaurant.Add(rest);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }
        public static BindingList<Restaurant> GetRestaurants()
        {
            BindingList<Restaurant> restaurants = new BindingList<Restaurant>();
            try
            {
                RestaurantContext db = new RestaurantContext(optionsRestaurant);
                db.Restaurant.Load();
                restaurants = db.Restaurant.Local.ToBindingList();
                var tasks = new List<Task>();
                Parallel.ForEach(restaurants, el => {
                    tasks.Add(Task.Run(() =>
                    {
                        el.Restaurant_Picture = new Picture();
                        el.Restaurant_Picture.PictureByteArray = el.Image;
                        el.Restaurant_ImageSource = ImageConverter.ImageSourceFromBitmap(el.Restaurant_Picture.Source);
                        el.Restaurant_ImageSource.Freeze();
                    }));
                });
                Task t = Task.WhenAll(tasks);
                t.Wait();
                return restaurants;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return restaurants;
            }
        }
    }
}
