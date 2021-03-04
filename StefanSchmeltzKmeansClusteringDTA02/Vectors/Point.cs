using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StefanSchmeltzKmeansClusteringDTA02.ReadData
{
    public class Point 
    {
        public List<double> Data { get; set; } = new List<double>();
        public int CustomerId { get; set; }
        public double SumPurchasedWine { get; set; }

        public int Centroid { get; set; }
        public List<Tuple<int, double>> MeanNeighbhourCluster { get; set; } = new List<Tuple<int, double>>();

        public double MeanMyCluster { get; set; }
        public double customerSillhouette { get; set; }
       
        public Point() { }

        public void AddDataPoint(int i, float point)
        {
            
            Data.Add(point);
            CustomerId = i;
            sum_of_purchased_wine();
        }
        public void AddNeighbhour(Tuple<int,double> neighbour)
        {

            MeanNeighbhourCluster.Add(neighbour);
        }

        public void sum_of_purchased_wine()
        {
            var count = Data.Count(x => x.Equals(1));

            SumPurchasedWine = count;
        }
        /*
        public void calculateSilhouette()
        {
            customerSillhouette = (MeanNeighbhourCluster.Min(x => x.Item2) - MeanMyCluster) / CalculatMax(MeanMyCluster, MeanNeighbhourCluster) == double.NaN ? 0
                : (MeanNeighbhourCluster.Min(x => x.Item2) - MeanMyCluster) / CalculatMax(MeanMyCluster, MeanNeighbhourCluster);
        }
        
        public double CalculatMax(double mycluster, List<Tuple<int, double>> neighbourCluster)
        {
           return mycluster > neighbourCluster.Min(x => x.Item2) ? mycluster : MeanNeighbhourCluster.Min(x => x.Item2);
        }
        */
        
        public void calculateSilhouette()
        {
            double cohesion = MeanMyCluster;
            double seperation = MeanNeighbhourCluster.Min(x => x.Item2);
            if (cohesion < seperation)
            {
                customerSillhouette = 1 - (cohesion / seperation);
            }
            else if (cohesion > seperation)
            {
                customerSillhouette = (seperation / cohesion) - 1;
            }
            else
            {
                customerSillhouette = 0;
            }

        }
    }
}