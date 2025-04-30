using Bibliotheque.Interface;
using Bibliotheque.Data;
using Bibliotheque.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Bibliotheque.Controlleurs
{
    /// <summary>
    /// Controlleurs pour effectuer des opérations sur un utilisateur
    /// Il est relié a la source des données sur un utilisateur et permet de 
    /// faire un certains nombre d'action
    /// </summary>
    public class UtilisateursCtlr
    {
        #region Attributs
        private IUtilisateurSource _gestionnaireUtilisateur;
        private static UtilisateursCtlr? _instance = null;
        #endregion

        #region Constructeur & Méthodes
        /// <summary>
        /// Constructeur vide
        /// </summary>
        private UtilisateursCtlr()
        {
            _gestionnaireUtilisateur = new UtilisateurBaseDeDonnee();
        }

        /// <summary>
        /// Methode pour enregistrer un utilisateur dans la base de donnée
        /// </summary>
        ///<param name="personne">L'utilisateur qu'on veut enregistré</param>
        /// <returns>true si l'enregistrement est réussi si non false
        /// </returns>
        public bool Enregistrement (Utilisateurs personne)
        {
            return _gestionnaireUtilisateur.Enregistrer(personne);
        }

        /// <summary>
        /// Authentification de l'utilisateur
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="motDePasse">Mot de passe </param>
        /// <returns>si l'utilisateur existe, on retournera son role</returns>
        public string Identification(string email, string motDePasse)
        {
            return _gestionnaireUtilisateur.Identifier(email, motDePasse);
        }

        /// <summary>
        /// Methode pour modifier les informations d'un utilisateur
        /// </summary>
        /// <param name="email">Courriel (obligatoire) car servira a identifier l'utilisateur dans la bd</param>
        /// <param name="nom">Nouveau nom (facultatif)</param>
        /// <param name="prenom">Nouveau prénom (facultatif)</param>
        /// <param name="motDp">NOuveau mot de passe (facultatif)</param>
        /// <returns>L'utilisateur une fois modifié</returns>
        public Utilisateurs Modification(Utilisateurs user,string? email=null, string? nom=null, string? prenom=null, string? motDp= null)
        {
            if (nom is null)
                nom = user.Nom;
            if (prenom is null)
                prenom = user.Prenom;
            if (motDp is null)
                motDp = user.MotDePasse;
            if (email is null)
                email = user.Email;

            return _gestionnaireUtilisateur.Modifier(user.Email, email, nom, prenom, motDp);
        }

        /// <summary>
        /// Méthodes pour afficher tous les utilisateurs enregistrer dans la base de donnée
        /// </summary>
        /// <returns>Liste des utilisateurs</returns>
        public List<Utilisateurs> TousLesUtilisateurs()
        {
            return _gestionnaireUtilisateur.AfficherTousLesUtilisateurs();
        }

        /// <summary>
        /// Cherhcer des utilisateurs en fonction d'au moins un paramètre dans la bd
        /// </summary>
        /// <param name="email">pouvant etre null</param>
        /// <param name="nom">pouvant être null</param>
        /// <param name="prenom">pouvant être null</param>
        /// <returns>Liste des utilisateurs trouvé</returns>
        public List<Utilisateurs>? ChercherUtilisateurs(string? email=null , string? nom=null, string? prenom=null)
        {
            if (email == null && prenom == null && nom == null)
                TousLesUtilisateurs();

            return _gestionnaireUtilisateur.ChercherUtilisateurs(email, nom, prenom);
        }
        #endregion

        #region Accesseur
        /// <summary>
        /// Accesseur sur l'instance du controlleur
        /// Elle assure l'unicité de l'instanciation du controlleur
        /// des utilisateurs (une instance par utilisateur)
        /// </summary>
        public static UtilisateursCtlr Instance
        {
            get
            {
                return (_instance == null) ? new UtilisateursCtlr() : _instance;
            }
        }
        #endregion
    }
}
