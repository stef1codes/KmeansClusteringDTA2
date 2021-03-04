using StefanSchmeltzKmeansClusteringDTA02;
using StefanSchmeltzKmeansClusteringDTA02.Distance;
using StefanSchmeltzKmeansClusteringDTA02.Kmeans;
using StefanSchmeltzKmeansClusteringDTA02.KmeansAlgorithm;
using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using StefanSchmeltzKmeansClusteringDTA02.SSE;
using StefanSchmeltzKmeansClusteringDTA02.TopDeals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace StefanSchmeltzKmeansClusteringDTA02
{
    internal class KmeansClustering
    {
        public double SSE;
        public List<Centroid> Centroids;
        private readonly int columnSize;
        private readonly int iterations;
        private readonly int clusters;
        private readonly List<Point> datapoints;

        public KmeansClustering(int iterations, int clusters, List<Point> datapoints, IDistance distanceType)
        {
            Iteration(datapoints, clusters, iterations, distanceType, datapoints.Select(x => x.Data.Count).First());
            columnSize = datapoints.Select(x => x.Data.Count).First();
            this.iterations = iterations;
            this.clusters = clusters;
            this.datapoints = datapoints;
        }



        private void Iteration(List<Point> Datapoints, int clusters,int iterations, IDistance distanceType, int ColumnSize)
        {
            Centroids = new List<Centroid>();

           
            // choose a random centroids value of the amount of the chosen number of clusters
            Centroids = new ChooseRandomCentroid().RandomCentroids(Datapoints, clusters); 
           

            for (var iteration = 0; iteration < iterations; iteration++)
            {

                // /calculate the distances between every centroid and datapoints
                new DistanceCalculation().DistanceObjectToCentroids(Datapoints, Centroids, distanceType);

                //create clusters
                new CreateClusters().ClusterDatapointsToCentroids(Centroids, Datapoints);

                // recompute the centroids by calculating the mean of its cluster
                new RecomputionOfCentroids().RecomputeCentroids(Centroids, ColumnSize, Datapoints);

                // calculate the SSE
                SSE =  new calculateSSE().CalcSSE(Centroids, Datapoints, iteration, distanceType);
            }
        }

        public void PrintAll()
        {
            Console.WriteLine("number of clusters: "+ clusters);
            Console.WriteLine("SSE: "+ SSE);
            Console.WriteLine("Dataset size: " + datapoints.Count() + " items, " + columnSize + " dimensions, ");
            Console.WriteLine("dimensions: "+columnSize);
            Console.WriteLine("iterations: " + iterations);
            for (int i = 0; i < Centroids.Count(); i++)
            {
                var clusterSet = datapoints.Where(vector => vector.Centroid == i).ToList();
                Console.WriteLine("___________________________________________");
                Console.WriteLine("Cluster " + (i + 1) + " contains " + clusterSet.Count() + " items (customers)");
                Console.WriteLine("___________________________________________");
                clusterSet.ForEach(cluster => Console.WriteLine("Customer " + cluster.CustomerId + " has  bought " + cluster.SumPurchasedWine + " bottles of wine."));
                Console.Write("\n");
            }
        }


    }
}