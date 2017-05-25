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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MazeLib;

namespace GUI.View {
    /// <summary>
    /// Interaction logic for SinglePlayerView.xaml
    /// </summary>
    public partial class SinglePlayerView : Window {

        public SinglePlayerViewModel vm;
        private Key key;

        public Key Key {
            get => key;
            set => key = value;
        }

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

        private void Restart_Click(object sender, RoutedEventArgs e) {
            vm.Restart();
        }

        private void Main_Click(object sender, RoutedEventArgs e) {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        private void Solve_Click(object sender, RoutedEventArgs e) {
            vm.SolveMaze();
        }
    }
}
