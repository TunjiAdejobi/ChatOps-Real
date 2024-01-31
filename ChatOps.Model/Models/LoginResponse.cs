using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Model.Models
{
    public class LoginResponse
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        //public bool IsActive { get; set; }
    }
}
