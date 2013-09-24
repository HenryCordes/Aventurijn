using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Student")]
    [DisplayName("Leerling")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
    
        [StringLength(100)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        [DisplayName("Bouw")]
        public Level Level { get; set; }

        public int LevelId { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
    }
}