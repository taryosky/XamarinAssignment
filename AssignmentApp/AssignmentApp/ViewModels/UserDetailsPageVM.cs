using System.ComponentModel;
using AssignmentApp.Models;
using AssignmentApp.Pages;
using Xamarin.Forms;

namespace AssignmentApp.ViewModels
{
    public class UserDetailsPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public  Command GoToEditUserCommand { get; set; }

        private User _user;
        public User User { get { return _user; } set {
                _user = value;
                UserChanged("User");
            } }

        public UserDetailsPageVM()
        {
            GoToEditUserCommand = new Command(NavigateToEditUser);
        }

        private void UserChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void NavigateToEditUser()
        {
            App.Current.MainPage.Navigation.PushAsync(new EditUserPage(User.Id));
        }
    }
}

