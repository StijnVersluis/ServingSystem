using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ServingSystem.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public LoginViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
