using Bibliotheque.Classes;
using Bibliotheque.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;
using Bibliotheque.Controlleurs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bibliotheque.Data
{
    /// <summary>
    /// Classe contenant toutes les actions pouvant être fait sur la 
    /// base de donnée
    /// </summary>
    public class BaseDeDonnee : IUtilisateurSource
    {
        #region Attributs
        private static bool _status = false;
        private static MySqlConnection _connection;
        private static BaseDeDonnee _instance = null;
        #endregion

        #region Méthodes
        /// <summary>
        /// Connection a la base de donnée
        /// </summary>
        public static string Open()
        {
            try
            {
                _connection = new MySqlConnection("Server=localhost;Database=Dybss;Uid=root;Pwd=mysql;");
                _connection.Open();
                _status = true;
                return "Connexion réussie à MySQL !";
            }
            catch (Exception ex)
            {
                return "Erreur : " + ex.Message;
            }
        }

        /// <summary>
        /// Fermer la connection a la base de donnée
        /// </summary>
        /// <returns>mesage de fermeture</returns>
        public static string Close()
        {
            if (_status == true && _connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    _connection.Close();
                    _status = false;
                    return "Connexion fermée.";
                }
                catch (Exception ex)
                {
                    return "Erreur lors de la fermeture de la connexion : " + ex.Message;
                }
            }
            else
            {
                _status = false;
                return "La connexion est déjà fermée ou nulle.";
            }
        }

        /// <summary>
        /// Enregistre l'utilisateur dans le base de donnée
        /// </summary>
        /// <param name="personne">L'utilisateur</param>
        public bool Enregistrer(Utilisateurs personne)
        {
            try
            {
                //Organisation des informations de l'utilisateur
                string nom = personne.Nom;
                string prenom = personne.Prenom;
                string email = personne.Email;
                string mdp = personne.MotDePasse;

                if (nom is null)
                    throw new ArgumentNullException("Le nom est obligatoire");
                if (prenom is null)
                    throw new ArgumentNullException("Le prénom est obligatoire");
                if (email is null)
                    throw new ArgumentNullException("L'email est obligatoire");
                if (mdp is null)
                    throw new ArgumentNullException("Le mot de passe est obligatoire est obligatoire");
                if (Identifier(email, mdp))
                    throw new ArgumentException("Compte déjà existant");

                Open();

                //Conception de la requête Sql
                const string Colonne = "nom, prenom, email, motDepasse";
                const string Table = "users";
                string valeurs ="'"+ nom + "', '" + prenom + "', '" + email + "', '" + mdp+"'";

                string requete = $"INSERT INTO {Table} ({Colonne}) VALUE ({valeurs})";

                MySqlCommand command = new MySqlCommand(requete, _connection);

                int rowsAffected = command.ExecuteNonQuery();

                Close();

                return (rowsAffected > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'exécution de la requête : " + ex.Message);
            }
        }

        /// <summary>
        ///Identifier un utilisateur dans la base de donnée 
        /// </summary>
        /// <param name="email">l'email de l'utilisateur</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur</param>
        /// <returns>True si il est identifier</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Identifier(string email, string motDePasse)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifier les informationsd'un utilisateur se trouvant dans la base de donnée
        /// </summary>
        /// <param name="email">email pour identifier l'utilsateur</param>
        /// <returns>Les informatons une fois modifié</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string Modifier(string email)
        {
            throw new NotImplementedException();
        }

        public List<Utilisateurs> AfficherTousLesUtilisateurs()
        {
            throw new NotImplementedException();
        }

        public Utilisateurs ChercherUnUtilisateur()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseur sur l'instance de la base de donnée
        /// Empeche de creer deux instances
        /// </summary>
        public static BaseDeDonnee Instance
        {
            get
            {
                _instance = _instance ?? new BaseDeDonnee();
                return _instance;
            }
        }
        #endregion
    }
}