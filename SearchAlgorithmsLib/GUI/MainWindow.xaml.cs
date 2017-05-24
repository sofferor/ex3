using GUI.View;
using System;
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

namespace GUI
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

        private void Button_Click_Settings(object sender, RoutedEventArgs e)
        {
            SettingsWindow w = new SettingsWindow();
            w.Show();
            this.Close();
        }

        private void Button_Click_Multi_Player(object sender, RoutedEventArgs e)
        {
            MultiPlayerMenu w = new MultiPlayerMenu();
            w.Show();
            this.Close();
        }

        private void Button_Click_Single_Player(object sender, RoutedEventArgs e) {
            SinglePlayerMenu w = new SinglePlayerMenu();
            w.Show();
            this.Hide();
        }
    }
}
