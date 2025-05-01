using System.Globalization;
using System.Text.RegularExpressions;

namespace Bibliotheque.Classes
{
    /// <summary>
    /// Classe user 
    /// entité qui définis un utilisateur général de l'application 
    /// </summary>
    public class Utilisateurs
    {
        #region Attributs
        private string _nom;
        private string _prenom;
        private string _email;
        private string _motDePasse;
        private string _role="Client";
        #endregion

        #region Constructeurs & méthodes
        /// <summary>
        /// Constructeur non vide avec toutes les informations
        /// </summary>
        /// <param name="nom">Nom valeur, non null ni vide</param>
        /// <param name="prenom">valeur, non null ni vide</param>
        /// <param name="email">valeur, non null ni vide et respectant un regex</param>
        public Utilisateurs(string nom, string prenom, string email,string role, string motDePasse)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            MotDePasse = motDePasse;

            
        }

        /// <summary>
        ///Constructeur vide 
        /// </summary>
        public Utilisateurs()
        {
            Nom = "nom";
            Prenom = "prenom";
            Email = "test@gmail.com";
            MotDePasse = "Test12345!";
        }

        /// <summary>
        /// Méthode pour afficher un utilisateur
        /// </summary>
        /// <returns>Le nom de l'utilisateur</returns>
        public override string ToString()
        {
            return Nom+" "+Prenom+" "+Email+" "+ MotDePasse;
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseurs su le nom, non null ni vide
        /// </summary>
        public string Nom
        {
            get => _nom;
            private set
            {
                if (value is null)
                    throw new ArgumentNullException("Le nom ne peut être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le nom ne peut être vide");
                _nom = value;
            }
        }

        /// <summary>
        /// Accessurs sur le prenom, non null ni vide
        /// </summary>
        public string Prenom
        {
            get => _prenom;
            private set
            {
                if (value is null)
                    throw new ArgumentNullException("Le prénom ne peut être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le prénom ne peut être vide");
                _prenom = value;
            }
        }

        /// <summary>
        /// Accessurs sur l'email, non null ni vide et respectant un regex particulier
        /// </summary>
        public string Email
        {
            get => _email;
            private set
            {
                if (value is null)
                    throw new ArgumentNullException("Le courriel ne peut être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le courriel ne peut être vide");
                Regex rgx = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$");
                _email = value;
                if (!rgx.IsMatch(value))
                    throw new ArgumentException("L'email est pas correct");
            }
        }

        /// <summary>
        /// Accesseurs su le mot de passe, non null ni vide
        /// au moins 8 caractères
        /// contenant au moins une majuscule, un caractère spécial un chiffre (frontend)
        /// </summary>
        public string MotDePasse
        {
            get => _motDePasse;
            private set
            {
                if (value is null)
                    throw new ArgumentNullException("Le mot de passe ne peut être null");
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Le mot de passe ne peut être vide");
                _motDePasse = value;
            }
        }

        /// <summary>
        /// Accesseur sur le role de l'utilisateur
        /// </summary>
        public string Role
        {
            get => _role;
            private set
            {
                _motDePasse = value;
            }
        }
        #endregion
    }
}
