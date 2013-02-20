using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
    
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Level Level { get; set; }
    
    }
}