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
    public partial class KitchenPage : ContentPage
    {
        public KitchenPage()
        {
            InitializeComponent();
            this.BindingContext = new KitchenViewModel();
        }
    }
}