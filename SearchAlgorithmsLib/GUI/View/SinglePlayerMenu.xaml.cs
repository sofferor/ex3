using GUI.ViewModel;
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
using GUI.Model;

namespace GUI.View {
    /// <summary>
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window {

        private SinglePlayerViewModel vm;

        public SinglePlayerMenu() {
            InitializeComponent();
            vm = new SinglePlayerViewModel(new SinglePlayerModel());
            this.DataContext = vm;
            UserControl.TxtMazeName = Properties.Settings.Default.MazeName;
            UserControl.TxtRows = Properties.Settings.Default.MazeRows;
            UserControl.TxtCols = Properties.Settings.Default.MazeCols;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e) {
            vm.MazeName = UserControl.TxtMazeName;
            vm.Rows = UserControl.TxtRows;
            vm.Cols = UserControl.TxtCols;

            SinglePlayerView sp = new SinglePlayerView(vm);
            sp.Show();
            this.Close();
            vm.GenerateMaze(vm.MazeName, vm.Rows, vm.Cols);
        }
    }
}
