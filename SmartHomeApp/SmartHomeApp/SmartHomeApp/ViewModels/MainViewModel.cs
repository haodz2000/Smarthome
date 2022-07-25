using OpenNETCF.MQTT;
using SmartHomeApp.Models;
using SmartHomeApp.Views;
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
    public class MainViewModel: BaseViewModel
    {

        MqttClient mqttClient;

        private Home home;
        public User user;
        private string weather;
        public string Weather { get => weather; set { weather = value; OnPropertyChanged(); } }
        private int temperature;
        public int Temperature { get => temperature; set { temperature = value; OnPropertyChanged(); } }
        private string humidity;
        public string Humidity { get => humidity; set { humidity = value; OnPropertyChanged(); } }
        private string door;
        public string Door { get => door; set { door = value; OnPropertyChanged(); } }

        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public Home Home {
            get => home;
            set
            {
                home = value;
                OnPropertyChanged(nameof(Home));
                if (Home != null)
                {
                    Weather = Home.weather;
                    Temperature = Home.temperature;
                    Humidity = Home.humidity +"%";
                    Door = Home.door ? "Open" : "Closed";
                }
            }
        }
        public ICommand OpenDoor { get; set; }
        public ICommand CloseDoor { get; set; }
        public  MainViewModel()
        {
            this.Home = new Home();
            this.User = new User();
            Title = "Home"; 
            if(App.isUserLogin == false)
            {
                GotoLoginPage();
            }
            else
            {
                User = App.User;
                loadData(User._id);
                if(this.Home != null)
                {
                    Task.Run(() =>
                    {
                        mqttClient = new MqttClient("broker.emqx.io");
                        mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                        mqttClient.Subscribe(new string[] { "Application2/Home" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                        mqttClient.Connect("Application1");
                    });
                    OpenDoor = new Command(() => { ChangeDoor(this.Home._id, "open"); });
                    CloseDoor = new Command(() => { ChangeDoor(this.Home._id, "closed");});
                }
              
            }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            //application 2
            string message = Encoding.UTF8.GetString(e.Message);
            loadData(User._id);
        }

        public async void ChangeDoor(string idHome,string status)
        {
            this.Home = await App.HomeManager.GetTasksAsync($"home/{idHome}/door/{status}");
            _ = Task.Run(() =>
              {
                  if (mqttClient != null && mqttClient.IsConnected)
                  {
                      mqttClient.Publish("Application1/Home", Encoding.UTF8.GetBytes("b"));
                  }
              });
        }
        public async void GotoLoginPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        public async void loadData(string idUser)
        {
            this.Home = await App.HomeManager.GetTasksAsync($"home/find/{idUser}");
            App.Home = this.Home;
        }
    }
}
