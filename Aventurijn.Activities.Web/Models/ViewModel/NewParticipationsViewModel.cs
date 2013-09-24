using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Aventurijn.Activities.Web.Models.Domain;

namespace Aventurijn.Activities.Web.Models.ViewModel
{
    public class NewParticipationsViewModel
    {
        public NewParticipationsViewModel(IEnumerable<Subject> subjects, 
                                          IEnumerable<Activity> activities, 
                                          IEnumerable<Student> students,
                                          IEnumerable<Level> levels)
        {
            Subjects = subjects.ToList();
            Activities = activities.ToList();
            Students = students.ToList();
            Levels = levels.ToList();
        }

        [Display(Name = "Vakken")]
        public List<Subject> Subjects { get; private set; }

        [Display(Name = "Activiteiten")]
        public List<Activity> Activities { get; private set; }

        [Display(Name = "Leerlingen")]
        public List<Student> Students { get; private set; }

        [Display(Name = "Bouw")]
        public List<Level> Levels { get; private set; }

        public IEnumerable<Participation> Participations { get; set; }

        public DateTime ParticipationDate { get; set; }
        public string ParticipationDateAsString
        {
            get
            {
                return ParticipationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            set
            {
                var date = new DateTime();
                if (DateTime.TryParse(value, out date))
                {
                    ParticipationDate = date;
                }

            }
        }
    }
}