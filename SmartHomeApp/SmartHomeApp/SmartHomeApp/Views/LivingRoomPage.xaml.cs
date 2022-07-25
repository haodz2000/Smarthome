using SmartHomeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHomeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LivingRoomPage : ContentPage
    {
        public LivingRoomPage()
        {
            InitializeComponent();
            this.BindingContext = new LivingRoomViewModel();
        }
    }
}