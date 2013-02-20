using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Activity")]
    public class Activity
    {
        [Key]
        public string ActivityId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public Subject Subject { get; set; }
    }
}