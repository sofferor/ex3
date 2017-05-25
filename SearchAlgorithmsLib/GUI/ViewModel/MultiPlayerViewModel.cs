using GUI.Model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel {
    public class MultiPlayerViewModel : ViewModel {
        private MultiPlayerModel model;

        private string mazeString;
        private string otherMazeString;
        private int gameSelected;
        private ObservableCollection<string> listOfGames;

        public MultiPlayerViewModel(PlayerModel model) : base(model) {
            this.model = model as MultiPlayerModel;
            this.model.MazeGenerated += delegate (Object sender, Maze maze) {
                MazeString = maze.ToString();
                MazeName = maze.Name;
                Rows = maze.Rows;////maybe to minus 1
                Cols = maze.Cols;
                NotifyPropertyChanged("mazeGenerated");
            };
            this.model.ListOfGames += delegate (Object sender, string list) {
                string[] gameList = null;
                if (list != null) {
                    gameList = list.Replace("[", "").Replace("]", "").Replace("\n", "").Split(',');
                    for (int i = 0; i < gameList.Length; i++) {
                        listOfGames.Add(gameList[i]);
                    }
                    ListOfGames = listOfGames;
                }
            };
        }

        public string MazeString {
            get => mazeString;
            set {
                mazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }

        public string OtherMazeString {
            get => otherMazeString;
            set {
                otherMazeString = value;
                NotifyPropertyChanged("OtherMazeString");
            }
        }

        public int GameSelected {
            get { return gameSelected; }
            set {
                gameSelected = value;
                MazeString = model.GameAt(gameSelected);
            }
        }

        public ObservableCollection<string> ListOfGames {
            get { return listOfGames; }
            set {
                listOfGames = value;
                NotifyPropertyChanged("ListOfGames");
            }
        }

        public void GenerateMaze(string name, int rows, int cols) {
            model.GenerateMaze(name, rows, cols);
        }

        public void Start(string name, int rows, int cols) {
            model.Start(name, rows, cols);
        }

        public void Join(string name) {
            model.Join(name);
        }

        public void AskListOfGames() {
            model.AskListOfGames();
        }
    }
}
