using Bibliotheque.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        private static UtilisateursCtlr _instance = null;
        #endregion

        #region Constructeur
        private UtilisateursCtlr()
        {
            
        }
        #endregion


    }
}
