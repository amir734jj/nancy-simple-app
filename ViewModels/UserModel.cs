using System;
using System.Dynamic;

namespace ViewModels {
    
    [Serializable]
    public class User {
        public String Firstname { get; set; }

        public String Lastname{ get; set; }

        public String Email { get; set; }

        public String Username { get; set; }

        public String Password { get; set; }

        public User() {
            
        }

        public User(String Firstname, String Lastname, String Email, String Username, String Password)
            : this() {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Email = Email;
            this.Username = Username;
            this.Password = Password;
        }

        public override String ToString() {
            return String.Format("[User: Firstname={0}, Lastname={1}, Email={2}, Username={3}, Password={4}]", Firstname, Lastname, Email, Username, Password);
        }
    }
}

