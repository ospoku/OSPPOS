
using DMX.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;



namespace OSPPOS.Data
{
    public class XContext(DbContextOptions<XContext> options) : AuditableIdentityContext(options)
    {
        
 
     
        public DbSet<Message> Messages { get; set; }
      
 public DbSet<Fingerprint> Fingerprints { get; set; }
    

      
        public DbSet<Permission> Permissions { get; set; }
 
        public DbSet<Patient> Patients { get; set; }
  




    }
}
