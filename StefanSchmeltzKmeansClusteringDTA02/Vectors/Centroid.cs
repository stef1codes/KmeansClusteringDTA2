using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector
{
    public class Centroid 
    {
        public List<double> Points { get; set; } = new List<double>();
    
        public List<Point> cluster { get; set; } = new List<Point>();
        

        public Centroid(int id, Point cPoints)
        {
            Id = id;
           
            Points = cPoints.Data;
        }
        public Centroid() { }

        public int Id { get; set; }



     
    }
}
