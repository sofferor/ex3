using GUI.ViewModel;
using MazeLib;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View {
    /// <summary>
    /// Interaction logic for MultiPlayerView.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerView : Window {

        /// <summary>
        /// The vm
        /// </summary>
        public MultiPlayerViewModel vm;
        /// <summary>
        /// The close win
        /// </summary>
        private bool closeWin;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerView"/> class.
        /// </summary>
        /// <param name="spvm">The SPVM.</param>
        public MultiPlayerView(MultiPlayerViewModel spvm) {
            InitializeComponent();
            vm = spvm;
            DataContext = vm;
            MazeControl.DataContext = vm;
            OtherMazeControl.DataContext = vm;
            closeWin = false;

            vm.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "mazeGenerated") {
                    MazeControl.DrawMazeBoard();
                    OtherMazeControl.DrawMazeBoard();
                }
            };

            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                if ((e.PropertyName != "otherPlayerLeaved" && e.PropertyName != "loseMaze" && e.PropertyName != "wonMaze" && e.PropertyName != "lostConnection") || closeWin == true) {
                    return;
                }

                string message;
                string windowName;

                if (e.PropertyName == "otherPlayerLeaved") {
                    closeWin = true;
                    message = "Other player leaved";
                    windowName = "Other leaved window";
                } else if (e.PropertyName == "loseMaze") {
                    message = "YOU LOSER !!!";
                    windowName = "Lose Window";
                } else if (e.PropertyName == "wonMaze") {
                    message = "YOU WON !!!";
                    windowName = "Won Window";
                } else {
                    message = "Lost connection";
                    windowName = "Lost connection Window";
                }

                this.Dispatcher.Invoke(() => {
                    MessageBox.Show(this, message, windowName, MessageBoxButton.OK);
                    MainWindow win = new MainWindow();
                    win.Show();
                    this.Close();
                });
            };
            vm.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
        }

        /// <summary>
        /// Mazes the board key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void MazeBoardKeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
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
        /// Closes the view.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void CloseView(object sender, CancelEventArgs e) {
            //if we are on the player the was left
            if (closeWin == true) { return; }
            //else we are on the player that want to leave.
            closeWin = true;
            vm.Close();
        }

        /// <summary>
        /// Handles the Click event of the Main control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Main_Click(object sender, RoutedEventArgs e) {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}
