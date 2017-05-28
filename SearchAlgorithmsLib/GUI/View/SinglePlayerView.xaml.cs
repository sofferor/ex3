using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MazeLib;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace GUI.View {
    /// <summary>
    /// Interaction logic for SinglePlayerView.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerView : Window {

        /// <summary>
        /// The vm
        /// </summary>
        public SinglePlayerViewModel vm;
        /// <summary>
        /// The key
        /// </summary>
        private Key key;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public Key Key {
            get => key;
            set => key = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerView"/> class.
        /// </summary>
        /// <param name="spvm">The SPVM.</param>
        public SinglePlayerView(SinglePlayerViewModel spvm) {
            InitializeComponent();
            vm = spvm;
            DataContext = vm;
            MazeControl.DataContext = vm;

            PropertyChangedEventHandler genMaze = null;
            PropertyChangedEventHandler wonMaze = null;
            EventHandler<Key> autoPress = null;

            genMaze = delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "mazeGenerated") {
                    MazeControl.DrawMazeBoard();
                }
            };
            vm.PropertyChanged += genMaze;

            wonMaze = delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName != "wonMaze") return;
                MessageBox.Show(this, "YOU WON !!!", "Win Window", MessageBoxButton.OK);
                MainWindow win = new MainWindow();
                win.Show();
                this.Close();
                vm.PropertyChanged -= wonMaze;
                vm.AutoPress -= autoPress;
                vm.PropertyChanged -= genMaze;
            };
            vm.PropertyChanged += wonMaze;
            
            autoPress = delegate (Object sender, Key e) {
                Key = e;
                MazeBoardKeyDown(this, null);
            };
            vm.AutoPress += autoPress;

            key = Key.None;

            vm.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
        }

        /// <summary>
        /// Mazes the board key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void MazeBoardKeyDown(object sender, KeyEventArgs e) {
            Key moveKey;
            if (e == null) {
                moveKey = Key;
            } else {
                moveKey = e.Key;
            }
            switch (moveKey) {
                case Key.Down: {
                    vm.MovePlayer(Direction.Down);
                    break;
                }
                case Key.Up: {
                    vm.MovePlayer(Direction.Up);
                    break;
                }
                case Key.Left: {
                    vm.MovePlayer(Direction.Left);
                    break;
                }
                case Key.Right: {
                    vm.MovePlayer(Direction.Right);
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the Restart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Restart_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult res = MessageBox.Show(this, "Are you sure?", "Important Question", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No) {
                return;
            }
            vm.Restart();
        }

        /// <summary>
        /// Handles the Click event of the Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Main_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult res = MessageBox.Show(this, "Are you sure?", "Important Question", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No) {
                return;
            }
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the Solve control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Solve_Click(object sender, RoutedEventArgs e) {
            vm.SolveMaze();
        }
    }
}
