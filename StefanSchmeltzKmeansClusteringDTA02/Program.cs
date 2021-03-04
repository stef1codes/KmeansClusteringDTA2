using StefanSchmeltzKmeansClusteringDTA02.ReadData;
using StefanSchmeltzKmeansClusteringDTA02.TopDeals;
using StefanSchmeltzKmeansClusteringDTA02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StefanSchmeltzKmeansClusteringDTA02.Distance;

namespace StefanSchmeltzKmeansClusteringDTA02
{
    class Program
    {
        static void Main(string[] args)
        {
            IDistance distanceType = new Euclidean();;
            var numbOfIterations = 50;
            var MAXclusterIterations = 30;
            var data = new DataReader().Read();
            List<Tuple<int, double>> listBestSSE = new List<Tuple<int, double>>();
           
            
            /*  ITERATE FROM 1 TO MAX AMOUNT OF CLUSTER TO FIND THE LOWEST SSE AND HIGHEST SILLOUETTE  */

            Console.WriteLine("::::::::::DIFFERENT NUMBERS OF CLUSTERS RESULTS::::::::::");
            for (int clusters = 2; clusters < MAXclusterIterations + 1; clusters++)
             {
                var kmeans2       = new KmeansClustering(numbOfIterations, clusters, data, distanceType);
                Console.WriteLine("for "+ clusters + " clusters ->  SSE: "+kmeans2.SSE); // PRINT THE SSE AND SILHOUETTE FOR EACH AMOUNT OF K(CLUSTERS)
                listBestSSE.Add(new Tuple<int, double>(clusters, kmeans2.SSE));   
             }
            
            var lowestSSE        = listBestSSE.Min(x => x.Item2); 
            Console.WriteLine("________________________________________");
            Console.WriteLine("number of clusters: "+ listBestSSE.Where(x=>x.Item2.Equals(lowestSSE)).First().Item1);
            Console.WriteLine("Lowest SSE: "+ lowestSSE);
            Console.WriteLine("________________________________________");



            /*   ITERATE WITH THE NUMBER OF CLUSTER WITH THE BEST SSE */
            var data2 = new DataReader().Read();
            var k_with_lowest_SSE = listBestSSE.Where(x => x.Item2.Equals(lowestSSE)).First().Item1;
            var kmeans            = new KmeansClustering(numbOfIterations, k_with_lowest_SSE, data2, distanceType);
            var sill              = new Sillhouette(kmeans.Centroids, data2, distanceType);
            
            Console.WriteLine("sillhouette: "+ sill.AGVSillouette);
            kmeans.PrintAll();
            var MINamountOfBoughtWine = 0;
            
            //TOP DEALS
            new TopDeal().getTopDeal(kmeans.Centroids, MINamountOfBoughtWine );
            Console.ReadLine();
            Console.ReadLine();
        }
        

        
        
    }
}
