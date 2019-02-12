using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Areas.Affectation.Models
{
    public class ConducteurPermisPLUS
    {
        public int IdConducteurPermis { get; set; }
        public Nullable<int> IdCategoriePermis { get; set; }
        public Nullable<int> IdConducteur { get; set; }
        public string NumPermis { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        public Nullable<bool> Supprimer { get; set; }
        public Nullable<System.DateTime> DateSupprimer { get; set; }

        public virtual CategoriePermis CategoriePermis { get; set; }
        public virtual Conducteur Conducteur { get; set; }
    }
}