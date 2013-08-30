using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Aventurijn.Activities.Web.Models.Domain;

namespace Aventurijn.Activities.Web.Models.ViewModel
{
    public class ActivityViewModel
    {
        public ActivityViewModel(IEnumerable<Subject> subjects)
        {
            Subjects = subjects.ToList();
        }
        public Activity Activity { get; set; }
   

        [Display(Name = "Vakken")]
        public List<Subject> Subjects { get; private set; }
    }
}