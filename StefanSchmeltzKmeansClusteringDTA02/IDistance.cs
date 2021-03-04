using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.Distance
{
    public interface IDistance
    {
        double Distance(List<double> centroid, List<double> points);
    }
}
