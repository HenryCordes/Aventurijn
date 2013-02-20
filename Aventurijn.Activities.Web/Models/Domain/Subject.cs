using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Subject")]
    [DisplayName("Onderwerp")]
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        [StringLength(100)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
    }
}