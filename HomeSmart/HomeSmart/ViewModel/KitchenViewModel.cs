using HomeSmart.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace HomeSmart.ViewModel
{
    public class KitchenViewModel: BaseViewModel
    {
        private Kitchen kitchen;
        public Kitchen Kitchen
        {
            get => kitchen;
            set
            {
                kitchen = value;
                OnPropertyChanged();
                if(kitchen != null)
                {
                    Lamp = kitchen.lamp ? "ON" : "OFF";
                    WaterPump = kitchen.waterPump ? "ON" : "OFF";
                }
            }
        }
        private string lamp;
        public string Lamp { get=>lamp; set { lamp = value; OnPropertyChanged(); }  }

        private string waterPump;
        private MqttClient mqttClient;
        public string HomeId;

        public string WaterPump { get => waterPump; set { waterPump = value; OnPropertyChanged(); } }

        public ICommand OpenWaterCommand { get; set; }
        public ICommand CloseWaterCommand { get; set; }
        public ICommand OpenLampCommand { get; set; }
        public ICommand CloseLampCommand { get; set; }

        public KitchenViewModel()
        {
            Kitchen = new Kitchen();
            Lamp = "";
            WaterPump = "";
            MainWindow mainWindow = new MainWindow();
            if (mainWindow.DataContext == null)
                return;
            var mainVM = mainWindow.DataContext as MainViewModel;
            HomeId = mainVM.home._id;
            LoadDataRoom(HomeId);
            if (Kitchen != null)
            {
                #region connectMQTT
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application1/Kitchen" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application2");
                });
                #endregion
            }

            OpenWaterCommand = new RelayCommand<object>((p) =>
            {
                return !kitchen.waterPump;
            }, (p) =>
            {
                UpdateDevice(kitchen._id, "waterPump", "on");

            });
            CloseWaterCommand = new RelayCommand<object>((p) =>
            {
                return kitchen.waterPump;
            }, (p) =>
            {
                UpdateDevice(kitchen._id, "waterPump", "off");

            });

            OpenLampCommand = new RelayCommand<object>((p) =>
            {
                return !kitchen.lamp;
            }, (p) =>
            {
                UpdateDevice(kitchen._id,"lamp", "on");

            });
            CloseLampCommand = new RelayCommand<object>((p) =>
            {
                return kitchen.lamp;
            }, (p) =>
            {
                UpdateDevice(kitchen._id, "lamp", "off");

            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            LoadDataRoom(HomeId);
        }

        void LoadDataRoom(string homeId)
        {
            string urlGetHome = "kitchen/find/" + homeId;
            var response = WebAPI.GetCall(urlGetHome);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Kitchen>(json);
                Kitchen = data;
            }
        }

        void UpdateDevice(string roomId, string device, string mode)
        {
            string url = "kitchen/"+roomId+"/change/" + device + "/" + mode;
            var response = WebAPI.GetCall(url);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Kitchen>(json);
                Kitchen = data;
                _ = Task.Run(() =>
                {
                    mqttClient.Publish("Application2/Kitchen", Encoding.UTF8.GetBytes("a"));
                });
            }
            else
            {
                MessageBox.Show("Update Not Found");
            }
        }
    }
}
