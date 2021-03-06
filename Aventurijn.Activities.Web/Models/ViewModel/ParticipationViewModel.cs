﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Aventurijn.Activities.Web.Models.Domain;

namespace Aventurijn.Activities.Web.Models.ViewModel
{
    public class ParticipationViewModel
    {
        public ParticipationViewModel(IEnumerable<Activity> activities, IEnumerable<Student> students)
        {
            Activities = activities.ToList();
            Students = students.ToList();
        }
        public Participation Participation { get; set; }
   

        [Display(Name = "Activiteiten")]
        public List<Activity> Activities { get; private set; }

        [Display(Name = "Leerlingen")]
        public List<Student> Students { get; private set; }
    }
}