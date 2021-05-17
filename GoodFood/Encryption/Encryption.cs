using System.Security.Cryptography;
using System.Text;

namespace GoodFood {
  class Encryption {
    public static string Encrypt(string password) {
      using(SHA256 mySHA256 = SHA256.Create()) {
        byte[] bytes = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i < bytes.Length; i++) {
          builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
      }
    }
  }
}
