using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class ResetPasswordDTO
    {
        public string Username { get; set; }
        public string Token {  get; set; }
        public string NewPassword { get; set; }        
    }
}
