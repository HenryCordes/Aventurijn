using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aventurijn.Activities.Web.Models.Context;
using Aventurijn.Activities.Web.Models.Domain;

namespace Aventurijn.Activities.Web.Models.ViewModel
{
 
    public class StudentViewModel
    {
        public StudentViewModel(IEnumerable<Level> levels)
        {
            Levels = levels.ToList();
        }
        public Student Student { get; set; }
   

        [Display(Name = "Nivo's")]
        public List<Level> Levels { get; private set; }
    
    }
}