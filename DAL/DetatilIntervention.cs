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
    
    public partial class DetatilIntervention
    {
        public int IdInterventionPiece { get; set; }
        public Nullable<int> IdIntervention { get; set; }
        public Nullable<int> IdPiece { get; set; }
    
        public virtual Intervention Intervention { get; set; }
        public virtual Piece Piece { get; set; }
    }
}