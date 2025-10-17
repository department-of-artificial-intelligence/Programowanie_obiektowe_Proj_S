using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class User
    {
        public int _id { get; set; }

        public string Username { get; set; }
        public string HashPassword { get; set; }

        // Default Constructor without params
        public User() { }

        public User(int id, string username, string hashPassword)
        {
            _id = id;
            this.Username = username;
            this.HashPassword = hashPassword;
        }


        public override string ToString()
        {
            return String.Format("User: {0} with username: {1}",_id, Username);
        }

    }
}
