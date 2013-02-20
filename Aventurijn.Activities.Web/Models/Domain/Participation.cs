using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Participation")]
    public class Participation
    {
        [Key]
        public int ParticipationId { get; set; }

        public string ExtraInfo { get; set; }

        public bool Participating { get; set; }

        public Activity Activity { get; set; }

        public Student Student { get; set; }

        public DateTime ParticipationDateTime { get; set; }
    }
}