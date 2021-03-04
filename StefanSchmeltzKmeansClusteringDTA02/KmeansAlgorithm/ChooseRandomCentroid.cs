using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.Kmeans
{
     class ChooseRandomCentroid
    {
     
        public List<Centroid> RandomCentroids(List<Point> datapoints, int numberClusters)
        {
            // random numbers
            /*
            List<Centroid> randomCentroids = new List<Centroid>();
            List<int> rdmnumbers = new List<int>();
            Random random = new Random();
            for (int i = 0; i < numberClusters; i++)
            {
                int num = random.Next(datapoints.Count);
                rdmnumbers.Add(num);
                randomCentroids.Add(new Centroid(i, datapoints[num]));
            }
            */


            //data splitted

            //up with the amount of numbers of clusters, for each iteration take the i * k of the list
            List<Centroid> randomCentroids = new List<Centroid>();
            List<int> splitteddatanumbers = new List<int>();
            for (int i = 0; i < numberClusters ; i++)
            {

                randomCentroids.Add(new Centroid(i, datapoints[(i * (datapoints.Count - 1) / numberClusters)]));
                splitteddatanumbers.Add((i * (datapoints.Count - 1) / numberClusters));
            }

            return randomCentroids;

           
        }
    }
}
