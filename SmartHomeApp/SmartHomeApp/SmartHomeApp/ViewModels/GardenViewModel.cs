using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Xamarin.Forms;

namespace SmartHomeApp.ViewModels
{
    public class GardenViewModel: BaseViewModel
    {
        MqttClient mqttClient;
        
        private Garden room;
        private string humidity;
        private int temperature;
        private string waterPump;

        public Garden Room
        {
            get => room;
            set
            {
                room = value;
                OnPropertyChanged(nameof(Room));
                if (room != null)
                {
                    Humidity = room.humidity+"%";
                    Temperature = room.temperature;
                    WaterPump = room.waterPump ? "Bật" : "Tắt";
                }
            }
        }
        public string Humidity
        {
            get => humidity;
            set
            {
                humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }
        public int Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }
        public string WaterPump
        {
            get => waterPump;
            set
            {
                waterPump = value;
                OnPropertyChanged(nameof(WaterPump));
            }
        }
        public ICommand OpenWaterPumpCommand { get; set; }
        public ICommand CloseWaterPumpCommand { get; set; }
        public GardenViewModel()
        {
            this.Room = new Garden();
            LoadData(App.Home._id);
            if (Room != null)
            {
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application2/Garden" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application1");
                });
            }
            OpenWaterPumpCommand = new Command(() => { Change(Room._id, "on"); });
            CloseWaterPumpCommand = new Command(() => { Change(Room._id, "off"); });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            LoadData(App.Home._id);
        }

        public async void LoadData(string homeId)
        {
            string urlRoom = $"garden/find/{homeId}";

            this.Room = await App.GardenManager.GetTasksAsync(urlRoom);
        }
        public async void Change(string idRoom,  string action)
        {
            string url = $"garden/{idRoom}/change/{action}";
            this.Room = await App.GardenManager.GetTasksAsync(url);
            _ = Task.Run(() =>
            {
                mqttClient.Publish("Application1/Garden", Encoding.UTF8.GetBytes("a"));
            });
        }
    }
}
