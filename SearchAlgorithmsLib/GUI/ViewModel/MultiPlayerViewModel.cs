using GUI.Model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            listOfGames = new ObservableCollection<string>();
            this.model.MazeGenerated += delegate (Object sender, Maze maze) {
                MazeString = maze.ToString();
                OtherMazeString = maze.ToString();
                MazeName = maze.Name;
                Rows = maze.Rows;////maybe to minus 1
                Cols = maze.Cols;
                NotifyPropertyChanged("mazeGenerated");
            };
            this.model.ListOfGames += delegate (Object sender, string list) {
                string[] gameList = null;
                if (list != null) {
                    list = list.Replace("[", "").Replace("]", "").Replace("\n", "").Replace("\r", "").Replace("\"", "").Replace(" ", "");
                    this.model.ListOfGamesString = list;
                    gameList = list.Split(',');
                    for (int i = 0; i < gameList.Length; i++) {
                        listOfGames.Add(gameList[i]);
                    }
                    ListOfGames = listOfGames;
                }
            };
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName.Contains("close")) {
                    checkIfSomeoneWon();
                } else if (e.PropertyName.Contains("Direction")) {
                    Direction dir = getDirectionFromString(e.PropertyName);
                    MoveOtherPlayer(dir);
                } else if (e.PropertyName.Contains("lostConnection")) {
                    NotifyPropertyChanged(e.PropertyName);
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
                MazeName = listOfGames.ElementAt<string>(gameSelected);
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



        public void MovePlayer(Direction direction) {
            this.model.NewPos += delegate (Object sender, Position pos) {
                if (pos.Row == -1) {
                    return;
                } else if (pos.Row == -2) {
                    NotifyPropertyChanged("wonMaze");
                    return;
                }
                int mazeStringLen = mazeString.Length;
                char[] mazeStringArr = mazeString.ToCharArray();


                for (int i = 0; i < mazeStringLen; i++) {
                    switch (mazeStringArr[i]) {
                        case '*': {
                                mazeStringArr[i] = 'w';
                                break;
                            }
                        case 'w': {
                                mazeStringArr[i] = '0';
                                break;
                            }
                        case 'n': {
                                mazeStringArr[i] = 'w';
                                break;
                            }
                    }
                }

                int index = (pos.Row) * Cols + 2 * (pos.Row) + pos.Col;
                char ifEnd = mazeStringArr[index];
                mazeStringArr[index] = 'n';


                MazeString = new string(mazeStringArr);

                if (ifEnd == '#') {
                    NotifyPropertyChanged("wonMaze");
                }
            };
            model.Move(direction);
        }

        public void MoveOtherPlayer(Direction direction) {
            this.model.OtherNewPos += delegate (Object sender, Position pos) {
                if (pos.Row == -1) {
                    return;
                } else if (pos.Row == -2) {
                    NotifyPropertyChanged("wonMaze");
                    return;
                }
                int mazeStringLen = otherMazeString.Length;
                char[] mazeStringArr = otherMazeString.ToCharArray();


                for (int i = 0; i < mazeStringLen; i++) {
                    switch (mazeStringArr[i]) {
                        case '*': {
                                mazeStringArr[i] = 'w';
                                break;
                            }
                        case 'w': {
                                mazeStringArr[i] = '0';
                                break;
                            }
                        case 'n': {
                                mazeStringArr[i] = 'w';
                                break;
                            }
                    }
                }

                int index = (pos.Row) * Cols + 2 * (pos.Row) + pos.Col;
                char ifEnd = mazeStringArr[index];
                mazeStringArr[index] = 'n';


                OtherMazeString = new string(mazeStringArr);

                if (ifEnd == '#') {
                    NotifyPropertyChanged("wonMaze");
                }
            };
            model.MoveOtherPlayer(direction);
        }

        private Direction getDirectionFromString(string play) {
            Direction dir = Direction.Up;
            return dir;
        }

        private void checkIfSomeoneWon() {
            if (model.OtherPos.Equals(model.GoalPos)) {
                NotifyPropertyChanged("loseMaze");
            } else {
                NotifyPropertyChanged("otherPlayerLeaved");
            }
        }
    }
}
