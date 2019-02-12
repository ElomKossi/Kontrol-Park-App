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
    
    public partial class Intervention
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Intervention()
        {
            this.DetatilIntervention = new HashSet<DetatilIntervention>();
            this.MecaIntervention = new HashSet<MecaIntervention>();
        }
    
        public int IdIntervention { get; set; }
        public string LibelleIdIntervention { get; set; }
        public string DescriptionIdIntervention { get; set; }
        public Nullable<System.DateTime> DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
        public Nullable<int> IdTypeIntervention { get; set; }
        public Nullable<bool> Supprimer { get; set; }
        public Nullable<System.DateTime> DateSupprimer { get; set; }
        public Nullable<int> IdVehicule { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetatilIntervention> DetatilIntervention { get; set; }
        public virtual TypeIntervention TypeIntervention { get; set; }
        public virtual Vehicule Vehicule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MecaIntervention> MecaIntervention { get; set; }
    }
}