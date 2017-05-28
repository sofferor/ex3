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
    public partial class MultiPlayerView : Window {

        public MultiPlayerViewModel vm;

        public MultiPlayerView(MultiPlayerViewModel spvm) {
            InitializeComponent();
            vm = spvm;
            DataContext = vm;
            MazeControl.DataContext = vm;
            OtherMazeControl.DataContext = vm;
            
            vm.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "mazeGenerated") {
                    MazeControl.DrawMazeBoard();
                    OtherMazeControl.DrawMazeBoard();
                }
            };
            /*
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName != "wonMaze") return;
                MessageBox.Show(this, "YOU WON !!!", "Win Window", MessageBoxButton.OK);
                MainWindow win = new MainWindow();
                win.Show();
                win.Show();
                this.Close();
            };
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName != "loseMaze") return;
                MessageBox.Show(this, "YOU LOSER !!!", "Lose Window", MessageBoxButton.OK);
                MainWindow win = new MainWindow();
                win.Show();
                win.Show();
                this.Close();
            };
            */

            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName != "otherPlayerLeaved" && e.PropertyName != "loseMaze" && e.PropertyName != "wonMaze" && e.PropertyName != "lostConnection") {
                    return;
                }

                string message;
                string windowName;

                if (e.PropertyName == "otherPlayerLeaved") {
                    message = "Other player leaved";
                    windowName = "Other leaved window";
                } else if (e.PropertyName == "loseMaze") {
                    message = "YOU LOSER !!!";
                    windowName = "Lose Window";
                } else if (e.PropertyName == "wonMaze") {
                    message = "YOU WON !!!";
                    windowName = "Won Window";
                } else {
                    //e.PropertyName == "lostConnection"
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
    }
}
