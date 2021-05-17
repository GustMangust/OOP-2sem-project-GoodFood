using System.ComponentModel.DataAnnotations;

namespace GoodFood.Model {
  public class User {
    [Key]
    public int User_ID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool Is_admin { get; set; }
    public User() { }
    public User(int user_ID, string email, string password, string name, string surname, bool is_admin) {
      User_ID = user_ID;
      Email = email;
      Password = password;
      Name = name;
      Surname = surname;
      Is_admin = is_admin;

    }
    public User(string email, string password, string name, string surname, bool is_admin) {
      Email = email;
      Password = password;
      Name = name;
      Surname = surname;
      Is_admin = is_admin;
    }
  }
}
