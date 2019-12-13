﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepWPFApp
{
    public static class User
    {
        public static int Leeftijd { get; set; }
        public static string Voornaam { get; set; }
        public static string Achternaam { get; set; }
        public static List<string> AllergieList = new List<string>();

//        //maak allergisch
//        public User(List<string> allergieInput, string voornaam, string achternaam, int leeftijd)
//        {
//            AllergieList = allergieInput;
//            Voornaam = voornaam;
//            Achternaam = achternaam;
//            Leeftijd = leeftijd;
//        }
//
//        //maak zonder allergie
//        public User(string naam, string achternaam, int leeftijd)
//        {
//            Voornaam = naam;
//            Achternaam = achternaam;
//            Leeftijd = leeftijd;
//        }


        public static bool  IsAllergic(Product product)
        {
            if (!AllergieList.Any())
            {
                return false;
            }

            var newData = AllergieList.Select(i => i.ToString()).Intersect(product.Allergie);
            if (newData.Any()) return true;

            return false;
        }

    }
}
