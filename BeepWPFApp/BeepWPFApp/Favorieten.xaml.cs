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
using System.Windows.Shapes;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for Favorieten.xaml
    /// </summary>
    public partial class Favorieten : Window
    {
        public Favorieten()
        {
            InitializeComponent();
            listbox2.ItemsSource = lstPage.lstFav;
        }

        
    }
}
