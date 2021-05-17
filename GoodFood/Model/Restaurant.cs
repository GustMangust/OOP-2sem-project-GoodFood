using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace GoodFood.Model {
  public class Restaurant {
    [Key]
    public int Rest_ID { get; set; }
    public string Name { get; set; }
    public int Number_of_tables { get; set; }
    public int Start_time { get; set; }
    private int _end_time;
    public int End_time {
      get {
        return _end_time;
      }
      set {
        _end_time = value;
      }
    }
    public string Type_of_cuisine { get; set; }
    public string Restaurant_String_Image { get; set; }
    [NotMapped]
    public Picture Restaurant_Picture { get; set; }
    [NotMapped]
    public Decimal Rating { get; set; }
    [NotMapped]
    public ImageSource Restaurant_ImageSource { get; set; }
    public Restaurant() { }
    public Restaurant(int id, string name, int num, int start, int end, string im, string type) {
      Rest_ID = id;
      Name = name;
      Number_of_tables = num;
      Start_time = start;
      End_time = end;
      Restaurant_String_Image = im;
      Type_of_cuisine = type;
    }
    public Restaurant(string name, int num, int start, int end, string im, string type) {
      Name = name;
      Number_of_tables = num;
      Start_time = start;
      End_time = end;
      Restaurant_String_Image = im;
      Type_of_cuisine = type;
    }
  }
}
