using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.ViewModel
{
    public class NewParticipationsRequest
    {
        public List<int> StudentIds { get; set; }
        public int SubjectId { get; set; }
        public long ActivityId { get; set; }
        public string ExtraInfo { get; set; }
        public DateTime ParticipationDate { get; set; }
    }
}