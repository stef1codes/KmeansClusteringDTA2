using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.KmeansAlgorithm
{
    class CreateClusters
    {
        
        public List<Centroid> ClusterDatapointsToCentroids(List<Centroid> centroids, List<Point> datapoints)
        {
              centroids.ForEach(randomCentroid => randomCentroid.cluster = datapoints.FindAll(x => x.Centroid == randomCentroid.Id).ToList());
            return centroids;
        }
    }
}
