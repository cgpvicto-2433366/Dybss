using Bibliotheque.Classes;
using Bibliotheque.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheque.Data
{
    /// <summary>
    /// source de donnée pour les utilisateurs dans le cas d'un test
    /// </summary>
    public class UtilisateurSourceTest : IUtilisateurSource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Utilisateurs> AfficherTousLesUtilisateurs()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Utilisateurs>? ChercherUtilisateurs(string? email = null, string? nom = null, string? prenom = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Enregistrer(Utilisateurs personne)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Identifier(string email, string motDePasse)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailID"></param>
        /// <param name="email"></param>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="mdp"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Utilisateurs Modifier(string emailID, string email, string nom, string prenom, string mdp)
        {
            throw new NotImplementedException();
        }
    }
}
