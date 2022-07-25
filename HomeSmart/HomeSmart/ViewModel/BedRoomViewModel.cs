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
    public class BedRoomViewModel: BaseViewModel
    {
        private BedRoom room;
        private string lamp;
        public string Lamp { get => lamp; set { lamp = value; OnPropertyChanged(); } }

        private string AirConditioningId;
        private string activeAir;
        public string ActiveAir { get => activeAir; set { activeAir = value; OnPropertyChanged(); } }
        private string mode;
        public string Mode { get => mode; set { mode = value; OnPropertyChanged(); } }
        private string temp;
        public string Temp { get => temp; set { temp = value; OnPropertyChanged(); } }

        private AirConditioning airConditioning;
        private MqttClient mqttClient;

        public AirConditioning AirConditioning
        {
            get => airConditioning;
            set
            {
                airConditioning = value;
                OnPropertyChanged();
                if (airConditioning != null)
                {
                    ActiveAir = airConditioning.active ? "Bật" : "Tắt";
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
                    }
                    Temp = airConditioning.temperature + "C";
                }
            }
        }
        public BedRoom Room
        {
            get => room;
            set
            {
                room = value;
                OnPropertyChanged();
                if (room != null)
                {
                    Lamp = room.lamp ? "Bật" : "Tắt";
                }
            }
        }
        public ICommand OpenAirCommand { get; set; }


        public ICommand CloseAirCommand { get; set; }
        public ICommand RaiseTempCommand { get; set; }
        public ICommand DescTempCommand { get; set; }
        public ICommand AutoCommand { get; set; }
        public ICommand HeatCommand { get; set; }

        public ICommand DryCommand { get; set; }

        public ICommand CoolCommand { get; set; }
        public ICommand OpenLampCommand { get; set; }

        public ICommand CloseLampCommand { get; set; }
        public string HomeId { get; }

        public BedRoomViewModel()
        {
            Room = new BedRoom();
            AirConditioning = new AirConditioning();
            MainWindow mainWindow = new MainWindow();
            if (mainWindow.DataContext == null)
                return;
            var mainVM = mainWindow.DataContext as MainViewModel;
            HomeId = mainVM.home._id;
            LoadDataRoom(HomeId);
            if(Room != null && AirConditioning != null)
            {
                #region connectMQTT
                Task.Run(() =>
                {
                    mqttClient = new MqttClient("broker.emqx.io");
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    mqttClient.Subscribe(new string[] { "Application1/BedRoom" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    mqttClient.Connect("Application2");
                });
                #endregion
            }

            OpenAirCommand = new RelayCommand<object>((p) =>
            {
                return !airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "on");
            });
            CloseAirCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "off");
            });
            RaiseTempCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "raise");
            });
            DescTempCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "desc");
            });
            AutoCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "auto");
            });
            HeatCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "heat");
            });
            CoolCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "cool");
            });
            DryCommand = new RelayCommand<object>((p) =>
            {
                return airConditioning.active;
            }, (p) =>
            {
                updateAirCondition(airConditioning._id, "dry");
            });
            OpenLampCommand = new RelayCommand<object>((p) =>
            {
                return !room.lamp;
            }, (p) =>
            {
                changeLamp(room._id, "on");
            });
            CloseLampCommand = new RelayCommand<object>((p) =>
            {
                return room.lamp;
            }, (p) =>
            {
                changeLamp(room._id, "off");
            });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            LoadDataRoom(HomeId);
        }

        void LoadDataRoom(string homeId)
        {
            string urlGetHome = "bedroom/find/" + homeId;
            var response = WebAPI.GetCall(urlGetHome);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BedRoom>(json);
                Room = data;
                LoadAirConditioning(Room.airConditioningId);
            }
        }
        void LoadAirConditioning(string AirConditionId)
        {
            string urlGetData = "air/find/" + AirConditionId;
            var response = WebAPI.GetCall(urlGetData);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<AirConditioning>(json);
                AirConditioning = data;

            }

        }
        void updateAirCondition(string id, string type)
        {
            string url = "air/change/" + id + "/" + type;
            var response = WebAPI.GetCall(url);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<AirConditioning>(json);
                AirConditioning = data;
                _ = Task.Run(() =>
                {
                    mqttClient.Publish("Application2/BedRoom", Encoding.UTF8.GetBytes("a"));
                });
            }
            else
            {
                MessageBox.Show("Update Not Found");
            }
        }
        void changeLamp(string idRoom, string status)
        {
            string url = "livingroom/" + idRoom + "/lamp/" + status;
            var response = WebAPI.GetCall(url);
            if ((int)response.Result.StatusCode == 200)
            {
                var json = response.Result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<BedRoom>(json);
                Room = data;
                _ = Task.Run(() =>
                {
                    mqttClient.Publish("Application2/BedRoom", Encoding.UTF8.GetBytes("a"));
                });
            }
            else
            {
                MessageBox.Show("Update Not Found");
            }
        }
    }
}
