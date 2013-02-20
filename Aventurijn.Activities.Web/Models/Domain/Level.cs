using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Level")]
    public class Level
    {
        [Key]
        public int LevelId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
