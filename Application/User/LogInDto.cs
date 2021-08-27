using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public class LogInDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code_Challenge { get; set; }
    }
}
