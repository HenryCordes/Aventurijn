using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Aventurijn.Activities.Web.Models.ViewModel
{
    public class ParticipationsPerSubject
    {
        public string SubjectName { get; set; }
        public int SubjectId { get; set; }

        public string ActivityName { get; set; }
        public long ActivityId { get; set; }

        public int NumberOfParticipations { get; set; }
    }
}