using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Aventurijn.Activities.Web.Models.Domain
{
    [Table("Participation")]
    [DisplayName("Deelname")]
    public class Participation
    {
        [Key]
        public int ParticipationId { get; set; }

        [DisplayName("Extra")]
        public string ExtraInfo { get; set; }

        [DisplayName("Deelname")]
        public bool Participating { get; set; }

        [DisplayName("Activiteit")]
        public Activity Activity { get; set; }
        public long ActivityId { get; set; }

        [DisplayName("Leerling")]
        public Student Student { get; set; }
        public int StudentId { get; set; }

        [DisplayName("Datum")]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime ParticipationDateTime { get; set; }

        [DisplayName("Datum")]
        [DataType(DataType.DateTime)]
        public string ParticipationDateTimeAsString {
            get
            {
                return ParticipationDateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            set
            {
                var date = new DateTime();
                if (DateTime.TryParse(value, out date))
                {
                    ParticipationDateTime = date;
                }
               
            }
        }
    }
}