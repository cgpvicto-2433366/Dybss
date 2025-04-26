using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheque.Classes
{
    /// <summary>
    /// Classes définissant de façon générale d'un produit
    /// </summary>
    public class Produits
    {
        #region Attributs
        private string _nom;
        private string _code;
        private string _categorie;
        #endregion

        #region Constructeurs et methodes 
        /// <summary>
        /// Constructeurs d'un produits
        /// </summary>
        /// <param name="nom">nom du produit, non null ni vide</param>
        /// <param name="code">code du produit non null ni vide</param>
        /// <param name="categorie">catégorie du produit , non null ni vide</param>
        public Produits(string nom, string code, string categorie= "boisson")
        {
            Nom = nom;
            Code = code;
            Categorie = categorie;
        }

        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Produits()
        {
            Nom = "prodits";
            Code = "00000000";
            Categorie = "boisson";
        }

        /// <summary>
        /// Affichage d'un produit par son code
        /// </summary>
        /// <returns>le code du produit</returns>
        public override string ToString()
        {
            return Code;
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accessuer sur le nomù
        /// valeur non vide non null
        /// </summary>
        public string Nom 
        { 
            get => _nom; 
            private set 
            {
                if (value is null)
                    throw new ArgumentNullException("Le nom du produit ne pas être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le nom ne peut pas avoir une valeur vide");
                _nom = value;
            } 
        }

        /// <summary>
        /// Accesseurs sur le code du produit
        /// valeur non vide non null
        /// </summary>
        public string Code 
        { 
            get => _code; 
            private set 
            {
                if (value is null)
                    throw new ArgumentNullException("Le code du produit ne pas être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le code du produit ne peut pas avoir une valeur vide");
                _code = value; 
            } 
        }

        /// <summary>
        /// Accessuer sur la catégorie du produits
        /// valeur non vide non null
        /// </summary>
        public string Categorie 
        { 
            get => _categorie; 
            private set  
            {
                if (value is null)
                    throw new ArgumentNullException("La catégorie du produit ne pas être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("La catégorie ne peut pas avoir une valeur vide");

                _categorie = value; 
            } 
        }
        #endregion
    }
}
