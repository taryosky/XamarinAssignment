using System.Collections.ObjectModel;
using AssignmentApp.Helpers;
using AssignmentApp.Models;
using AssignmentApp.Pages;
using Xamarin.Forms;

namespace AssignmentApp.ViewModels
{
    public class MainPageVM
    {
        private UserCard _selectedCard;

        public Command GotToNewUserPageCommand { get; set; }
        public Command ItemSelectedCommand { get; set; }
        public ObservableCollection<UserCard> Users { get; set; }

        public UserCard SelectedCard { get { return _selectedCard; } set
            {
                _selectedCard = value;
                if (_selectedCard != null)
                    App.Current.MainPage.Navigation.PushAsync(new UserDetailsPage(_selectedCard));
            } }


        public MainPageVM()
        {
            GotToNewUserPageCommand = new Command(NavigateToNewUserPage);
            Users = new ObservableCollection<UserCard>();
        }

        async void NavigateToNewUserPage()
        {
            //App.Current.MainPage.DisplayAlert("hello", "Hello", "Ok");
            App.Current.MainPage.Navigation.PushAsync(new AddNewUserPage());
        }

        public void GetUsers()
        {
            Users.Clear();
            var users = Db.GetAll<User>();
            foreach(var user in users)
            {
                var c = new UserCard
                {
                    Id = user.Id,
                    Name = user.Name,
                    Address = user.Address,
                    Photo = user.ProfilePic
                };
                Users.Add(c);
            }
        }

        private void ItemSelected()
        {

        }
    }
}

