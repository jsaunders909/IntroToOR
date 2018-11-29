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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for NodeResults.xaml
    /// </summary>
    public partial class NodeResults : UserControl
    {
        public NodeResults(int number, double EET, double LET)
        {
            InitializeComponent();
            NodeVal.Content = string.Format("Node: {0}", number);
            EETValue.Content = string.Format("EET = {0}", EET);
            LETValue.Content = string.Format("LET = {0}", LET);
        }
    }
}
