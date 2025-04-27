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
    /// controlleur de client
    /// </summary>
    public interface IUtilisateurSource
    {
        /// <summary>
        /// Enregistrer les infos dans  la bd
        /// </summary>
        /// <param name="personne">toutes les infos sur le client</param>
        public bool Enregistrer(Utilisateurs personne);

        /// <summary>
        /// Identifier un utilisateur
        /// </summary>
        /// <param name="email">son email</param>
        /// <param name="motDePasse">son mot de passe</param>
        /// <returns> true si il existe</returns>
        public bool Identifier(string email, string motDePasse);

        /// <summary>
        /// Modifier les informations de l'utilisateur
        /// </summary>
        /// <param name="email">email, pour identifier quel utilisateur modifier</param>
        /// <returns>L'ensemble des informations une fois modifier</returns>
        public Utilisateurs Modifier(string email, string nom, string prenom, string mdp);

        /// <summary>
        /// Afficher tous les utilisateurs
        ///  de la base de donnée
        /// </summary>
        /// <returns>la liste des utilisateurs</returns>
        public List<Utilisateurs> AfficherTousLesUtilisateurs();

        /// <summary>
        /// Cherhcher un utilisateur dans la base de donnée
        /// </summary>
        /// <returns>Un utilisateur</returns>
        /// <param name="email">email de l'utilisateur</param>
        public List<Utilisateurs>? ChercherUtilisateurs(string email);


    }
}
