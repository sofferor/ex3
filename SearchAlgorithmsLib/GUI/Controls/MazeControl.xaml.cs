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
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeControl : UserControl {

        /// <summary>
        /// The curr rectangle
        /// </summary>
        private Rectangle currRectangle;
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeControl"/> class.
        /// </summary>
        public MazeControl() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the curr rectangle.
        /// </summary>
        /// <value>The curr rectangle.</value>
        public Rectangle CurrRectangle {
            get => currRectangle;
            set => currRectangle = value;
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze string property
        /// </summary>
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl), new PropertyMetadata(OnDrawMove));

        /// <summary>
        /// Handles the <see cref="E:DrawMove" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnDrawMove(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((MazeControl) d).DrawMove();
        }


        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl), new PropertyMetadata(0));




        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The cols property
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl), new PropertyMetadata(0));



        /// <summary>
        /// Gets or sets the initial position.
        /// </summary>
        /// <value>The initial position.</value>
        public string InitialPos {
            get { return (string)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The initial position property
        /// </summary>
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(string), typeof(MazeControl));


        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>The goal position.</value>
        public string GoalPos {
            get { return (string)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The goal position property
        /// </summary>
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string), typeof(MazeControl));



        /// <summary>
        /// Gets or sets the player image file.
        /// </summary>
        /// <value>The player image file.</value>
        public string PlayerImageFile {
            get { return (string)GetValue(PlayerImageFileProperty); }
            set { SetValue(PlayerImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerImageFile.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The player image file property
        /// </summary>
        public static readonly DependencyProperty PlayerImageFileProperty =
            DependencyProperty.Register("PlayerImageFile", typeof(string), typeof(MazeControl));




        /// <summary>
        /// Gets or sets the exit image file.
        /// </summary>
        /// <value>The exit image file.</value>
        public string ExitImageFile {
            get { return (string)GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExitImageFile.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The exit image file property
        /// </summary>
        public static readonly DependencyProperty ExitImageFileProperty =
            DependencyProperty.Register("ExitImageFile", typeof(string), typeof(MazeControl));


        /// <summary>
        /// Draws the maze board.
        /// </summary>
        public void DrawMazeBoard() {
            MazeBoard.Children.Clear();
            string mazeString = MazeString;///maybe to skip /n and /r at the string!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            mazeString = mazeString.Replace("\r\n", "\n");

            double x = 0, y = 0;

            foreach (char c in mazeString) {
                Rectangle rect = new Rectangle();
                rect.Height = MazeBoard.ActualHeight / Rows;
                rect.Width = MazeBoard.ActualWidth / Cols;
                switch (c) {
                    case '1': {
                        rect.Stroke = new SolidColorBrush(Colors.Black);
                        rect.Fill = new SolidColorBrush(Colors.Black);
                        break;
                    }
                    case '0': {
                        rect.Stroke = new SolidColorBrush(Colors.White);
                        rect.Fill = new SolidColorBrush(Colors.White);
                        break;
                    }
                    case '*': {
                        rect.Fill = (ImageBrush)Resources["Poo"];
                        Panel.SetZIndex(rect, 1);
                        currRectangle = rect;
                        break;
                    }
                    case '#': {
                        rect.Fill = (ImageBrush)Resources["End"];
                        Panel.SetZIndex(rect, 1);
                        break;
                    }
                    case '\n': {
                        x = 0;
                        y += rect.Height;
                        continue;
                    }
                }

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                MazeBoard.Children.Add(rect);

                x += rect.Width;
            }

        }

        /// <summary>
        /// Draws the move.
        /// </summary>
        private void DrawMove() {
            string mazeString = MazeString;
            mazeString = mazeString.Replace("\r\n", "\n");

            double rectHight = MazeBoard.ActualHeight / Rows;
            double rectWidth = MazeBoard.ActualWidth / Cols;

            double width = 0, hight = 0;
            MazeBoard.Children.Remove(currRectangle);

            foreach (char c in mazeString) {
                switch (c) {
                    case 'w': {
                        Rectangle rect = new Rectangle();
                        rect.Width = rectWidth;
                        rect.Height = rectHight;
                        rect.Stroke = new SolidColorBrush(Colors.White);
                        rect.Fill = new SolidColorBrush(Colors.White);
                        Canvas.SetLeft(rect, width);
                        Canvas.SetTop(rect, hight);
                        MazeBoard.Children.Add(rect);
                        break;
                    }
                    case 'n': {
                        Rectangle rect = new Rectangle();
                        rect.Width = rectWidth;
                        rect.Height = rectHight;
                        rect.Fill = (ImageBrush)Resources["Poo"];
                        Panel.SetZIndex(rect, 1);
                        currRectangle = rect;
                        Canvas.SetLeft(rect, width);
                        Canvas.SetTop(rect, hight);
                        MazeBoard.Children.Add(rect);
                        break;
                        }
                    case '\n': {
                        width = 0;
                        hight += rectHight;
                        continue;
                    }
                }
                width += rectWidth;
            }
        }
    }
}
