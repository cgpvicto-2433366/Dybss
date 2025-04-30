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
    /// methodes pouvant être affectué sur les produits
    ///  dans la base de donnée
    /// </summary>
    public class ProduitBaseDeDonnee : IProduitSource
    {
        /// <summary>
        /// Afficher tous les produits se trouvant
        ///  dans la base ded donnée
        /// </summary>
        /// <returns>La liste de produits contenu dans la base de donnée, chaque produit aura toutes ses infos</returns>
        public List<Produits> AfficherTousLesProduits()
        {
            //string requete=$"SELECT "
            throw new NotImplementedException();
        }

        public List<Produits>? ChercherUtilisateurs(string? nom = null, string? codeRapide = null, string? codeBarre = null)
        {
            throw new NotImplementedException();
        }

        public bool Enregistrer(Produits produit)
        {
            throw new NotImplementedException();
        }

        public Produits Modifier(string nom, string codeRapide, string codeBarre, DateOnly dateFabrication)
        {
            throw new NotImplementedException();
        }
    }
}
