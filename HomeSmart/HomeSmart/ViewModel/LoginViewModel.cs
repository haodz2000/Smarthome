using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HomeSmart.Model;


namespace HomeSmart.ViewModel
{
    class LoginViewModel: BaseViewModel
    {
        public  bool IsLogin { get; set; }
        private string _Username;
        public User client { get; set; }
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        public string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            IsLogin = false;
            Username = "";
            Password = "";
            LoginCommand = new RelayCommand<Window>((p) => { if (Username == "" || Password == "") { return false; } else { return true; } }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        void  Login(Window p)
        {
            if (p == null)
                return;
            User user = new User()
            {
                email = _Username,
                password = _Password
            };
            //string urlGet = "http://localhost:5000/api/posts/62a56c033e9d0558981b6fba";
            string urlPost = "auth/login";
            //var response = WebAPI.GetCall(urlGet);
            var response = WebAPI.PostCall<User>(urlPost, user);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                client = JsonConvert.DeserializeObject<User>(json);
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai ten tai khoan hoac mat khau");
            }
            




        }
       
     
    }
}
