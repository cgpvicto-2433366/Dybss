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
        /// Méthodes pour modifier les informations d'un utilisateur 
        /// </summary>
        /// <param name="emailID">Email de l'utilisateur servant a son identifiactaion(email avant modification)</param>
        /// <param name="email"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="mdp"></param>
        /// <returns>Utilisateur modifié</returns>
        /// <exception cref="Exception"></exception>
        public Utilisateurs Modifier(string emailID, string email, string nom, string prenom, string mdp);

        /// <summary>
        /// Afficher tous les utilisateurs
        ///  de la base de donnée
        /// </summary>
        /// <returns>la liste des utilisateurs</returns>
        public List<Utilisateurs> AfficherTousLesUtilisateurs();

        /// <summary>
        /// Chercher si le compte d'un utilisateur est déja existant
        /// La recherche peut se faire en fonction du nom, du prénom ou de l'email
        /// </summary>
        /// <param name="email">Email de l'utilisateur</param>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prenom de l'utilsateur</param>
        /// <returns>Une liste d'utilisateur pouvant être null</returns>
        /// <exception cref="Exception">Si il y'a une erreur coté serveur</exception>
        public List<Utilisateurs>? ChercherUtilisateurs(string? email = null, string? nom = null, string? prenom = null);


    }
}
