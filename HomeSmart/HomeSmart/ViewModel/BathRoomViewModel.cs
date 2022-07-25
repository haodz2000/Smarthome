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
    public class BathRoomViewModel: BaseViewModel
    {
        private BathRoom bathRoom;
        public BathRoom BathRoom
        {
            get => bathRoom;
            set
            {
                bathRoom = value;
                OnPropertyChanged();
                if(bathRoom != null)
                {
                    Lamp = bathRoom.lamp ? "ON" : "OFF";
                    Heater = bathRoom.heater ? "ON" : "OFF";
                }
            }
        }
        private string lamp;
        public string Lamp { get=>lamp; set { lamp = value; OnPropertyChanged(); } }

        private string heater;
        public string Heater { get => heater; set { heater = value; OnPropertyChanged(); } }

        public ICommand OpenLampCommand { get; set; }
        public ICommand CloseLampCommand { get; set; }
        public ICommand OpenHeaterCommand { get; set; }
        public ICommand CloseHeaterCommand { get; set; }
        public string HomeId;
        private MqttClient mqttClient;

        public BathRoomViewModel()
        {
            BathRoom = new BathRoom();
            MainWindow mainWindow = new MainWindow();
            if (mainWindow.DataContext == null)
                return;
            var mainVM = mainWindow.DataContext as MainViewModel;
            HomeId = mainVM.home._id;
            LoadData(HomeId);
            if(BathRoom != null)
            {
                #region connectMQTT
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application1/BathRoom" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application2");
                });
                #endregion
            }

            OpenHeaterCommand = new RelayCommand<object>((p) =>
            {
                return !bathRoom.heater;
            }, (p) =>
            {
                ActionBathRoom(bathRoom._id, "heater", "on");
            });
            CloseHeaterCommand = new RelayCommand<object>((p) =>
            {
                return bathRoom.heater;
            }, (p) =>
            {
                ActionBathRoom(bathRoom._id, "heater", "off");
            });
            OpenLampCommand = new RelayCommand<object>((p) =>
            {
                return !bathRoom.lamp;
            }, (p) =>
            {
                ActionBathRoom(bathRoom._id, "lamp", "on");
            });
            CloseLampCommand = new RelayCommand<object>((p) =>
            {
                return bathRoom.lamp;
            }, (p) =>
            {
                ActionBathRoom(bathRoom._id, "lamp", "off");
            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            LoadData(HomeId);
        }

        void LoadData(string homeId)
        {
            string urlGetHome = "bathroom/find/" + homeId;
            var response = WebAPI.GetCall(urlGetHome);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BathRoom>(json);
                BathRoom = data;
            }
        }
        void ActionBathRoom(string id, string device, string mode)
        {
            string url = "bathroom/" + id + "/change/" + device + "/" + mode;
            var response = WebAPI.GetCall(url);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BathRoom>(json);
                BathRoom = data;
                _ = Task.Run(() =>
                {
                    mqttClient.Publish("Application2/BathRoom", Encoding.UTF8.GetBytes("a"));
                });
            }
            else
            {
                MessageBox.Show("Update Not Found");
            }
        }
    }
}
