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

        [StringLength(100)]
        [DisplayName("Achternaam")]
        public string LastName { get; set; }

        [DisplayName("Geboortedatum")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Nivo")]
        public Level Level { get; set; }

        public int LevelId { get; set; }
    
    
    }
}