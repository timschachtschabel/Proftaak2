﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeepWPFApp.Classes;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for InlogScherm.xaml
    /// </summary>
    public partial class InlogScherm : Page
    {

        public InlogScherm()
        {
            InitializeComponent();
        }

        private void Login(string naam, string pass) // inlogknop
        {
            api api = new api();
            User loginUser = api.GetUser(naam, pass);
            if (loginUser.Name !=null)
            {
                GlobalSettings.Id = loginUser.Id;
                GlobalSettings.Naam = loginUser.Name;
                GlobalSettings.Email = loginUser.Email;
                GlobalSettings.Pass = loginUser.Password;

                char[] seperator = ".".ToCharArray();

                GlobalSettings.AllergieList = loginUser.allergies.Split(seperator).ToList();
                MainWindow.AppWindow.switchPage(2);
                MainWindow.AppWindow.EnableButtons();

                MainWindow.AppWindow.GetShoppinglist();
            }
            else 
            {
                MessageBox.Show("Kan gebruiker niet vinden", "Error!", MessageBoxButton.OK);
                TxtUser.Clear();
                TxtPass.Clear();
            }


        }


        private void Button_Click(object sender, RoutedEventArgs e) // inlogknop
        {
            Login(TxtUser.Text, TxtPass.Password);
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login(TxtUser.Text, TxtPass.Password);
            }
        }

        private void register_click(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.switchPage(5);
        }
    }
}
