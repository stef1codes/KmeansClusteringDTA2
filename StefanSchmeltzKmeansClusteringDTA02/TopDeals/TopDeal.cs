using StefanSchmeltzKmeansClusteringDTA02.ReadData.Vector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanSchmeltzKmeansClusteringDTA02.TopDeals
{
    class TopDeal
    {

        public void getTopDeal(List<Centroid> centroids,int amount){
            Console.WriteLine("::::::::::::::::::::::::: TOPDEALS :::::::::::::::::::::::::");
            Console.WriteLine("For minimum "+amount+" of puchased wine for each cluster");
            for (int i = 0; i < centroids.Count; i++)
            {
                List<Offers> offers = new List<Offers>();
                for (int j = 1; j < 33; j++)
                {
                    offers.Add(new Offers(j));
                }

                foreach (var point in centroids[i].cluster)
                {
                    for (int k = 0; k < 32; k++)
                    {
                        if (point.Data[k] == 1)
                        {
                            offers[k].amount += 1;
                        }
                    }
                }
                Console.WriteLine("******************* TOP DEALS FOR CLUSTER:"+ (i + 1) +" ***********************");
                PrintTopDeals(offers, amount);


            }
            
        }
        public void PrintTopDeals(List<Offers> offers, int amount)
        {
            foreach (var offer in offers.OrderByDescending(e => e.amount).Where(a => a.amount > amount))
            {
                Console.WriteLine($"Offer {offer.offerId} bought: {offer.amount}  times");
            }
        }
    }
}
