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
    public class BathRoomViewModel: BaseViewModel
    {
        MqttClient mqttClient;
        private BathRoom room;
        private string lamp;
        private string heater;
        private Boolean isEnable;

        public BathRoom Room
        {
            get => room;
            set
            {
                room = value;
                OnPropertyChanged(nameof(Room));
                if (room != null)
                {
                    Lamp = room.lamp ? "Bật" : "Tắt";
                    Heater = room.heater? "Bật" : "Tắt";
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
        public string Heater
        {
            get => heater;
            set
            {
                heater = value;
                OnPropertyChanged(nameof(Heater));
            }
        }
        public Boolean IsEnable
        {
            get => isEnable;
            set
            {
                isEnable = value;
                OnPropertyChanged(nameof(IsEnable));
            }
        }
        public ICommand OpenLampCommand { get; set; }
        public ICommand CloseLampCommand { get; set; }
        public ICommand OpenHeaterCommand { get; set; }
        public ICommand CloseHeaterCommand { get; set; }
        public BathRoomViewModel()
        {
            this.Room = new BathRoom();
            LoadData(App.Home._id);
            if(Room != null)
            {
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application2/BathRoom" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application1");
                });
            }
            OpenLampCommand = new Command(() => { Change(Room._id,"lamp","on"); });
            CloseLampCommand = new Command(() => { Change(Room._id, "lamp", "off"); });
            OpenHeaterCommand = new Command(() => { Change(Room._id, "heater", "on"); });
            CloseHeaterCommand = new Command(() => { Change(Room._id, "heater", "off"); });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            LoadData(App.Home._id);
        }

        public async void LoadData(string homeId)
        {
            string urlRoom = $"bathroom/find/{homeId}";

            this.Room = await App.BathRoomManager.GetTasksAsync(urlRoom);
        }
        public async void Change(string idRoom, string device ,string action)
        {
            string url = $"bathroom/{idRoom}/change/{device}/{action}";
            this.Room = await App.BathRoomManager.GetTasksAsync(url);
            _ = Task.Run(() =>
            {
                mqttClient.Publish("Application1/BathRoom", Encoding.UTF8.GetBytes("a"));
            });
        }
    }
}
