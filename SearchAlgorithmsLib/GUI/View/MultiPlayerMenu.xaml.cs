using GUI.Model;
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

namespace GUI.View {
    /// <summary>
    /// Interaction logic for MultiPlayerMenu.xaml
    /// </summary>
    public partial class MultiPlayerMenu : Window {

        private MultiPlayerViewModel vm;

        public MultiPlayerMenu() {
            InitializeComponent();
            vm = new MultiPlayerViewModel(new MultiPlayerModel());
            this.DataContext = vm;
            //vm.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort); UserControl.TxtMazeName = Properties.Settings.Default.MazeName;
            UserControl.TxtRows = Properties.Settings.Default.MazeRows;
            UserControl.TxtCols = Properties.Settings.Default.MazeCols;
            GameList.ItemsSource = vm.ListOfGames;
        }

        private void btnJoin_Click(object sender, RoutedEventArgs e) {
            vm.GameSelected = GameList.SelectedIndex;/////to check if this is the right func

            MultiPlayerView sp = new MultiPlayerView(vm);
            sp.Show();
            this.Close();
            vm.Join(vm.MazeName);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e) {
            vm.MazeName = UserControl.TxtMazeName;
            vm.Rows = UserControl.TxtRows;
            vm.Cols = UserControl.TxtCols;

            MultiPlayerView sp = new MultiPlayerView(vm);
            sp.Show();
            this.Close();
            vm.Start(vm.MazeName, vm.Rows, vm.Cols);
        }

        public void DropDown(Object sender, Object args) {
            vm.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort); UserControl.TxtMazeName = Properties.Settings.Default.MazeName;
            vm.AskListOfGames();
        }
    }
}
