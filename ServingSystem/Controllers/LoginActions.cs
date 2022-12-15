using DataLayer;
using LogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServingSystem.Controllers
{
    public class LoginActions : Controller
    {
        private static StaffContainer staffContainer = new StaffContainer(new StaffDAL());
        public HttpContext Context;

        public static bool IsLoggedIn(HttpContext _context)
        {
            var loggedInId = _context.Session.GetInt32("UserId");
            if (loggedInId != null && loggedInId != 0) return true;
            else return false;
        }

        public static bool IsAdmin(HttpContext _context)
        {
            if (!IsLoggedIn(_context)) return false;
            var loggedInId = _context.Session.GetInt32("UserId");
            return staffContainer.GetUserById((int)loggedInId).IsAdmin;
        }

        public static void LogoutUser(HttpContext _context)
        {
            _context.Session.Remove("UserId");
        }

        public static ValidationResponse Login(string username, string password, HttpContext _context)
        {
            ValidationResponse response = new ValidationResponse();
            int userId = 0;
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
            {
                response.Success = false;
                response.Errors.Add("Username or Password was incorrect!");
            }

            if (response.Success)
            {
                userId = staffContainer.AttemptLogin(username, password);
                if (userId == 0)
                {
                    response.Success = false;
                    response.Errors.Add("Username or Password was incorrect!");
                }
                else
                {
                    _context.Session.SetInt32("UserId", userId);
                    response.Message = "Login Successfull";
                }
            }

            return response;
        }

        public static Staff GetLoggedInUser(HttpContext _context)
        {
            return staffContainer.GetUserById((int)_context.Session.GetInt32("UserId"));
        }
    }

    /// <summary>
    /// Response for function with bool's for success and messages or errormessages.
    /// </summary>
    /// <remarks>Success is true by default.</remarks>
    public class ValidationResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public ValidationResponse()
        {
            Success = true;
            Errors = new List<string>();
        }
    }
}
