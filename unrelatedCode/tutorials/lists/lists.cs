using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

           List<Player> Players = new List<Player>();

            Players.Add(new Player("Ryan Giggs", "Right Wing", 10, 10));
            Players.Add(new Player("Akinfenwa", "Center Back", 10, 10));

            Players[0].getDetails();
            Players[1].getDetails();


            Console.ReadKey();
        }
    }


    class Player
    {
        private string name;
        private string pos;
        private int stamina;
        private int power;

        public Player(string startnames, string startpos, int startstamina, int startpower)
        {
            name = startnames;
            pos = startpos;
            stamina = startstamina;
            power = startpower;
        }

        public void getDetails()
        {
            Console.WriteLine("Player Name: " + name);
            Console.WriteLine("Player pos: " + pos);
            Console.WriteLine("Player stamina: " + stamina);
            Console.WriteLine("Player power: " + power);
        }

        public void updatePower(int newPow)
        {
            power = newPow;
        }

    }
}

