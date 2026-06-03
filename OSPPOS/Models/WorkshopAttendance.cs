using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMX.Models
{
    public class InternalTrainingAttendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AttendanceId { get; set; }
        public string ParticipantId { get; set; }

        public string EventId { get; set; }
    }
}
