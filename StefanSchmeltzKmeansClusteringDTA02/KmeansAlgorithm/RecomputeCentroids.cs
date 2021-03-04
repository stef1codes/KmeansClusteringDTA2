using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.KmeansAlgorithm
{
    class RecomputionOfCentroids
    {
        public void RecomputeCentroids(List<Centroid> centroids, int columnSize,List<Point> datapoints)
        {
            centroids.ForEach(centroid =>
            {
                //1. filter the datapoints that contains the id of the centroid
                List<Point> filteredData = FilterDatapoints(centroid.Id, datapoints);
                
                //2. creates a new list with the same column size in order to insert the new mean data
                var emptyCentroidList = CreateEmptyCentroidList(columnSize);
                
                //3. calculates the mean of the current centroid 
                var newCentroidMean = CalculateMeanCentroid(filteredData, emptyCentroidList);
                
                //4. old centroid data is updated with the new mean.
                centroid.Points = newCentroidMean; 
            });


        }
        //filter the datapoint that has the same centroid id as centroid id
        private List<Point> FilterDatapoints(int centroid_id, List<Point> datapoints)
        {
            List<Point> filteredlist =  datapoints.Where(dataCentroid => dataCentroid.Centroid.Equals(centroid_id)).ToList();
            return filteredlist;
        }

       
    

        public List<double> CalculateMeanCentroid(List<Point> filteredData, List<double> newcluster)
        {

            //Sum customers in cluster
            for (int i = 0; i < filteredData.Count(); i++)
            {
               
                for (int j = 0; j < newcluster.Count(); j++)
                {
                    newcluster[j] += filteredData[i].Data[j];
                   
                }
            }
            //Divide by the number of customers
            for (int i = 0; i < newcluster.Count(); i++)
            {
                newcluster[i] /= filteredData.Count;
            }
            return newcluster;
        }

        public List<double> CreateEmptyCentroidList(int columnSize)
        {
            var newcluster = new List<double>(columnSize);
            for (int i = 0; i < columnSize; i++)
            {
                newcluster.Add(0);
            }
            return newcluster;
        }

     
    }
}
