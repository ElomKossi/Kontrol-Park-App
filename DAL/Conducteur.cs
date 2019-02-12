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
    
    public partial class Conducteur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Conducteur()
        {
            this.Affectation = new HashSet<Affectation>();
            this.ConducteurPermis = new HashSet<ConducteurPermis>();
        }
    
        public int IdConducteur { get; set; }
        public string NumCNIConducteur { get; set; }
        public string NomConducteur { get; set; }
        public string PrenomConducteur { get; set; }
        public Nullable<System.DateTime> DateNaissanceConducteur { get; set; }
        public string LieuxNaissanceConducteur { get; set; }
        public string SexeConducteur { get; set; }
        public string AdresseConducteur { get; set; }
        public string TelConducteur { get; set; }
        public string EmailConducteur { get; set; }
        public Nullable<System.DateTime> DateEmbaucheConducteur { get; set; }
        public string NumPermis { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        public Nullable<bool> EnActivite { get; set; }
        public Nullable<bool> EnMission { get; set; }
        public Nullable<System.DateTime> DateDesactivation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Affectation> Affectation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConducteurPermis> ConducteurPermis { get; set; }
    }
}