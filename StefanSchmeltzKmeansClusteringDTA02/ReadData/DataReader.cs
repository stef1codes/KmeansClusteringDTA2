using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.ReadData
{
    class DataReader
    {

        public List<Tuple<int, double>> rowsumdata = new List<Tuple<int, double>>();
        private string PATH = "C:/Users/Gebruiker/source/repos/StefanSchmeltzKmeansClusteringDTA02/StefanSchmeltzKmeansClusteringDTA02/ReadData/WineData.csv";

        public List<List<float>> Lines()=> File.ReadAllLines(PATH).Select(line => line.Split(',').Select(float.Parse).ToList()).ToList();


        public List<Point> Read()
        {
            List<Point> points = new List<Point>();
            var winelists = new List<List<float>>();

            //Loop through all rows and columns
            for (var i = 0; i < Lines().Count; i++)
            {
                var wine = new List<float>();
                var sumrow = Lines()[i].Count(d => d == 1);
                int count = 0;
                
                for (var j = 0; j < Lines()[i].Count; j++)
                {
                    if (points.ElementAtOrDefault(j) == null)
                    {
                        points.Add(new Point());
                    }
                    //Transpose the dataset, into a list of 100 vectors containing 32 points
                    var data = Lines()[i][j];
                    count = j;
                    points[j].AddDataPoint(count, data);
                }
                rowsumdata.Add(new Tuple<int, double>(i, sumrow));
                

            }
           
            return points;
        }



        
    }
}
         
        


