using System.ComponentModel;
using AssignmentApp.Models;

namespace AssignmentApp.ViewModels
{
    public class UserDetailsPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private User _user;
        public User User { get { return _user; } set {
                _user = value;
                UserChanged("User");
            } }

        public UserDetailsPageVM()
        {

        }

        private void UserChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}

