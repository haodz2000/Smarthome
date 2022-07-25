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
    public class KitchenViewModel:BaseViewModel
    {
        MqttClient mqttClient;

        private Kitchen room;
        private string lamp;
        private string waterPump;

        public Kitchen Room
        {
            get => room;
            set
            {
                room = value;
                OnPropertyChanged(nameof(Room));
                if (room != null)
                {
                    Lamp = room.lamp ? "Bật" : "Tắt";
                    WaterPump = room.waterPump ? "Bật" : "Tắt";
                }
            }
        }
        public string Lamp
        {
            get => lamp;
            set
            {
                lamp = value;
                OnPropertyChanged(nameof(Lamp));
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
        public ICommand OpenLampCommand { get; set; }
        public ICommand CloseLampCommand { get; set; }
        public ICommand OpenWaterPumpCommand { get; set; }
        public ICommand CloseWaterPumpCommand { get; set; }
        public KitchenViewModel()
        {
            this.Room = new Kitchen();
            LoadData(App.Home._id);
            if(Room != null)
            {
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application2/Kitchen" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application1");
                });
            }
            OpenLampCommand = new Command(() => { Change(Room._id, "lamp", "on"); });
            CloseLampCommand = new Command(() => { Change(Room._id, "lamp", "off"); });
            OpenWaterPumpCommand = new Command(() => { Change(Room._id, "waterPump", "on"); });
            CloseWaterPumpCommand = new Command(() => { Change(Room._id, "waterPump", "off"); });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            //application 2
            string message = Encoding.UTF8.GetString(e.Message);
            LoadData(App.Home._id);
        }
        public async void LoadData(string homeId)
        {
            string urlRoom = $"kitchen/find/{homeId}";

            this.Room = await App.KitchenManager.GetTasksAsync(urlRoom);
        }
        public async void Change(string idRoom, string device, string action)
        {
            string url = $"kitchen/{idRoom}/change/{device}/{action}";
            this.Room = await App.KitchenManager.GetTasksAsync(url);
            _ = Task.Run(() =>
            {
                mqttClient.Publish("Application1/Kitchen", Encoding.UTF8.GetBytes("a"));
            });
        }
    }
}
