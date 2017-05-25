﻿using GUI.ViewModel;
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

        private Rectangle currRectangle;
        public MazeControl() {
            InitializeComponent();
        }

        public Rectangle CurrRectangle {
            get => currRectangle;
            set => currRectangle = value;
        }

        public string MazeString {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl), new PropertyMetadata(OnDrawMove));

        private static void OnDrawMove(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((MazeControl) d).DrawMove();
        }


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
