
using OSPPOS.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSPPOS.Models
{
    public class Permission:TableAudit
    {
        [Key]
        public int PermissionId { get; set; }
    
     
        public string Module { get; set; }
       
        public string Action { get; set; }
        public string Code { get { return $"Permission.{Module}.{Action}"; } set { } } 
        //public void GenerateCode()
        //{
        //    Code = $"{Module}.{Action}";
        //}
        public string Description { get; set; }
        public bool isIsDeleted { get; set; }
    }
}
