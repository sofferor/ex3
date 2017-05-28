using GUI.View;
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
    /// Interaction logic for DefinePlay.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class DefinePlay : UserControl {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinePlay"/> class.
        /// </summary>
        public DefinePlay() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the text cols.
        /// </summary>
        /// <value>The text cols.</value>
        public int TxtCols {
            get => int.Parse(txtCols.Text);
            set => txtCols.Text = value.ToString();
        }

        /// <summary>
        /// Gets or sets the name of the text maze.
        /// </summary>
        /// <value>The name of the text maze.</value>
        public string TxtMazeName {
            get => txtMazeName.Text;
            set => txtMazeName.Text = value;
        }

        /// <summary>
        /// Gets or sets the text rows.
        /// </summary>
        /// <value>The text rows.</value>
        public int TxtRows {
            get => int.Parse(txtRows.Text);
            set => txtRows.Text = value.ToString();
        }
    }
}
