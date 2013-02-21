using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Level")]
    [DisplayName("Nivo")]
    public class Level
    {
        [Key]
        public int LevelId { get; set; }

        [StringLength(100)]
        [DisplayName("Naam")]
        public string Name { get; set; }

        //public List<Student> Students { get; set; } 
    }
}
