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

        protected ViewModel(PlayerModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));
            this.model = model;
        }

        public string MazeName {
            get { return model.MazeName; }
            set {
                model.MazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        public int Rows {
            get {
                return model.Rows;
            }
            set {
                model.Rows = value;
                NotifyPropertyChanged("Rows");
            }
        }
        public int Cols {
            get {
                return model.Cols;
            }
            set {
                model.Cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
