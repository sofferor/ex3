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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Controls {
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl {
        public MazeControl() {
            InitializeComponent();
        }



        public string MazeString {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl));



        public int Rows {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl), new PropertyMetadata(0));




        public int Cols {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl), new PropertyMetadata(0));



        public string InitialPos {
            get { return (string)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeControl));


        public string GoalPos {
            get { return (string)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeControl));



        public string PlayerImageFile {
            get { return (string)GetValue(PlayerImageFileProperty); }
            set { SetValue(PlayerImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerImageFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerImageFileProperty =
            DependencyProperty.Register("PlayerImageFile", typeof(string), typeof(MazeControl));




        public string ExitImageFile {
            get { return (string)GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExitImageFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExitImageFileProperty =
            DependencyProperty.Register("ExitImageFile", typeof(string), typeof(MazeControl));




        //private void MazeBoardLoaded(object sender, RoutedEventArgs e) {
            
            //DrawMazeBourd();
        //}

        public void DrawMazeBoard(SinglePlayerViewModel vm) {
            int rows = vm.Rows;
            string mazeString = vm.MazeString;///maybe to skip /n and /r at the string!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            int cols = vm.Cols;
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            //calculating hight and width of rectangle
            rect.Height = (int)MazeBoard.ActualHeight / rows;
            rect.Width = (int)MazeBoard.ActualWidth / cols;
            int x = 0, y = 0;
            int xSave = x, ySave = y;

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    char c = mazeString[i * cols + cols];
                    if (c == '1') {
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Fill = new SolidColorBrush(Colors.Black);
                    } else if (c == '0') {
                        rect.Stroke = new SolidColorBrush(Colors.White);
                        rect.Fill = new SolidColorBrush(Colors.White);
                    } else if (c == '*') {
                        rect.Fill = (ImageBrush)Resources["poo"];
                        Panel.SetZIndex(rect, 1);
                    } else if (c == '#') {
                        rect.Fill = (ImageBrush)Resources["poo"];
                        Panel.SetZIndex(rect, 1);
                    }

                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);
                    MazeBoard.Children.Add(rect);

                    x += (int)rect.Width;
                }
                x = xSave;
                y += (int)rect.Height;
            }

        }

        private void MazeBoardKeyDown(object sender, KeyEventArgs e) {

        }
    }
}
