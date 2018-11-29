using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToORLibFrame
{
    public class Project
    {
        private Dictionary<int, Node> nodes;
        private int numberOfNodes;
        private List<Activity> activities;


        public static Project GenerateProject(int size)
        {
            double[,] matrix = GenerateAcyclicGraph(size);
            List<Activity> activities = new List<Activity>();
            for (int i =0; i < size; i++)
            {
                for (int j =0; j < size; j++)
                {
                    if (matrix[i,j] != 0)
                    {
                        activities.Add(new Activity("", i, j, matrix[i, j]));
                    }
                }
            }
            return new Project(size, activities);
        }

        private static double[,] GenerateAcyclicGraph(int size)
        {
            // An acyclic graph can be produced a lower triangluar matrix, with all 0s on the leading diagonal.
            double[,] matrix = new double[size, size];
            for (int i = 0; i<size; i++)
            {
                for(int j= 0; j < size; j++)
                {
                    if (i < j)
                    {
                        matrix[i, j] = GenerateRandomActivityDuration(1, 10, 0.5);
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
            return matrix;
        }

        private static double GenerateRandomActivityDuration(double min, double max, double resolution)
        {
            Random random = new Random();
            double rand = random.NextDouble();
            double notRounded = (min * rand + max * (1 - rand));
            return Math.Round(notRounded / resolution) * resolution;
        }

        private static string GenerateName()
        {
            StringBuilder sb = new StringBuilder();
            return "";
        }

        public Project(int numberOfNodes, List<Activity> activities)
        {
            this.numberOfNodes = numberOfNodes;
            this.activities = activities;
            nodes = new Dictionary<int, Node>();
            for (int i = 1; i <= numberOfNodes; i++)
            {
                nodes.Add(i, new Node(i));
            }
            foreach (Activity activity in activities)
            {
                nodes[activity.from].ForwardStar.Add(activity);
                nodes[activity.to].ReverseStar.Add(activity);
            }
        }

        public void Run()
        {
            CalculateEETs();
            CalculateLETs();
            CalculateTFs();
        }

        private void CalculateEETs()
        {
            nodes[1].EET = 0;
            for (int i = 2; i <= numberOfNodes; i++)
            {
                List<Activity> reverseStar = nodes[i].ReverseStar;
                double maxTime = double.NegativeInfinity;

                foreach (Activity activity in reverseStar)
                {
                    double EETi = nodes[activity.from].EET;
                    double costFromiToj = activity.duration;
                    if (EETi + costFromiToj > maxTime)
                    {
                        maxTime = costFromiToj + EETi;
                    }
                }
                nodes[i].EET = maxTime;

            }
        }

        private void CalculateLETs()
        {
            nodes[numberOfNodes].LET = nodes[numberOfNodes].EET;
            for (int i = numberOfNodes -1; i >= 1; i--)
            {
                List<Activity> fowardStar = nodes[i].ForwardStar;
                double minTime = double.PositiveInfinity;

                foreach (Activity activity in fowardStar)
                {
                    double LETj = nodes[activity.to].LET;
                    double costFromiToj = activity.duration;
                    if (LETj - costFromiToj < minTime)
                    {
                        minTime = LETj - costFromiToj;
                    }
                }
                nodes[i].LET = minTime;
            }

        }

        private void CalculateTFs()
        {
            foreach (Activity activity in activities)
            {
                activity.TotalFloat = nodes[activity.to].LET - nodes[activity.from].EET - activity.duration;
            }
        }

        public Dictionary<int, double> GetEETs()
        {
            Dictionary<int , double> EETs = new Dictionary<int, double>();
            for (int i = 1; i <= numberOfNodes; i++)
            {
                EETs.Add(i,nodes[i].EET);
            }
            return EETs;
        }

        public Dictionary<int, double> GetLETs()
        {
            Dictionary<int, double> LETs = new Dictionary<int, double>();
            for (int i = 1; i <= numberOfNodes; i++)
            {
                LETs.Add(i,nodes[i].LET);
            }
            return LETs;
        }
    }
}

