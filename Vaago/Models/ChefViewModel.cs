using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vaago.ViewModels
{
    public class ChefViewModel
    {
        [Key]
        public int Chiefid { get; set; }

        public string Chefname { get; set; }
        public string Chefemail { get; set; }
        public string chefcontact { get; set; }
        public string msg { get; set; }
    }
}