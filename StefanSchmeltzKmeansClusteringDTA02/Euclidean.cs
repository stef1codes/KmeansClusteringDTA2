using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.Distance
{
    public class Euclidean :IDistance
    {
        public double Distance(List<double> centroid, List<double> points)
        {
                double distance = 0;
                for (int i = 0; i < points.Count(); i++)
                {
                    distance += Math.Pow((points[i] - centroid[i]), 2);
                }
                return Math.Sqrt(distance);

        }
        
    }
}
