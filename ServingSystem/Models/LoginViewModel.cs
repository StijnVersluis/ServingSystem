using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServingSystem.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Passcode")]
        public string Password { get; set; }
        public List<string> ErrorMsgs { get; set; }

        public LoginViewModel()
        {
            ErrorMsgs = new();
        }
        public LoginViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
            ErrorMsgs = new();
        }
        public void AddErrors(List<string> errors)
        {
            ErrorMsgs = errors;
        }
    }
}
