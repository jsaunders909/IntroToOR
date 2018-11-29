using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using IntroToORLibFrame;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Project p;
        List<IntroToORLibFrame.Activity> activities;
        private int numberOfNodes;
        private int numberOfArcs;

        public MainWindow()
        {
            InitializeComponent();
            numberOfNodes = -1;
            activities = new List<IntroToORLibFrame.Activity>();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(new Activity());
        }

        private void UpdateGraph(object sender, RoutedEventArgs e)
        {
            activities = new List<IntroToORLibFrame.Activity>();
            foreach (Activity activityCtrl in listBox.Items)
            {
                int from = activityCtrl.from;
                int to = activityCtrl.to;
                double duration = activityCtrl.duration;
                IntroToORLibFrame.Activity activity = new IntroToORLibFrame.Activity(activityCtrl.name, from, to, duration);
                activities.Add(activity);
            }
            int numNodes = int.MinValue;
            for (int i = 0; i < activities.Count; i++)
            {
                if (activities[i].to > numNodes)
                {
                    numNodes = activities[i].to;
                }
            }
            NumNodes.Text = numNodes.ToString();
            NumArcs.Text = activities.Count.ToString();
            UpdateNumNodes();
        }

        private void UpdateNumNodes()
        {
            NumNodes.Background = Brushes.White;
            if (!int.TryParse(NumNodes.Text, out numberOfNodes) || numberOfNodes <= 0)
            {
                NumNodes.Background = Brushes.LightPink;
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            Nodes.Items.Clear();
            FloatsBox.Items.Clear();
            try
            {
                StringBuilder sb = new StringBuilder();
                p = new Project(numberOfNodes, activities);
                p.Run();
                Dictionary<int, double> EETs = p.GetEETs();
                Dictionary<int, double> LETs = p.GetLETs();
                
                for (int i = 1; i<= numberOfNodes; i++)
                {
                    Nodes.Items.Add(new NodeResults(i, EETs[i], LETs[i]));
                }

                foreach (IntroToORLibFrame.Activity activity in activities)
                {
                    string name = activity.name;
                    double TF = activity.TotalFloat;
                    FloatsBox.Items.Add(new ActivityResult("Activity:" + name,TF ));
                    if (TF == 0)
                    {
                        sb.Append(name + "-");
                    }
                }
                s.Stop();
                long elapsed = s.ElapsedMilliseconds;

                if (elapsed > 10000)
                {
                    TimeBox.Text = ((int)Math.Round(((double)elapsed) / 1000)).ToString() + " s";
                }
                else
                {
                    TimeBox.Text = elapsed.ToString() + " ms";
                }
                sb.Remove(sb.Length - 1, 1);
                CP.Text = sb.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Graph");
            }


        }

        private void ExampleSizeSparse_Click(object sender, RoutedEventArgs e)
        {
            List<IntroToORLibFrame.Activity> activities = new List<IntroToORLibFrame.Activity>();
            activities.Add(new IntroToORLibFrame.Activity("A", 1, 2, 6));
            activities.Add(new IntroToORLibFrame.Activity("B", 2, 3, 7));
            activities.Add(new IntroToORLibFrame.Activity("C", 3, 4, 4));
            activities.Add(new IntroToORLibFrame.Activity("D", 4, 5, 5));
            activities.Add(new IntroToORLibFrame.Activity("E", 5, 6, 10));
            activities.Add(new IntroToORLibFrame.Activity("F", 6, 7, 5));
            activities.Add(new IntroToORLibFrame.Activity("G", 7, 8, 6));
            activities.Add(new IntroToORLibFrame.Activity("H", 8, 9, 7));
            activities.Add(new IntroToORLibFrame.Activity("I", 9, 10, 3));
            activities.Add(new IntroToORLibFrame.Activity("J", 10, 11, 8));
            activities.Add(new IntroToORLibFrame.Activity("K", 11, 12, 5));
            activities.Add(new IntroToORLibFrame.Activity("L", 12, 13, 6));
            activities.Add(new IntroToORLibFrame.Activity("M", 13, 14, 6));
            activities.Add(new IntroToORLibFrame.Activity("N", 14, 15, 7));
            activities.Add(new IntroToORLibFrame.Activity("O", 15, 16, 8));
            activities.Add(new IntroToORLibFrame.Activity("P", 16, 17, 9));
            activities.Add(new IntroToORLibFrame.Activity("Q", 17, 18, 6));
            activities.Add(new IntroToORLibFrame.Activity("R", 18, 19, 4));
            activities.Add(new IntroToORLibFrame.Activity("S", 19, 20, 3));
            activities.Add(new IntroToORLibFrame.Activity("T", 20, 21, 5));
            activities.Add(new IntroToORLibFrame.Activity("U", 21, 22, 7));
            activities.Add(new IntroToORLibFrame.Activity("V", 22, 23, 6));
            activities.Add(new IntroToORLibFrame.Activity("W", 23, 24, 10));
            activities.Add(new IntroToORLibFrame.Activity("X", 24, 25, 4));
            activities.Add(new IntroToORLibFrame.Activity("Y", 25, 26, 7));
            activities.Add(new IntroToORLibFrame.Activity("Z", 26, 27, 9));
            activities.Add(new IntroToORLibFrame.Activity("AA", 27, 28, 5));
            Add_Example(activities);

        }

        private void Add_Example(List<IntroToORLibFrame.Activity> activities)
        {

            listBox.Items.Clear();
            int numNodes = int.MinValue;
            for (int i =0; i < activities.Count; i++)
            {
                if (activities[i].to > numNodes)
                {
                    numNodes = activities[i].to;
                }
            }
            NumNodes.Text = numNodes.ToString();
            UpdateNumNodes();
            foreach (IntroToORLibFrame.Activity activity in activities)
            {
                listBox.Items.Add(new Activity(activity));
            }
            UpdateGraph(this, new RoutedEventArgs());

        }

        private void ExampleSizeDense_Click(object sender, RoutedEventArgs e)
        {
            List<IntroToORLibFrame.Activity> activities = new List<IntroToORLibFrame.Activity>();

            // Example in here
            activities.Add(new IntroToORLibFrame.Activity("A", 1, 2, 6));
            activities.Add(new IntroToORLibFrame.Activity("B", 1, 3, 3));
            activities.Add(new IntroToORLibFrame.Activity("C", 1, 4, 4));
            activities.Add(new IntroToORLibFrame.Activity("D", 1, 7, 3));
            activities.Add(new IntroToORLibFrame.Activity("E", 2, 7, 8));
            activities.Add(new IntroToORLibFrame.Activity("F", 2, 5, 10));
            activities.Add(new IntroToORLibFrame.Activity("G", 3, 5, 9));
            activities.Add(new IntroToORLibFrame.Activity("H", 3, 6, 8));
            activities.Add(new IntroToORLibFrame.Activity("I", 4, 6, 7));
            activities.Add(new IntroToORLibFrame.Activity("J", 4, 9, 6));
            activities.Add(new IntroToORLibFrame.Activity("K", 5, 6, 2));
            activities.Add(new IntroToORLibFrame.Activity("L", 5, 7, 5));
            activities.Add(new IntroToORLibFrame.Activity("M", 5, 8, 4));
            activities.Add(new IntroToORLibFrame.Activity("N", 5, 9, 1));
            activities.Add(new IntroToORLibFrame.Activity("O", 6, 9, 2));
            activities.Add(new IntroToORLibFrame.Activity("P", 7, 13, 6));
            activities.Add(new IntroToORLibFrame.Activity("Q", 7, 10, 6));
            activities.Add(new IntroToORLibFrame.Activity("R", 8, 10, 1));
            activities.Add(new IntroToORLibFrame.Activity("S", 8, 11, 8));
            activities.Add(new IntroToORLibFrame.Activity("T", 9, 11, 5));
            activities.Add(new IntroToORLibFrame.Activity("U", 10, 13, 3));
            activities.Add(new IntroToORLibFrame.Activity("V", 10, 12, 4));
            activities.Add(new IntroToORLibFrame.Activity("W", 10, 11, 2));
            activities.Add(new IntroToORLibFrame.Activity("X", 11, 12, 7));
            activities.Add(new IntroToORLibFrame.Activity("Y", 11, 14, 9));
            activities.Add(new IntroToORLibFrame.Activity("Z", 12, 14, 8));
            activities.Add(new IntroToORLibFrame.Activity("AA", 13, 12, 7));


            Add_Example(activities);

        }

        private void ExampleSize10_Click(object sender, RoutedEventArgs e)
        {
            List<IntroToORLibFrame.Activity> activities = new List<IntroToORLibFrame.Activity>();

            // Example in here
            activities.Add(new IntroToORLibFrame.Activity("A", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("B", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("C", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("D", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("E", 1, 3, 10));

            activities.Add(new IntroToORLibFrame.Activity("F", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("G", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("H", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("I", 1, 3, 10));
            activities.Add(new IntroToORLibFrame.Activity("J", 1, 3, 10));

            Add_Example(activities);
        }

        private void ExampleSize33_Click(object sender, RoutedEventArgs e)
        {
            List<IntroToORLibFrame.Activity> activities = new List<IntroToORLibFrame.Activity>();

            // Example in here
            activities.Add(new IntroToORLibFrame.Activity("A", 1, 2, 10));
            activities.Add(new IntroToORLibFrame.Activity("B", 1, 3, 4));
            activities.Add(new IntroToORLibFrame.Activity("C", 1, 4, 7));
            activities.Add(new IntroToORLibFrame.Activity("D", 1, 5, 8));
            activities.Add(new IntroToORLibFrame.Activity("E", 2, 6, 4)); //5

            activities.Add(new IntroToORLibFrame.Activity("F", 2, 7, 6));
            activities.Add(new IntroToORLibFrame.Activity("G", 2, 8, 7));
            activities.Add(new IntroToORLibFrame.Activity("H", 3, 6, 3));
            activities.Add(new IntroToORLibFrame.Activity("I", 3, 8, 5));
            activities.Add(new IntroToORLibFrame.Activity("J", 3, 9, 8)); //10


            activities.Add(new IntroToORLibFrame.Activity("K", 4, 6, 4));
            activities.Add(new IntroToORLibFrame.Activity("L", 4, 7, 10));
            activities.Add(new IntroToORLibFrame.Activity("M", 4, 9, 17));
            activities.Add(new IntroToORLibFrame.Activity("N", 5, 8, 3));
            activities.Add(new IntroToORLibFrame.Activity("O", 5, 9, 7)); //15
        
            activities.Add(new IntroToORLibFrame.Activity("P", 6, 10, 8));
            activities.Add(new IntroToORLibFrame.Activity("Q", 6, 11, 5));
            activities.Add(new IntroToORLibFrame.Activity("R", 6, 12, 10));
            activities.Add(new IntroToORLibFrame.Activity("S", 7, 10, 12));
            activities.Add(new IntroToORLibFrame.Activity("T", 7, 11, 7)); //20


            activities.Add(new IntroToORLibFrame.Activity("U", 7, 12, 6));
            activities.Add(new IntroToORLibFrame.Activity("V", 8, 11, 10));
            activities.Add(new IntroToORLibFrame.Activity("W", 8, 12, 8));
            activities.Add(new IntroToORLibFrame.Activity("X", 8, 13, 5));
            activities.Add(new IntroToORLibFrame.Activity("Y", 9, 12, 10)); //25

            activities.Add(new IntroToORLibFrame.Activity("Z", 9, 13, 6));
            activities.Add(new IntroToORLibFrame.Activity("AA", 10, 14, 9));
            activities.Add(new IntroToORLibFrame.Activity("AB", 10, 15, 8));
            activities.Add(new IntroToORLibFrame.Activity("AC", 11, 14, 6));
            activities.Add(new IntroToORLibFrame.Activity("AD", 12, 14, 12)); //30


            activities.Add(new IntroToORLibFrame.Activity("AE", 13, 15, 15));
            activities.Add(new IntroToORLibFrame.Activity("AF", 14, 16, 6));
            activities.Add(new IntroToORLibFrame.Activity("AG", 15, 16, 7)); //33

            Add_Example(activities);
        }

        private void ExampleSize50_Click(object sender, RoutedEventArgs e)
        {
            List<IntroToORLibFrame.Activity> activities = new List<IntroToORLibFrame.Activity>();

            // Example in here
            // Example in here
            activities.Add(new IntroToORLibFrame.Activity("A", 1, 2, 6));
            activities.Add(new IntroToORLibFrame.Activity("B", 1, 3, 7));
            activities.Add(new IntroToORLibFrame.Activity("C", 1, 4, 9));
            activities.Add(new IntroToORLibFrame.Activity("D", 1, 5, 4));
            activities.Add(new IntroToORLibFrame.Activity("E", 2, 10, 10)); //5

            activities.Add(new IntroToORLibFrame.Activity("F", 2, 6, 7));
            activities.Add(new IntroToORLibFrame.Activity("G", 2, 7, 2));
            activities.Add(new IntroToORLibFrame.Activity("H", 2, 8, 6));
            activities.Add(new IntroToORLibFrame.Activity("I", 3, 6, 5));
            activities.Add(new IntroToORLibFrame.Activity("J", 3, 7, 6)); //10


            activities.Add(new IntroToORLibFrame.Activity("A", 3, 8, 8));
            activities.Add(new IntroToORLibFrame.Activity("B", 4, 7, 9));
            activities.Add(new IntroToORLibFrame.Activity("C", 4, 8, 5));
            activities.Add(new IntroToORLibFrame.Activity("D", 4, 9, 4));
            activities.Add(new IntroToORLibFrame.Activity("E", 5, 7, 10)); //15

            activities.Add(new IntroToORLibFrame.Activity("F", 5, 8, 7));
            activities.Add(new IntroToORLibFrame.Activity("G", 5, 9, 6));
            activities.Add(new IntroToORLibFrame.Activity("H", 5, 13, 11));
            activities.Add(new IntroToORLibFrame.Activity("I", 6, 10, 5));
            activities.Add(new IntroToORLibFrame.Activity("J", 6, 11, 7)); //20


            activities.Add(new IntroToORLibFrame.Activity("K", 7, 10, 7));
            activities.Add(new IntroToORLibFrame.Activity("L", 7, 11, 5));
            activities.Add(new IntroToORLibFrame.Activity("M", 7, 15, 12));
            activities.Add(new IntroToORLibFrame.Activity("N", 7, 12, 6));
            activities.Add(new IntroToORLibFrame.Activity("O", 7, 13, 6)); //25

            activities.Add(new IntroToORLibFrame.Activity("P", 8, 12, 5));
            activities.Add(new IntroToORLibFrame.Activity("Q", 8, 13, 5));
            activities.Add(new IntroToORLibFrame.Activity("R", 9, 11, 8));
            activities.Add(new IntroToORLibFrame.Activity("S", 9, 12, 9));
            activities.Add(new IntroToORLibFrame.Activity("T", 9, 13, 7)); //30


            activities.Add(new IntroToORLibFrame.Activity("U", 10, 14, 3));
            activities.Add(new IntroToORLibFrame.Activity("V", 10, 15, 4));
            activities.Add(new IntroToORLibFrame.Activity("W", 10, 16, 7));
            activities.Add(new IntroToORLibFrame.Activity("X", 11, 14, 5));
            activities.Add(new IntroToORLibFrame.Activity("Y", 11, 15, 9)); //35

            activities.Add(new IntroToORLibFrame.Activity("Z", 12, 15, 7));
            activities.Add(new IntroToORLibFrame.Activity("AA", 12, 16, 6));
            activities.Add(new IntroToORLibFrame.Activity("AB", 12, 17, 5));
            activities.Add(new IntroToORLibFrame.Activity("AC", 13, 16, 3));
            activities.Add(new IntroToORLibFrame.Activity("AD", 13, 17, 9)); //40


            activities.Add(new IntroToORLibFrame.Activity("AE", 13, 20, 16));
            activities.Add(new IntroToORLibFrame.Activity("AF", 14, 18, 7));
            activities.Add(new IntroToORLibFrame.Activity("AG", 14, 19, 6));
            activities.Add(new IntroToORLibFrame.Activity("AH", 15, 18, 5));
            activities.Add(new IntroToORLibFrame.Activity("AI", 15, 19, 6)); //45

            activities.Add(new IntroToORLibFrame.Activity("AJ", 16, 18, 7));
            activities.Add(new IntroToORLibFrame.Activity("AK", 16, 19, 10));
            activities.Add(new IntroToORLibFrame.Activity("AL", 16, 20, 5));
            activities.Add(new IntroToORLibFrame.Activity("AM", 17, 19, 11));
            activities.Add(new IntroToORLibFrame.Activity("AN", 17, 20, 4)); //50

            activities.Add(new IntroToORLibFrame.Activity("AO", 18, 21, 5));
            activities.Add(new IntroToORLibFrame.Activity("AP", 18, 23, 9));
            activities.Add(new IntroToORLibFrame.Activity("AQ", 19, 21, 7));
            activities.Add(new IntroToORLibFrame.Activity("AR", 19, 22, 3));
            activities.Add(new IntroToORLibFrame.Activity("AS", 20, 22, 6)); //55

            activities.Add(new IntroToORLibFrame.Activity("AT", 21, 23, 3));
            activities.Add(new IntroToORLibFrame.Activity("AU", 22, 23, 4));


            Add_Example(activities);
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            NumArcs.Background = Brushes.White;
            if (int.TryParse(NumArcs.Text, out numberOfArcs))
            {
                NumArcs.Background = Brushes.LightPink;
                Project p = Project.GenerateProject(numberOfArcs);
                Add_Example(p.activities);
            }   
        }
    }
}
