using System;
using System.ComponentModel.DataAnnotations;

namespace GoodFood.Model {
  public class Booking {
    [Key]
    public int Booking_ID { get; set; }
    public int Rest_ID { get; set; }
    public int User_ID { get; set; }
    public DateTime DateTime { get; set; }
    public int Number_of_table { get; set; }
    public Booking() { }
    public Booking(int booking_ID,
                   int rest_ID,
                   int user_ID,
                   DateTime date,
                   int number_of_table) {
      Booking_ID = booking_ID;
      Rest_ID = rest_ID;
      User_ID = user_ID;
      DateTime = date;
      Number_of_table = number_of_table;
    }
    public Booking(int rest_ID, int user_ID, DateTime date, int number_of_table) {
      Rest_ID = rest_ID;
      User_ID = user_ID;
      DateTime = date;
      Number_of_table = number_of_table;
    }
  }
}
