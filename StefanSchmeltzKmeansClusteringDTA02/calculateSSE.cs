using StefanSchmeltzKmeansClusteringDTA02.Distance;
using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.SSE
{
    public class calculateSSE
    {

        public double LowestSSE = double.MinValue;
        public double CalcSSE(List<Centroid> Centroids, List<Point> datapoints, int iteration, IDistance distanceType)
        {
            double SSE = 0;

            for (int i = 0; i < Centroids.Count; i++)
            {
                var clusterSet = datapoints.Where(vector => vector.Centroid == i).ToList();

                foreach (var datapoint in clusterSet)
                {
                    SSE += Math.Pow(distanceType.Distance(Centroids[i].Points, datapoint.Data), 2);
                }
            }
            if (LowestSSE < SSE)
            {
                LowestSSE = SSE;
            }

            //   Console.WriteLine("SSE of iteration " + iteration + ":" + SSE);
            return SSE;


        }

    }
}
