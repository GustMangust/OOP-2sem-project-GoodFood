using System.ComponentModel.DataAnnotations;

namespace GoodFood.Model {
  public class Rating {
    [Key]
    public int Rate_ID { get; set; }
    public int Rate { get; set; }
    public int User_ID { get; set; }
    public int Rest_ID { get; set; }
    public Rating() { }
    public Rating(int rate_ID, int rate, int user_ID, int rest_ID) {
      Rate_ID = rate_ID;
      Rate = rate;
      User_ID = user_ID;
      Rest_ID = rest_ID;
    }
    public Rating(int rate, int user_ID, int rest_ID) {
      Rate = rate;
      User_ID = user_ID;
      Rest_ID = rest_ID;
    }
  }
}
