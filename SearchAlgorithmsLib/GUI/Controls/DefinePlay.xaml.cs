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
    public partial class DefinePlay : UserControl {
        public DefinePlay() {
            InitializeComponent();
        }

        public int TxtCols {
            get => int.Parse(txtCols.Text);
            set => txtCols.Text = value.ToString();
        }

        public string TxtMazeName {
            get => txtMazeName.Text;
            set => txtMazeName.Text = value;
        }

        public int TxtRows {
            get => int.Parse(txtRows.Text);
            set => txtRows.Text = value.ToString();
        }
    }
}
