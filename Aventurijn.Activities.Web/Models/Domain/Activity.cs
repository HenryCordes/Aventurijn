using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Activity")]
    [DisplayName("Activiteit")]
    public class Activity
    {
        [Key]
        public string ActivityId { get; set; }

        [StringLength(100)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        [DisplayName("Gemaakt op")]
        public DateTime CreationDate { get; set; }

         [DisplayName("Onderwerp")]
        public Subject Subject { get; set; }

         public int SubjectId { get; set; }
    }
}