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

        public SinglePlayerView(SinglePlayerViewModel spvm) {
            InitializeComponent();
            vm = spvm;
            DataContext = vm;
            MazeControl.DataContext = vm;
            vm.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "mazeGenerated") {
                    MazeControl.DrawMazeBoard();
                }
            };
            vm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName != "wonMaze") return;
                MessageBox.Show(this, "YOU WON !!!", "Win Window", MessageBoxButton.OK);
                /*
                MainWindow win = new MainWindow();
                win = (MainWindow) Application.Current.MainWindow;
                win.Show();
                */
                foreach (Window window in Application.Current.Windows) {
                    if (!window.IsActive) {
                        window.Show();
                    }
                }
                this.Close();
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
