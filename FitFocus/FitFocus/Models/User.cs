using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitFocus.Models
{
    public class User
    {
        public string SecureString;
        public string Token;
        public string Username;
        public string Policies;
        public bool IsAuthenticated;
        public bool SaveLogin;

        public User()
        {
            this.IsAuthenticated = false;
        }

        /// <summary>
        /// Set the values from login to the app Database
        /// </summary>
        /// <returns></returns>
        public async Task AsyncPersistsPropertiesOnSuccessfulLogin()
        {
            await AsyncTryPersistObject("SecureString", this.SecureString);
            await AsyncTryPersistObject("SaveLogin", this.SaveLogin);
            await App.Current.SavePropertiesAsync();
        }

        /// <summary>
        /// Set the value to the db
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="IsString"></param>
        /// <returns></returns>
        private async Task AsyncTryPersistObject(string key, object obj)
        {
            Application.Current.Properties[key] = obj;
        }
    }

    public class Response
    {
        public bool success { get; set; }
        public LoginData data { get; set; }
        public string message { get; set; }
    }

    public class LoginData
    {
        public string username;
        public string token;
        public string policies;
    }
}

