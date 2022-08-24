using System;
using System.ComponentModel;
using AssignmentApp.Models;
using AssignmentApp.Helpers;
using Xamarin.Essentials;
using AssignmentApp.Pages;

namespace AssignmentApp.ViewModels
{
    public class NewUserPageVM : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _phoneNumber;
        private string _sex;

        private User _user;

        public Xamarin.Forms.Command AddNewUserCommand { get; set; }
        public Xamarin.Forms.Command SelectImageCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public User User { get { return _user; }
            set {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        public string Name { get { return _name; }
            set {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public FileResult _profilePic;
        public FileResult ProfilePic { get { return _profilePic; } set {
                _profilePic = value;
                OnPropertyChanged("ProfilePic");
            }
        }

        public NewUserPageVM()
        {
            AddNewUserCommand = new Xamarin.Forms.Command(AddNewUser);
            SelectImageCommand = new Xamarin.Forms.Command(ImageSelected);
            User = new User();
        }


        private async void AddNewUser()
        {
            if (!CanAddNewUser())
            {
                App.Current.MainPage.DisplayAlert("Error", "Please fill all fields currectly", "Ok");
                return;
            }

            var upload = await CloudinaryService.Upload(ProfilePic);
            if(upload.Item1 == null)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured, please try again", "Ok");
                return;
            }

            var user = new User
            {
                Name = Name,
                Sex = Sex,
                Address = Address,
                DateCreted = DateTime.Now,
                DateUpdated = DateTime.MinValue,
                Mobile = PhoneNumber,
                ProfilePic = upload.Item1,
                ImagePublicUrl = upload.Item2
            };

            var isCreated = Db.Insert<User>(user);
            if (isCreated)
                App.Current.MainPage.Navigation.PushAsync(new MainPage());
            else
                App.Current.MainPage.DisplayAlert("Error", "An error occured, please try again", "Ok");
        }

        private bool CanAddNewUser()
        {
            bool emptyName = string.IsNullOrWhiteSpace(Name);
            bool emptyAddress = string.IsNullOrWhiteSpace(Address);
            bool emptyPhone = string.IsNullOrWhiteSpace(PhoneNumber);
            bool emptySex = string.IsNullOrWhiteSpace(Sex);

            return !emptyName && !emptyAddress && !emptyPhone && ProfilePic != null && !emptySex;
        }

        private async void ImageSelected()
        {
            FileResult imgPicker = await MediaPicker.PickPhotoAsync();
            if (imgPicker == null)
                return;

            ProfilePic = imgPicker;
        }

        private bool ValidateFields()
        {
            return false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

