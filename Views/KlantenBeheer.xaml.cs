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
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;

namespace ISIS.Views
{
    /// <summary>
    /// Interaction logic for KlantenBeheer.xaml
    /// </summary>
    public partial class KlantenBeheer : System.Windows.Controls.UserControl
    {
        public KlantenBeheer()
        {
            InitializeComponent();
        }
    }

    public class SearchComboBox : ComboBox
    {

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //Disables automatic selection when DropDownsOpen!
            if (this.IsEditable &&
                this.IsDropDownOpen == false &&
                this.StaysOpenOnEdit)
            {
                this.IsDropDownOpen = true;
            }
        }

    }
}
