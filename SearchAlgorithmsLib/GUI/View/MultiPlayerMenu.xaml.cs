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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerMenu : Window {

        /// <summary>
        /// The vm
        /// </summary>
        private MultiPlayerViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerMenu"/> class.
        /// </summary>
        public MultiPlayerMenu() {
            InitializeComponent();
            vm = new MultiPlayerViewModel(new MultiPlayerModel());
            this.DataContext = vm;
            UserControl.TxtRows = Properties.Settings.Default.MazeRows;
            UserControl.TxtCols = Properties.Settings.Default.MazeCols;
            GameList.ItemsSource = vm.ListOfGames;
        }

        /// <summary>
        /// Handles the Click event of the btnJoin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnJoin_Click(object sender, RoutedEventArgs e) {
            try {
                vm.GameSelected = GameList.SelectedIndex;
            }
            catch (Exception exception) {
                MessageBox.Show(this, "please choose game", "choose game");
                return;
            }

            MultiPlayerView sp = new MultiPlayerView(vm);
            sp.Show();
            this.Close();
            vm.Join(vm.MazeName);
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, RoutedEventArgs e) {
            vm.MazeName = UserControl.TxtMazeName;
            vm.Rows = UserControl.TxtRows;
            vm.Cols = UserControl.TxtCols;

            //creating the waiting window.
            WaitingWindow ww = new WaitingWindow();
            ww.Show();
            this.Close();

            MultiPlayerView sp = new MultiPlayerView(vm);
            sp.Show();
            sp.Hide();
            vm.Start(vm.MazeName, vm.Rows, vm.Cols);
            sp.Show();
            ww.Close();
        }

        /// <summary>
        /// Drops down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        public void DropDown(Object sender, Object args) {
            vm.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort); UserControl.TxtMazeName = Properties.Settings.Default.MazeName;
            vm.AskListOfGames();
        }
    }
}
