using SmartHomeApp.Models;
using SmartHomeApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHomeApp
{
    public partial class App : Application
    {
        public static HomeManager HomeManager { get; private set; }
        public static AirManager AirManager { get; private set; }
        public static KitchenManager KitchenManager { get; private set; }
        public static LivingRoomManager LivingRoomManager { get; private set; }
        public static BedRoomManager BedRoomManager { get; private set; }
        public static BathRoomManager BathRoomManager { get; private set; }
        public static GardenManager GardenManager { get; private set; }
        
        public static UserManager UserManager { get; private set; }

        public static bool isUserLogin { get; set; }
        public static User User { get; set; }
        public static Home Home { get; set; }
        public App()
        {
            InitializeComponent();

            HomeManager = new HomeManager(new HomeService());
            AirManager = new AirManager(new AirService());
            LivingRoomManager = new LivingRoomManager(new LivingRoomService());
            BathRoomManager = new BathRoomManager(new BathRoomService());
            BedRoomManager = new BedRoomManager(new BedRoomService());
            GardenManager = new GardenManager(new GardenService());
            KitchenManager = new KitchenManager(new KitchenService());
            UserManager = new UserManager(new UserService());
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
