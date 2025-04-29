using Bibliotheque.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheque.Interface
{

    /// <summary>
    /// Interface definissant les actions que pourra effectuer un 
    /// controlleur de produit
    /// </summary>
    public interface IProduitSource
    {
        /// <summary>
        /// Enregistrer les produits dans la base de donnée
        /// La méthode prend un produit et construit une requête sql 
        /// pour enregistrer ses informations dans la table associé (produits)
        /// </summary>
        /// <param name="produit">toutes les informations sur le produit(nom, date de fabrication, codebarre, code rapide)</param>
        /// <returns>True si l'enregistrement à été bien fait</returns>
        public bool Enregistrer(Produits produit);

        /// <summary>
        /// Méthode pour modifier les informations d'un produit dans la 
        /// base de donnée, les paramètre de cette fonction sont obligatoire 
        /// cependant l'utilisateur n'est pas obligé de tout changer les paramètres en même
        /// dece fait il y'a une logique cote controlleur qu'il faudra mettre en place
        /// </summary>
        /// <param name="nom">Nom du produit</param>
        /// <param name="codeRapide">code rapide du produit un ensemble de 6 caractères</param>
        /// <param name="codeBarre">code barre du produit(un enmble de 15 chiffres)</param>
        /// <param name="dateFabrication">date de fabrication du produit, ne pouvant pas être superieur a la date du jour</param>
        /// <returns>Le produit une fois modifier</returns>
        public Produits Modifier(string nom, string codeRapide, string codeBarre, DateOnly dateFabrication);

        /// <summary>
        /// Afficher tous les produits de la base de donnée
        /// </summary>
        /// <returns>la liste des utilisateurs</returns>
        public List<Produits> AfficherTousLesProduits();
        
        /// <summary>
        /// Méthode pour faire des recherhes de produits, 
        /// le recherche se basera sur le premier paramètre non null de la fonction
        /// </summary>
        /// <param name="nom">Nom du produit</param>
        /// <param name="codeRapide">code rapide du produit</param>
        /// <param name="codeBarre">code barre du produit</param>
        /// <returns>une liste de produit pouvant être null</returns>
        public List<Produits>? ChercherUtilisateurs(string? nom = null, string? codeRapide = null, string? codeBarre= null);


    }
}
