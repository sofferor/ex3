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
    /// <summary>
    /// Class MultiPlayerViewModel.
    /// </summary>
    /// <seealso cref="GUI.ViewModel.ViewModel" />
    public class MultiPlayerViewModel : ViewModel {
        /// <summary>
        /// The model
        /// </summary>
        private MultiPlayerModel model;

        /// <summary>
        /// The maze string
        /// </summary>
        private string mazeString;
        /// <summary>
        /// The other maze string
        /// </summary>
        private string otherMazeString;
        /// <summary>
        /// The game selected
        /// </summary>
        private int gameSelected;
        /// <summary>
        /// The list of games
        /// </summary>
        private ObservableCollection<string> listOfGames;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
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
                    CheckIfSomeoneWon();
                } else if (e.PropertyName.Contains("Direction")) {
                    Direction dir = getDirectionFromString(e.PropertyName);
                    MoveOtherPlayer(dir);
                } else if (e.PropertyName.Contains("lostConnection")) {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString {
            get => mazeString;
            set {
                mazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }

        /// <summary>
        /// Gets or sets the other maze string.
        /// </summary>
        /// <value>The other maze string.</value>
        public string OtherMazeString {
            get => otherMazeString;
            set {
                otherMazeString = value;
                NotifyPropertyChanged("OtherMazeString");
            }
        }

        /// <summary>
        /// Gets or sets the game selected.
        /// </summary>
        /// <value>The game selected.</value>
        public int GameSelected {
            get { return gameSelected; }
            set {
                gameSelected = value;
                MazeName = listOfGames.ElementAt<string>(gameSelected);
            }
        }

        /// <summary>
        /// Gets or sets the list of games.
        /// </summary>
        /// <value>The list of games.</value>
        public ObservableCollection<string> ListOfGames {
            get { return listOfGames; }
            set {
                listOfGames = value;
                NotifyPropertyChanged("ListOfGames");
            }
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void GenerateMaze(string name, int rows, int cols) {
            model.GenerateMaze(name, rows, cols);
        }

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void Start(string name, int rows, int cols) {
            model.Start(name, rows, cols);
        }

        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Join(string name) {
            model.Join(name);
        }

        /// <summary>
        /// Asks the list of games.
        /// </summary>
        public void AskListOfGames() {
            model.AskListOfGames();
        }



        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction.</param>
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


        /// <summary>
        /// Moves the other player.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void MoveOtherPlayer(Direction direction) {
            this.model.OtherNewPos += delegate (Object sender, Position pos) {
                if (pos.Row == -1) {
                    return;
                } else if (pos.Row == -2) {
                    NotifyPropertyChanged("loseMaze");
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
                    NotifyPropertyChanged("loseMaze");
                }
            };
            model.MoveOtherPlayer(direction);
        }

        /// <summary>
        /// Gets the direction from string.
        /// </summary>
        /// <param name="play">The play.</param>
        /// <returns>Direction.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private Direction getDirectionFromString(string play) {
            Direction dir;
            play = play.Substring(play.IndexOf("Direction"));

            if (play.Contains("up")) {
                dir = Direction.Up;
            } else if (play.Contains("down")) {
                dir = Direction.Down;
            } else if (play.Contains("right")) {
                dir = Direction.Right;
            } else if (play.Contains("left")) {
                dir = Direction.Left;
            } else {
                throw new ArgumentOutOfRangeException();
            }
            return dir;
        }

        /// <summary>
        /// Checks if someone won.
        /// </summary>
        private void CheckIfSomeoneWon() {
            if (model.OtherPos.Equals(model.GoalPos)) {
                NotifyPropertyChanged("loseMaze");
            } else {
                NotifyPropertyChanged("otherPlayerLeaved");
            }
        }


        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close() {
            model.Close();
        }
    }
}
