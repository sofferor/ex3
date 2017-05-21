using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel {
    public class SinglePlayerViewModel : ViewModel {

        SinglePlayerModel model;

        public SinglePlayerViewModel() {
            model = new SinglePlayerModel();
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
    }
}
