using System;
using AssignmentApp.Helpers;
using AssignmentApp.Models;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AssignmentApp.ViewModels
{
    public class EditUserPageVM : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _phoneNumber;
        private string _sex;
        private ImageSource _photo;

        private User _user;


        public FileResult _profilePic;

        public Xamarin.Forms.Command EditUserCommand { get; set; }
        public Xamarin.Forms.Command SelectImageCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ImageSource Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
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

        public FileResult ProfilePic
        {
            get { return _profilePic; }
            set
            {
                _profilePic = value;
                Photo = _profilePic.FullPath;
                OnPropertyChanged("ProfilePic");
            }
        }

        public EditUserPageVM()
        {
            EditUserCommand = new Xamarin.Forms.Command(EditUserDetails, CanUpdateUser);
            SelectImageCommand = new Xamarin.Forms.Command(ImageSelected);
            User = new User();
            PropertyChanged += (_, __) => EditUserCommand.ChangeCanExecute();
        }


        private async void EditUserDetails()
        {
            if(ProfilePic != null)
            {
                var upload = await CloudinaryService.Upload(ProfilePic, User.PublicId);
                if (upload.Item1 == null)
                {
                    App.Current.MainPage.DisplayAlert("Error", "An error occured, please try again", "Ok");
                    return;
                }

                User.ProfilePic = upload.Item1;
                User.PublicId = upload.Item2;
            }

            User.Name = Name;
            User.Address = Address;
            User.Sex = Sex;
            User.Mobile = PhoneNumber;
            User.DateUpdated = DateTime.Now;

            var isUpdated = Db.Update<User>(User);
            if (isUpdated)
                App.Current.MainPage.Navigation.PushAsync(new MainPage());
            else
                App.Current.MainPage.DisplayAlert("Error", "An error occured, please try again", "Ok");
        }


        private bool CanUpdateUser()
        {
            bool emptyName = string.IsNullOrWhiteSpace(Name);
            bool emptyAddress = string.IsNullOrWhiteSpace(Address);
            bool emptyPhone = string.IsNullOrWhiteSpace(PhoneNumber);
            bool emptySex = string.IsNullOrWhiteSpace(Sex);

            var validResult = !emptyName && !emptyAddress && !emptyPhone && !emptySex;
            if (validResult)
                return true;

            return false;
        }

        private async void ImageSelected()
        {
            var fromGallery = await App.Current.MainPage.DisplayAlert("Choose Photo", "Select photo to upload", "Gallery", "Camera");
            if (fromGallery)
            {
                FileResult imgPicker = await MediaPicker.PickPhotoAsync();
                if (imgPicker == null)
                    return;

                ProfilePic = imgPicker;
                Photo = ImageSource.FromStream(() => imgPicker.OpenReadAsync().GetAwaiter().GetResult());

            }
            else
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Not supportted", "Your device does not support camera", "Ok");
                    return;
                }

                FileResult imgCapure = await MediaPicker.CapturePhotoAsync();
                if (imgCapure == null)
                    return;

                ProfilePic = imgCapure;
                Photo = ImageSource.FromStream(() => imgCapure.OpenReadAsync().GetAwaiter().GetResult());
            }
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetUser(long userId)
        {
            var user = Db.GetById<User>(userId);
            User = user;

            Name = user.Name;
            Address = user.Address;
            PhoneNumber = user.Mobile;
            Sex = user.Sex;
            Photo = ImageSource.FromUri(new Uri(user.ProfilePic));
        }
    }
}

