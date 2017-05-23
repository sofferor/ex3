using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged {
        private PlayerModel model;
        private string mazeName;
        private int rows;
        private int cols;

        protected ViewModel(PlayerModel model) {
            if (model != null) {
                this.model = model;
            }
            mazeName = "";
            rows = -2;
            cols = -2;
        }

        public string MazeName {
            get { return mazeName; }
            set {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        public int Rows {
            get { return rows; }
            set {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }
        public int Cols {
            get { return cols; }
            set {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Initialize(string IP, int port) {
            model.Initialize(IP, port);
        }
    }
}
