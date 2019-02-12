using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP.Models
{
    public class ViewsModels
    {
        public List<Vehicule> vehicule { get; set; }
        public List<Conducteur> conducteur { get; set; }
        public List<Mission> mission { get; set; }
    }
}