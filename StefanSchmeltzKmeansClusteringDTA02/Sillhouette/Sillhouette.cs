using System;
using System.Collections.Generic;
using System.Linq;
using StefanSchmeltzKmeansClusteringDTA02.Distance;
using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;

namespace StefanSchmeltzKmeansClusteringDTA02
{
    class Sillhouette
    {
        public List<Centroid> KmeansCentroids { get; }
        public List<Point> Data { get; }
        private IDistance distanceType;
        public double AGVSillouette;
        public Dictionary<int, Dictionary<int, double>> Distance_Matrix { get; set; } =
            new Dictionary<int, Dictionary<int, double>>();

        public Sillhouette() { }
        public Sillhouette(List<Centroid> kmeansCentroids, List<Point> data, IDistance distanceType)
        {
            KmeansCentroids = kmeansCentroids;
            Data = data;
            this.distanceType = distanceType;
            DistanceMatrix(data);
            SilhouetteCalculation(KmeansCentroids);
        }



        public void DistanceMatrix(List<Point> points){
            for (var customer = 0; customer < points.Count; customer++){
                 var dist_other_customers = new Dictionary<int, double>(); //a list to add the distance between the customer i and customer j
                for (var other_customer = customer + 1; other_customer < points.Count; other_customer++)
                {                  //calculate the distance between customer i and all the customers j. except where a> b & b > a in order to avoid duplicate values.
                    var distance = distanceType.Distance(points[customer].Data, points[other_customer].Data);
                    dist_other_customers.Add(other_customer, distance);  //Add the current customer i and the distance between customer i and customer j to this list.
                }
                Distance_Matrix.Add(customer, dist_other_customers);  //add the result of all the distance between the customers.
            }
        }

        //function that returns the average of the sillouette
        private void SilhouetteCalculation(List<Centroid> kmeansCentroids)
        {
            var listCohesianSeperation = new List<Tuple<double, double>>();
            //foreach centroid calculate the cohesion and seperation and appendthe results to a list
            kmeansCentroids.ForEach(cluster => {
                Cohension(cluster); 
                Seperation(cluster);
            });
            // calculate the sillhouette for each customer
            kmeansCentroids.ForEach(x => x.cluster.ForEach(cust => cust.calculateSilhouette())); 
            // the sillhouettes of every customer  are sbeing summed and divided by the amount of customers to obtain the average Silhouette.
            var sumSilouettte = KmeansCentroids.Select(cluster => cluster.cluster.Select(cust => cust.customerSillhouette).Sum()).Sum();
            AGVSillouette = sumSilouettte / Data.Count();
        }


        private void Cohension(Centroid centroid){
            for (var customer = 0; customer < centroid.cluster.Count; customer++){
                double sumdistance = 0; //accumulator
                for (var otherCustomer = customer + 1; otherCustomer < centroid.cluster.Count; otherCustomer++){
                    // for the amount of customers minus the target customer find the distance between the target customer and all other customers
                        sumdistance += Distance_Matrix[centroid.cluster[customer].CustomerId]
                                                      [centroid.cluster[otherCustomer ].CustomerId];  
                }
                // calculate the cohesion and add it unto the list of mean of the target customer
                centroid.cluster[customer].MeanMyCluster = sumdistance / (centroid.cluster.Count-1);
            }
        }


        private void Seperation(Centroid centroid){
            //  filter  out the cluster which has customers with identical id  as that of the centroid which we want to calculate the distance with.
            List<Centroid> tempCentroidList = (KmeansCentroids.Where(c => c.Id != centroid.Id)).ToList();
            for (int i = 0; i < tempCentroidList.Count; i++){
                for (int customer = 0; customer < centroid.cluster.Count; customer++){
                    double sumdistance = 0; // accumulator
                    for (int other_customer = 0; other_customer < tempCentroidList[i].cluster.Count; other_customer++){   
                        
                        if (tempCentroidList[i].cluster[other_customer].CustomerId < centroid.cluster[customer].CustomerId)
                        { // if the value cant't be found in the matrix because of the a> b & b > a implementation, flip the customer and the other customer to find th distance value. 
                            sumdistance += Distance_Matrix[tempCentroidList[i].cluster[other_customer].CustomerId] [centroid.cluster[customer].CustomerId]; 
                        }
                        else
                        {
                            sumdistance += Distance_Matrix[centroid.cluster[customer].CustomerId] [tempCentroidList[i].cluster[other_customer].CustomerId];
                        }
                    }
                    // add the accumulated results to the list of neighbours of the customer
                    centroid.cluster[customer].AddNeighbhour( new Tuple<int, double>(tempCentroidList[i].Id, sumdistance)); 
                }
            }
        }
    }
}
