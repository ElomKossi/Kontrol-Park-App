//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConducteurPermis
    {
        public int IdConducteurPermis { get; set; }
        public Nullable<int> IdCategoriePermis { get; set; }
        public Nullable<int> IdConducteur { get; set; }
        public Nullable<bool> Supprimer { get; set; }
        public Nullable<System.DateTime> DateSupprimer { get; set; }
    
        public virtual CategoriePermis CategoriePermis { get; set; }
        public virtual Conducteur Conducteur { get; set; }
    }
}