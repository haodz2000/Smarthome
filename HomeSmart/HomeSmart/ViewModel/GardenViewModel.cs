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
    public class GardenViewModel: BaseViewModel
    {
        private Garden garden;
        public Garden Garden
        {
            get => garden;
            set
            {
                garden = value;
                OnPropertyChanged();
                if(garden != null)
                {
                    Temp = garden.temperature;
                    Humidity = garden.humidity;
                    WaterPump = garden.waterPump ? "ON" : "OFF";
                }
            }
        }
        private int temp;
        public int Temp { get=>temp; set { temp = value;OnPropertyChanged(); } }
        private int humidity;
        public int Humidity { get => humidity; set { humidity = value; OnPropertyChanged(); } }

        private string waterPump;
        private MqttClient mqttClient;

        public string WaterPump { get => waterPump; set { waterPump = value; OnPropertyChanged(); } }
        public ICommand OpenWaterPump { get; set; }
        public ICommand CloseWaterPump { get; set; }
        public string HomeId;

        public GardenViewModel()
        {
            Garden = new Garden();

            MainWindow mainWindow = new MainWindow();
            if (mainWindow.DataContext == null)
                return;
            var mainVM = mainWindow.DataContext as MainViewModel;
            HomeId = mainVM.home._id;
            LoadData(HomeId);
            if (Garden != null)
            {
                #region connectMQTT
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application1/Garden" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application2");
                });
                #endregion
            }

            OpenWaterPump = new RelayCommand<object>((p) =>
            {
                return !Garden.waterPump;
            }, (p) =>
            {
                ActionWater(garden._id, "on");
            });

            CloseWaterPump = new RelayCommand<object>((p) =>
            {
                return Garden.waterPump;
            }, (p) =>
            {
                ActionWater(garden._id, "off");
            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            LoadData(HomeId);
        }

        void LoadData(string homeId)
        {
            string urlGetHome = "garden/find/" + homeId;
            var response = WebAPI.GetCall(urlGetHome);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Garden>(json);
                Garden = data;
            }
        }
        void ActionWater(string id, string mode)
        {
            string url = "garden/" + id + "/change/" + mode;
            var response = WebAPI.GetCall(url);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Garden>(json);
                Garden = data;
                _ = Task.Run(() =>
                {
                    mqttClient.Publish("Application2/Garden", Encoding.UTF8.GetBytes("a"));
                });
            }
            else
            {
                MessageBox.Show("Update Not Found");
            }
        }
    }
}
