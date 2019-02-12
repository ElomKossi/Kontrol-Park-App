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
    
    public partial class Carburant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carburant()
        {
            this.SortieCarburant = new HashSet<SortieCarburant>();
            this.EntreeCarburant = new HashSet<EntreeCarburant>();
        }
    
        public int IdCarburant { get; set; }
        public string LibelleCarburant { get; set; }
        public Nullable<double> VolumeCarburant { get; set; }
        public Nullable<bool> Supprimer { get; set; }
        public Nullable<System.DateTime> DateSupprimer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SortieCarburant> SortieCarburant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntreeCarburant> EntreeCarburant { get; set; }
    }
}
