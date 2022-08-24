using System;
using System.Collections.Generic;
using AssignmentApp.Helpers;
using AssignmentApp.Models;
using AssignmentApp.ViewModels;
using Xamarin.Forms;

namespace AssignmentApp.Pages
{
    public partial class UserDetailsPage : ContentPage
    {
        private UserDetailsPageVM vm;
        private UserCard userCard;
        public UserDetailsPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as UserDetailsPageVM;
        }

        public UserDetailsPage(UserCard user) : this()
        {
            userCard = user;
            vm.User = Db.GetById<User>(userCard.Id);
        }
    }
}

