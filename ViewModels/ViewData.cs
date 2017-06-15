using System;
using System.Dynamic;

namespace ViewModels {
    
    [Serializable]
    public class ViewData {
        public User user { get; set; } = null;

        public Boolean isLoggedIn { get; set; } = false;

        public String message { get; set; } = "";

        public Boolean hasMassge { get; set; } = false;

        public ViewData() {
            
        }

        public ViewData(User user, Boolean isLoggedIn)
            : this() {
            this.user = user;
            this.isLoggedIn = isLoggedIn;
        }

        public ViewData(User user)
            : this(user, user != null) {
        }

        public ViewData(String message)
            : this() {
            this.message = message;
            this.hasMassge = String.IsNullOrEmpty(message);
        }

        public ViewData(User user, Boolean isLoggedIn, String message)
            : this(user, isLoggedIn) {
            this.message = message;
            this.hasMassge = String.IsNullOrEmpty(message);
        }

    }
}

