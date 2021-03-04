using StefanSchmeltzKmeansClusteringDTA02.Distance;
using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.KmeansAlgorithm
{
    class DistanceCalculation
    {
       
        public void DistanceObjectToCentroids(List<Point> datapoints, List<Centroid> Centroids, IDistance distanceType)
        {
            // 1.foreach datapoint calculate the distance measure the distance of the centroids and datapoints.
            datapoints.ForEach(datapoint => {
                var centroidDistances = new List<(int, double)>();
                
                Centroids.ForEach(Centroid => {

                    //2. calculate the distance between the datapoint and the centroids.

                    //  var distanceCentroid =  new DistanceFactory().ChooseTypeDistance(Centroid.Points, datapoint.Data, distanceType);
                    var distanceCentroid = distanceType.Distance(Centroid.Points, datapoint.Data);
                    
                    //3. Add the distances between the centroid and the datapoint into a list
                    centroidDistances.Add((Centroid.Id, distanceCentroid)); //Add the calculated distances between the customer and all centroids
                });

                //.4 take the centroid with the least distance compared to the centroid and set the id of the centroid to the datapoint.
                datapoint.Centroid = centroidDistances.OrderBy(centroid => centroid.Item2).First().Item1;
            });
        }

        
    }
}
