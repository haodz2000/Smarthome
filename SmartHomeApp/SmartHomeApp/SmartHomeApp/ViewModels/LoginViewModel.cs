using SmartHomeApp.Models;
using SmartHomeApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SmartHomeApp.ViewModels
{
    public class Client
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class LoginViewModel : BaseViewModel
    {
        private User user;
        public User User
        {
            get => user;
            set{
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged(Username);
            }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged(Password);
            }
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            Username = "tahuuhao@gmail.com";
            Password = "123456";
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            User client = new User()
            {
                email = Username,
                password = Password
            };
            string url = "auth/login";
            User = await App.UserManager.SaveTaskAsync(client,url,true);
            if(User != null)
            {
                App.isUserLogin = true;
                App.User = User;
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
