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
using System.Windows.Shapes;

namespace GUI.View {
    /// <summary>
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window {
        public SinglePlayerMenu() {
            InitializeComponent();
        }

        private void definePlay_Loaded(object sender, RoutedEventArgs e) {
            SinglePlayerView sp = new SinglePlayerView();
            //sp = (SinglePlayerView)Application.Current.MainWindow;
            sp.Show();
            this.Close();
        }
    }
}
