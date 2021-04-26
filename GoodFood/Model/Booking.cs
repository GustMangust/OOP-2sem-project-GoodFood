using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoodFood.Model
{
    public class Booking
    {
        [Key]
        public int Booking_ID {get;set;}
        public int Rest_ID {get;set;}
        public int User_ID {get;set;}
        public int Time {get;set;}
        public DateTime Date { get; set; }
        public Booking() { }
        public Booking(int booking_ID,int rest_ID,int user_ID,int time,DateTime date) 
        {
            Booking_ID = booking_ID;
            Rest_ID = rest_ID;
            User_ID = user_ID;
            Time = time;
            Date = date;
        }
    }
}
