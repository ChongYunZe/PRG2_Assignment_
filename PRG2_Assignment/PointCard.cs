using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10258472
// Student Name : Daniel Sha
// Partner Name : Chong Yun Ze
//==========================================================
//==========================================================
// Student Number : S10257927
// Student Name : Chong Yun Ze
// Partner Name : Daniel Sha
//==========================================================

namespace PRG2_Assignment
{
    internal class PointCard
    {
        private int points;
        public int Points { get;set; }

        private int punchCard;
        public int PunchCard { get; set; }

        private string tier;
        public string Tier { get; set; }

        public PointCard()
        {

        }

        public PointCard(int points, int punchcard)
        {
            Points= points;
            PunchCard= punchcard;
        }

        public void AddPoints(int points)
        {
            PunchCard+= points;
            UpdateTier();
        }

        public void RedeemPoints(int points)
        {
            if (Points >= points)
            {
                Points -= points;
                UpdateTier();
            }
            else
            {
                Console.WriteLine("Insufficient points for redemption.");
            }
        }

        public void Punch()
        {
            PunchCard++;
            if (PunchCard == 10)
            {
                // Reset punch card and provide a free ice cream
                PunchCard = 0;
                Console.WriteLine("Congratulations! You've earned a free ice cream.");
            }

        }

        public void UpdateTier()
        {
            
            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50)
            {
                Tier = "Silver";
            }
            else
            {
                Tier = "Ordinary";
            }
        }
        public override string ToString()
        {
            return base.ToString() + $"Points: {Points} PunchCard: {PunchCard} Tier: {Tier}";
        }
    }
}
