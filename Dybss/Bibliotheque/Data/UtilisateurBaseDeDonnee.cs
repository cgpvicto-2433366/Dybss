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
using System.Runtime.InteropServices.ObjectiveC;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Digests;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;


namespace Bibliotheque.Data
{
    /// <summary>
    /// Classe contenant toutes les actions pouvant être fait sur la 
    /// base de donnée
    /// </summary>
    public class UtilisateurBaseDeDonnee : IUtilisateurSource
    {
        #region Attributs
        private static bool _status = false;
        private static MySqlConnection _connection= new MySqlConnection();
        private static UtilisateurBaseDeDonnee _instance = null;
        private string _tableAssocie = "users";
        #endregion

        #region Méthodes
        /// <summary>
        /// Connection a la base de donnée
        /// </summary>
        public static void Open()
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    Debug.Write("Connexion déja ouverte");
                _connection = new MySqlConnection("Server=localhost;Database=Dybss;Uid=root;Pwd=mysql;");
                _connection.Open();
                _status = true;
                Debug.Write("Connexion réussie à MySQL !");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion a la source de donnée");
            }
        }

        /// <summary>
        /// Fermer la connection a la base de donnée
        /// </summary>
        /// <returns>mesage de fermeture</returns>
        public static void Close()
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _status = false;
                    Debug.Write("Connexion fermée.");
                }
                else
                {
                    Debug.Write("La connexion est déjà fermée ou nulle.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erreur déconnexion a la source de donnée");
            } 
        }

        /// <summary>
        /// Methode pour hasher un mot de passe 
        /// selon l'algorithme SHA2,256
        /// </summary>
        /// <param name="motDePasse">Mot de passe a hacher, non null ni vide</param>
        /// <returns>string représentant le mot de passe hasher</returns>
        private string HacherMotDePasse(string motDePasse)
        {
            VerifivationDesChampsNull(motDePasse);
            VerifivationDesChampsVide(motDePasse);

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));

                // Convert the byte array to a hexadecimal string.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Méthode pour verifier si un champ est null
        /// </summary>
        /// <typeparam name="T">Le type du champ</typeparam>
        /// <param name="champ">le champ a verifier</param>
        private void VerifivationDesChampsNull<T>(T champ)
        {
            if (champ is null)
                throw new ArgumentNullException($"Le {nameof(champ)} est obligatoire");
        }

        /// <summary>
        /// Méthode pour verifier si un champ est vide
        /// </summary>
        /// <typeparam name="T">Le type du champ</typeparam>
        /// <param name="champ">le champ a verifier</param>
        private void VerifivationDesChampsVide<T>(T champ)
        {
            string? temp = champ?.ToString();
            if (string.IsNullOrEmpty(temp))
                throw new ArgumentException($"le {nameof(champ)} ne peut pas être vide");
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

                VerifivationDesChampsNull(nom);
                VerifivationDesChampsVide(nom);
                VerifivationDesChampsNull(prenom);
                VerifivationDesChampsVide(prenom);
                VerifivationDesChampsNull(email);
                VerifivationDesChampsVide(email);
                VerifivationDesChampsNull(mdp);
                VerifivationDesChampsVide(mdp);

                if (ChercherUtilisateurs(email) !=null)
                    throw new ArgumentException("Compte déjà existant, Essayer de vous identifier");

                //Conception de la requête Sql
                const string Colonne = "nom, prenom, email, motDepasse";

                string valeurs ="'"+ nom + "', '" + prenom + "', '" + email + "', AES_ENCRYPT(SHA2('" + mdp+"',256), @cle)";

                string requete = $"INSERT INTO {_tableAssocie} ({Colonne}) VALUE ({valeurs});";

                Open();

                MySqlCommand commande = new MySqlCommand(requete, _connection);
                commande.Parameters.AddWithValue("@cle", "DybVicQuebec");
                int rowsAffected = commande.ExecuteNonQuery();

                return (rowsAffected > 0 ? true : false);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'enregisttrement de l'utilisateur");
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        ///Identifier un utilisateur dans la base de donnée 
        /// </summary>
        /// <param name="email">l'email de l'utilisateur(champ obligatoire)</param>
        /// <param name="motDePasse">Mot de passe de l'utilisateur(champ obligatoire)</param>
        /// <returns>le role si il est identifier, si non retourne "null"</returns>
        public string Identifier(string email, string motDePasse)
        {
            VerifivationDesChampsNull(email);
            VerifivationDesChampsVide(email);
            VerifivationDesChampsNull(motDePasse);
            VerifivationDesChampsVide(motDePasse);

            //Cherche l'utilisateur
            List<Utilisateurs>? temp = ChercherUtilisateurs(email);

            if (temp != null && temp.Count == 1)
            {
                //Verifie le mot de passe
                string requete = $"SELECT AES_DECRYPT(motDePasse, @cle) AS MDP, role_user FROM {_tableAssocie} WHERE email= @Email;";

                Open();

                MySqlCommand commande = new MySqlCommand(requete, _connection);
                commande.Parameters.AddWithValue("@cle", "DybVicQuebec");
                commande.Parameters.AddWithValue("@Email", temp[0].Email);
                MySqlDataReader lecteur = commande.ExecuteReader();

                if (lecteur == null)
                {
                    throw new Exception("Aucun mot de passe enregistré ou Erreur de récupération de mot de passe dans la base de donné");
                }

                Object tempMdp = "";
                string role = "";

                while (lecteur.Read())
                {
                    tempMdp = lecteur["MDP"];
                    role = lecteur["role_user"].ToString();
                }

                Close();

                string mdpDecrypter = Encoding.UTF8.GetString((byte[])(tempMdp));

                //Hasher le mot de passe entrer par l'utilisateur avec le même algorithme
                //Utiliser pour hasher les mots de passe se trouvant dans la Base de donnée

                string mdpUserHasher = HacherMotDePasse(motDePasse);

                // Étape 3 : Comparer les deux valeurs
                if (mdpDecrypter.Equals(mdpUserHasher))
                {
                    return role;
                }

            }
            return "Null";
        }

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
        public Utilisateurs Modifier(string emailID, string email, string nom, string prenom, string mdp, string role="client")
        {
            try
            {
                VerifivationDesChampsNull(email);
                VerifivationDesChampsVide(email);
                VerifivationDesChampsNull(nom);
                VerifivationDesChampsVide(nom);
                VerifivationDesChampsNull(prenom);
                VerifivationDesChampsVide(prenom);
                VerifivationDesChampsNull(mdp);
                VerifivationDesChampsVide(mdp);

                Utilisateurs resultat = new Utilisateurs(nom, prenom, email, role, mdp);

                List<Utilisateurs>? temp = ChercherUtilisateurs(emailID);

                if (temp.Count == 1)
                {
                    //Construction de la requête
                    string requete = $"UPDATE {_tableAssocie} SET email=@Email";
                    requete += ", nom=@Nom";
                    requete += ", prenom=@Prenom";
                    requete += ", motDePasse = AES_ENCRYPT(SHA2(@mdp,256),@cle)";
                    requete += " WHERE email=@EmailId;";

                    Open();

                    MySqlCommand commande = new MySqlCommand(requete, _connection);
                    commande.Parameters.AddWithValue("@Email", email);
                    commande.Parameters.AddWithValue("@Nom", nom);
                    commande.Parameters.AddWithValue("@Prenom", prenom);
                    commande.Parameters.AddWithValue("@mdp", mdp);
                    commande.Parameters.AddWithValue("@cle", "DybVicQuebec");
                    commande.Parameters.AddWithValue("@EmailId",emailID);

                    int compteur = commande.ExecuteNonQuery();

                    if (compteur == 1)
                        return resultat;
                }
                throw new Exception("La modification de plus d'un utilisateur de façon simultanée n'est pas possible");
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'utilisateur");
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Afficher tous les utilisateurs enregistré dans la base de
        /// données
        /// </summary>
        /// <returns>La liste des utilisateurs</returns>
        public List<Utilisateurs>? AfficherTousLesUtilisateurs()
        {
            try
            {
                List<Utilisateurs> resultats = new List<Utilisateurs>();
                string? nom = "";
                string? prenom = "";
                string? courriel = "";
                string? mdp = "";
                string? role = "";

                //Construction de la requête
                string requete = $"SELECT nom, prenom, email, role_user motDepasse FROM {_tableAssocie};";

                Open();

                MySqlCommand commande = new MySqlCommand(requete, _connection);
                MySqlDataReader lecteur = commande.ExecuteReader();
                while (lecteur.Read())
                {
                    nom = lecteur["nom"].ToString();
                    prenom = lecteur["prenom"].ToString();
                    courriel = lecteur["email"].ToString();
                    mdp = lecteur["motDePasse"].ToString();
                    role= lecteur["role_user"].ToString();
                    Utilisateurs temp = new Utilisateurs(nom, prenom, courriel, role, mdp);
                    resultats.Add(temp);
                }

                return (resultats.Count == 0) ? null : resultats;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'affichage de tous les utilisateurs");
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// Chercher si le compte d'un utilisateur est déja existant
        /// La recherche peut se faire en fonction du nom, du prénom ou de l'email
        /// </summary>
        /// <param name="email">Email de l'utilisateur</param>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="prenom">Prenom de l'utilsateur</param>
        /// <returns>Une liste d'utilisateur pouvant être null</returns>
        /// <exception cref="Exception">Si il y'a une erreur coté serveur</exception>
        public List<Utilisateurs>? ChercherUtilisateurs(string? email=null, string? nom=null, string? prenom=null)
        {
            //Construction de la requête
            string requete = $"SELECT nom, prenom, email, motDePasse FROM {_tableAssocie}";

            if (email != null)
            {
                VerifivationDesChampsVide(email);
                requete += " WHERE email=@Email;";
            }
            else if (nom != null)
            {
                VerifivationDesChampsVide(nom);
                requete += " WHERE nom=@Nom";
            }

            else if (prenom != null)
            {
                VerifivationDesChampsVide(prenom);
                requete += " WHERE prenom=@Prenom;";
            }
                

            List<Utilisateurs> resultats= new List<Utilisateurs>();

            try
            {
                Open();

                MySqlCommand commande = new MySqlCommand(requete, _connection);
                if (email != null)
                    commande.Parameters.AddWithValue("@Email", email);
                if(nom!=null)
                    commande.Parameters.AddWithValue("@Nom", nom);
               if (prenom != null)
                    commande.Parameters.AddWithValue("@Prenom", prenom);

                MySqlDataReader lecteur = commande.ExecuteReader();

                string? nomNew = "";
                string? prenomNew = "";
                string? courrielNew = "";
                string? mdpNew = "";
                string? role = "";

                while (lecteur.Read())
                {
                    nomNew = lecteur["nom"].ToString();
                    prenomNew = lecteur["prenom"].ToString();
                    courrielNew = lecteur["email"].ToString();
                    mdpNew = lecteur["motDePasse"].ToString();
                    role= lecteur["role_user"].ToString();
                    Utilisateurs temp = new Utilisateurs(nomNew, prenomNew, courrielNew, role, mdpNew);
                    resultats.Add(temp);
                }

                return (resultats.Count == 0) ? null : resultats;
            }
            catch (Exception ex)
            {
                throw new Exception( "Erreur lors de la recherche de cet utilisateur dans notre source" );
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseur sur l'instance de la base de donnée
        /// Empeche de creer deux instances
        /// </summary>
        public static UtilisateurBaseDeDonnee Instance
        {
            get
            {
                _instance = _instance ?? new UtilisateurBaseDeDonnee();
                return _instance;
            }
        }
        #endregion
    }
}