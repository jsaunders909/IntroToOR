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
using IntroToORLibFrame;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Activity.xaml
    /// </summary>
    public partial class Activity : UserControl
    {
        public string name;
        public int from;
        public int to;
        public double duration;

        public Activity()
        {
            InitializeComponent();
        }

        public Activity(IntroToORLibFrame.Activity activity)
        {
            InitializeComponent();
            name = activity.name;
            this.from = activity.from;
            to = activity.to;
            duration = activity.duration;

            NameText.Text = name;
            FromText.Text = from.ToString();
            ToText.Text = to.ToString();
            DurationText.Text = duration.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ((ListBox)this.Parent).Items.Remove(this);
        }

        private void FromText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Background = Brushes.White;
            if (!int.TryParse(FromText.Text, out from))
            {
                Background = Brushes.LightPink;
            }
        }

        private void ToText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Background = Brushes.White;
            if (!int.TryParse(ToText.Text, out to))
            {
                Background = Brushes.LightPink;
            }
        }

        private void DurationText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Background = Brushes.White;
            if (!double.TryParse(DurationText.Text, out duration))
            {
                Background = Brushes.LightPink;
            }
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = NameText.Text;
        }
    }
}
