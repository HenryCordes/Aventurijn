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
        public long ActivityId { get; set; }

        [StringLength(100)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        public int SubjectId { get; set; } 
        
        [DisplayName("Gemaakt op")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DisplayName("Aktief")]
        public bool Active { get; set; }
    }
    
}