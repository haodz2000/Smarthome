using HomeSmart.Model;
using HomeSmart.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeSmart.ViewModel
{
   
    public class MainViewModel : BaseViewModel
    {
        MqttClient mqttClient;


        private Home _home;

        private string _Weather;
        public string Weather { get => _Weather; set { _Weather = value; OnPropertyChanged(); } }
        private int _Temperature;
        public int Temperature { get => _Temperature; set { _Temperature = value; OnPropertyChanged(); } }
        private int _Humidity;
        public int Humidity { get => _Humidity; set { _Humidity = value; OnPropertyChanged(); } }
        private string _Door;
        public string Door { get => _Door; set { _Door = value; OnPropertyChanged(); } }
        public Home home {
            get => _home;
            set 
            {
                _home = value;
                OnPropertyChanged(); 
                if(home != null)
                {
                    Weather = home.weather;
                    Temperature = home.temperature;
                    Humidity = home.humidity;
                    Door = home.door?"Open":"Closed";
                }
            } 
        }
        public User user;
        public bool IsLoaded = false;

        #region command
        public ICommand LoadedWindowCommand { get; set; }


        public ICommand LivingRoomCommand { get; set; }
        public ICommand BedRoomCommand { get; set; }
        public ICommand BathRoomCommand { get; set; }
        public ICommand GardenCommand { get; set; }
        public ICommand KitchenCommand { get; set; }

        public ICommand OpenDoorCommand { get; set; }
        
        public ICommand CloseDoorCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            home = new Home();
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    user = loginVM.client;
                    LoadHomeData();
                    p.Show();
                    Task.Run(() =>
                    {
                        mqttClient = new MqttClient("broker.emqx.io");
                        mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                        mqttClient.Subscribe(new string[] { "Application1/Home" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                        mqttClient.Connect("Application2");
                    });
                }
                else
                {
                    p.Close();
                }
            }
              );

            #region active comand

            OpenDoorCommand = new RelayCommand<object>((p) =>
            {
                if(Door == "Open")
                {
                    return false;
                }
                return true;
            },(p)=> {
                
                string url = "home/" + home._id + "/door/open";
                var response = WebAPI.GetCall(url);
                if((int)response.Result.StatusCode == 200)
                {
                    var json = response.Result.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Home>(json);
                    home = data;
                }
                else
                {
                    MessageBox.Show("Update Not Found");
                }
            });
            CloseDoorCommand = new RelayCommand<object>((p) =>
            {
                if (Door == "Closed")
                {
                    return false;
                }
                return true;
            }, (p) => {
                string url = "home/" + home._id + "/door/closed";
                var response = WebAPI.GetCall(url);
                if ((int)response.Result.StatusCode == 200)
                {
                    var json = response.Result.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Home>(json);
                    home = data;
                    Task.Run(() =>
                    {
                        if (mqttClient != null && mqttClient.IsConnected)
                        {
                            mqttClient.Publish("Application2/Home", Encoding.UTF8.GetBytes("a"));
                        }
                    });
                }
                else
                {
                    MessageBox.Show("Update Not Found");
                }
            });

            LivingRoomCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LivingRoomWindow wd = new LivingRoomWindow(); wd.ShowDialog(); });
            BedRoomCommand = new RelayCommand<object>((p) => { return true; }, (p) => { BedRoomWindow wd = new BedRoomWindow(); wd.ShowDialog(); });
            BathRoomCommand = new RelayCommand<object>((p) => { return true; }, (p) => { BathRoomWindow wd = new BathRoomWindow(); wd.ShowDialog(); });
            GardenCommand = new RelayCommand<object>((p) => { return true; }, (p) => { GardenWindow wd = new GardenWindow(); wd.ShowDialog(); });
            KitchenCommand = new RelayCommand<object>((p) => { return true; }, (p) => { KitchenWindow wd = new KitchenWindow(); wd.ShowDialog(); });

            #endregion
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            //application 1
            string message = Encoding.UTF8.GetString(e.Message);
            LoadHomeData();
        }
        void LoadHomeData()
        {
            string urlGetHome = "home/find/" + user._id;
            var response = WebAPI.GetCall(urlGetHome);
            if((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Home>(json);
                home = data;
            }
            else
            {
                MessageBox.Show("Load Data eroor");
            }
        }
    }
}
