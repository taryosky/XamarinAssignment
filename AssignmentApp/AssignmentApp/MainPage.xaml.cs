using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AssignmentApp.Pages;
using System.Windows.Input;
using AssignmentApp.ViewModels;

namespace AssignmentApp
{
    public partial class MainPage : ContentPage
    {
        private MainPageVM vm;

        public MainPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as MainPageVM;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetUsers();
        }

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
        }
    }
}

