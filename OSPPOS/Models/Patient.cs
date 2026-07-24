using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
    public class Patient:TableAudit
    {
        [Key]
        public int PatientId { get; set; }
        
        public string FullName { get; set; }
        public string NHISNumber { get; set; }

        public ICollection<Fingerprint> Fingerprints { get; set; }
    }


}
