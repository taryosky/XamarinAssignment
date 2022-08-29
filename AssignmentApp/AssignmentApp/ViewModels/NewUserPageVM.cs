using System;
using System.ComponentModel;
using AssignmentApp.Models;
using AssignmentApp.Helpers;
using Xamarin.Essentials;
using AssignmentApp.Pages;
using Xamarin.Forms;

namespace AssignmentApp.ViewModels
{
    public class NewUserPageVM : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _phoneNumber;
        private string _sex;
        private ImageSource _photo;
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

        public ImageSource Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
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
            AddNewUserCommand = new Xamarin.Forms.Command(AddNewUser, CanAddNewUser);
            SelectImageCommand = new Xamarin.Forms.Command(ImageSelected);
            User = new User();
            PropertyChanged += (_, __) => AddNewUserCommand.ChangeCanExecute();
        }


        private async void AddNewUser()
        {
            (string, string) uploadResult = (null, null);

            if(ProfilePic != null)
            {
                uploadResult = await CloudinaryService.Upload(ProfilePic);
                if (uploadResult.Item1 == null)
                {
                    App.Current.MainPage.DisplayAlert("Error", "An error occured, please try again", "Ok");
                    return;
                }
            }
            

            var user = new User
            {
                Name = Name,
                Sex = Sex,
                Address = Address,
                DateCreted = DateTime.Now,
                DateUpdated = DateTime.MinValue,
                Mobile = PhoneNumber,
                ProfilePic = uploadResult.Item1,
                PublicId = uploadResult.Item2
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

            return !emptyName && !emptyAddress && !emptyPhone && !emptySex;
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

