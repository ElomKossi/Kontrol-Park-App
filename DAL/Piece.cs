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
    
    public partial class Piece
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Piece()
        {
            this.DetatilIntervention = new HashSet<DetatilIntervention>();
        }
    
        public int IdPiece { get; set; }
        public string LibellePiece { get; set; }
        public Nullable<bool> Utilise { get; set; }
        public Nullable<bool> Supprimer { get; set; }
        public Nullable<System.DateTime> DateSupprimer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetatilIntervention> DetatilIntervention { get; set; }
    }
}