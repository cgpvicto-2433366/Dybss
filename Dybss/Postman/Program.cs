﻿using Bibliotheque;
using Bibliotheque.Classes;
using Bibliotheque.Controlleurs;
using Bibliotheque.Data;
namespace Postman
{
    /// <summary>
    /// Application console pour tester les opérations de mes 
    /// controlleurs (Postman)
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Fonction principale
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Utilisateurs test = new Utilisateurs("Ben", "test", "test00@gmail.com", "MotDepasse*12");

            //Console.Write( UtilisateursCtlr.Instance.Enregistrement(test));

            //Console.Write( UtilisateursCtlr.Instance.Identification("test3@gmail.com", "MotDepasse*12"));

            //Console.Write(UtilisateursCtlr.Instance.Modification(test, "test4@gmail.com", "test"));

            //Console.Write(UtilisateursCtlr.Instance.ChercherUtilisateurs(null,"test"));

        }
    }
}
