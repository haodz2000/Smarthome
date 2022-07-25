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
    public class BedRoomViewModel:BaseViewModel
    {
        MqttClient mqttClient;

        private AirConditioning airConditioning;
        private BedRoom room;
        private int temperature;
        private string mode;
        private string active;
        private string lamp;
        private Boolean isEnable;

        public AirConditioning AirConditioning
        {
            get => airConditioning;
            set
            {
                airConditioning = value;
                OnPropertyChanged(nameof(AirConditioning));
                if (airConditioning != null)
                {
                    IsEnable = airConditioning.active;
                    Temperature = airConditioning.temperature;
                    Active = airConditioning.active ? "Bật" : "Tắt";
                    switch (airConditioning.mode)
                    {
                        case 0:
                            {
                                Mode = "Auto";
                                break;
                            }
                        case 1:
                            {
                                Mode = "Heat";
                                break;
                            }
                        case 2:
                            {
                                Mode = "Cool";
                                break;
                            }
                        case 3:
                            {
                                Mode = "Dry";
                                break;
                            }
                        default: { break; }
                    }
                }
            }
        }
        public BedRoom Room
        {
            get => room;
            set
            {
                room = value;
                OnPropertyChanged(nameof(Room));
                if (room != null)
                {
                    Lamp = room.lamp ? "Bật" : "Tắt";
                }
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
        public string Mode
        {
            get => mode;
            set
            {
                mode = value;
                OnPropertyChanged(nameof(Mode));
            }
        }
        public string Active
        {
            get => active;
            set
            {
                active = value;
                OnPropertyChanged(nameof(Active));
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

        public Boolean IsEnable
        {
            get => isEnable;
            set
            {
                isEnable = value;
                OnPropertyChanged(nameof(IsEnable));
            }
        }
        public ICommand OpenAirCommand { get; set; }
        public ICommand CloseAirCommand { get; set; }
        public ICommand RaiseAirCommand { get; set; }
        public ICommand DescAirCommand { get; set; }
        public ICommand ModeAutoCommand { get; set; }
        public ICommand ModeHeatCommand { get; set; }
        public ICommand ModeCoolCommand { get; set; }
        public ICommand ModeDryCommand { get; set; }
        public ICommand OpenLampCommand { get; set; }
        public ICommand CloseLampCommand { get; set; }
        public BedRoomViewModel()
        {
            this.AirConditioning = new AirConditioning();
            this.Room = new BedRoom();
            LoadData(App.Home._id);
            if(Room != null)
            {
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application2/BedRoom" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application1");
                });
            }
            OpenAirCommand = new Command(() => { ChangeMode(AirConditioning._id, "on"); });
            CloseAirCommand = new Command(() => { ChangeMode(AirConditioning._id, "off"); });
            RaiseAirCommand = new Command(() => { ChangeMode(AirConditioning._id, "raise"); });
            DescAirCommand = new Command(() => { ChangeMode(AirConditioning._id, "desc"); });
            ModeAutoCommand = new Command(() => { ChangeMode(AirConditioning._id, "auto"); });
            ModeHeatCommand = new Command(() => { ChangeMode(AirConditioning._id, "heat"); });
            ModeCoolCommand = new Command(() => { ChangeMode(AirConditioning._id, "cool"); });
            ModeDryCommand = new Command(() => { ChangeMode(AirConditioning._id, "dry"); });
            OpenLampCommand = new Command(() => { ChangeLamp(Room._id, "on"); });
            CloseLampCommand = new Command(() => { ChangeLamp(Room._id, "off"); });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            LoadData(App.Home._id);
        }

        public async void LoadData(string homeId)
        {
            string urlRoom = $"bedroom/find/{homeId}";

            this.Room = await App.BedRoomManager.GetTasksAsync(urlRoom);
            string urlAir = $"air/find/{Room.airConditioningId}";
            this.AirConditioning = await App.AirManager.GetTasksAsync(urlAir);
        }
        public async void ChangeMode(string idDevice, string action)
        {
            string url = $"air/change/{idDevice}/{action}";
            this.AirConditioning = await App.AirManager.GetTasksAsync(url);
            _ = Task.Run(() =>
            {
                mqttClient.Publish("Application1/BedRoom", Encoding.UTF8.GetBytes("a"));
            });
        }
        public async void ChangeLamp(string idRoom, string action)
        {
            string url = $"bedroom/{idRoom}/lamp/{action}";
            this.Room = await App.BedRoomManager.GetTasksAsync(url);
            _ = Task.Run(() =>
            {
                mqttClient.Publish("Application1/BedRoom", Encoding.UTF8.GetBytes("a"));
            });
        }
    }
}
