using AssignmentApp.Models;
using AssignmentApp.ViewModels;
using Xamarin.Forms;

namespace AssignmentApp.Pages
{
    public partial class EditUserPage : ContentPage
    {
        public EditUserPageVM vm;
        long UserId { get; set; }
        public EditUserPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as EditUserPageVM;
        }

        public EditUserPage(long userId): this()
        {
            UserId = userId;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetUser(UserId);
        }
    }
}

