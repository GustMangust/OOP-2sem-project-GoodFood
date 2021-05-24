using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GoodFood {
  class Validation {
    public static bool IsEmailValid(string emailaddress) {
      try {
        MailAddress m = new MailAddress(emailaddress);
        if(Regex.IsMatch(emailaddress, @"^[\w!#$%&'*+\-\/=?\^_`{|}~]+(\.[\w!#$%&'*+\-\/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z")) {
          return true;
        } else {
          throw new Exception();
        }
      }
      catch(Exception e) {
        return false;
      }
    }
    public static bool IsNameSurnameValid(string name) {
      if(Regex.IsMatch(name, @"^[a-zа-я]{1,30}$", RegexOptions.IgnoreCase))
        return true;
      else
        return false;
    }
    public static bool IsRestaurantNameValid(string restaurant_name) {
      if(restaurant_name != null) {
        if(Regex.IsMatch(restaurant_name, @"^[A-Za-zа-яА-Я]{3,15}$", RegexOptions.IgnoreCase))
          return true;
      }
      return false;
    }
    public static bool IsTimeValid(string time) {
      if(time != null) {
        if(Regex.IsMatch(time, @"^(0[0-9]|1[0-9]|2[0-3])$", RegexOptions.IgnoreCase))
          return true;
      }
      return false;
    }
    public static bool IsNumberOfTablesValid(string number_of_tables) {
      try {
        int value = Convert.ToInt32(number_of_tables);
        if(value > 0) {
          return true;
        } else {
          return false;
        }
      }
      catch(Exception e) {
        return false;
      }
    }
    public static bool IsPasswordValid(string password) {
      if(Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S{6,20}$")) {
        return true;
      } else
        return false;
    }

  }
}
