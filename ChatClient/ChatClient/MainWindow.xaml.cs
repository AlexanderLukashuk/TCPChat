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

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendName(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text != string.Empty && nameTextBox.Text != " ")
            {
                var window = new ClientChatWindow(nameTextBox.Text);
                App.Current.MainWindow = window;
                this.Close();
                window.Show();
            }
            else
            {
                MessageBox.Show("Ошибка ввода");
            }
        }
    }
}
