using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    /// <summary>
    /// Class ViewModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class ViewModel : INotifyPropertyChanged {
        /// <summary>
        /// The model
        /// </summary>
        private PlayerModel model;
        /// <summary>
        /// The maze name
        /// </summary>
        private string mazeName;
        /// <summary>
        /// The rows
        /// </summary>
        private int rows;
        /// <summary>
        /// The cols
        /// </summary>
        private int cols;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        protected ViewModel(PlayerModel model) {
            if (model != null) {
                this.model = model;
            }
            mazeName = "";
            rows = -2;
            cols = -2;
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName {
            get { return mazeName; }
            set {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows {
            get { return rows; }
            set {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }
        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols {
            get { return cols; }
            set {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Initializes the specified ip.
        /// </summary>
        /// <param name="IP">The ip.</param>
        /// <param name="port">The port.</param>
        public void Initialize(string IP, int port) {
            model.Initialize(IP, port);
        }
    }
}
