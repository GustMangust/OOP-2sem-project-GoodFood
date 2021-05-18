using DevExpress.Mvvm;
using GoodFood.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

namespace GoodFood.ViewModel {
  class AdminStatisticsViewModel:ViewModelBase {
    public  string TimeOne { get; set; }
    public  string TimeTwo { get; set; }
    public  string TimeThree { get; set; }
    public string DayOfWeekOne { get; set; }
    public string DayOfWeekTwo { get; set; }
    public string DayOfWeekThree { get; set; }
    public  AdminStatisticsViewModel(MainViewModel mainViewModel) {
      IEnumerable<TimeSpan> top3Time = DB.GetBookings()
                                    .GroupBy(q => q.DateTime.TimeOfDay)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(3)
                                    .Select(g => g.Key).ToList();
      List<TimeSpan> list = top3Time.ToList();
      List<string> timeList = new List<string>();
      foreach(TimeSpan t in list) {
        if(t.Hours < 10) {
          timeList.Add("0" + t.Hours + ":00");
        } else {
          timeList.Add(t.Hours + ":00");
        }
      }
      if(timeList.Count() == 3) {
        TimeOne = timeList[0];
        TimeTwo = timeList[1];
        TimeThree = timeList[2];
      } else if(timeList.Count() == 2) {
        TimeOne = timeList[0];
        TimeTwo = timeList[1];
      }else if(timeList.Count() == 1) {
        TimeOne = timeList[0];
      }
      IEnumerable<DayOfWeek> top3Day = DB.GetBookings()
                                    .GroupBy(q => q.DateTime.DayOfWeek)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(3)
                                    .Select(g => g.Key).ToList();
      List<string> dayList = new List<string>();
      var culture = new CultureInfo("ru-RU");
      foreach(DayOfWeek d in top3Day) {
        dayList.Add(culture.DateTimeFormat.GetDayName(d).First().ToString().ToUpper() + culture.DateTimeFormat.GetDayName(d).Substring(1));
      }
      if(dayList.Count() == 3) {
        DayOfWeekOne = dayList[0];
        DayOfWeekTwo = dayList[1];
        DayOfWeekThree = dayList[2];
      } else if(dayList.Count() == 2) {
        DayOfWeekOne = dayList[0];
        DayOfWeekTwo = dayList[1];
      } else if(dayList.Count() == 1) {
        DayOfWeekOne = dayList[0];
      }
    }
  }
}
