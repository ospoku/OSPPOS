namespace OSPPOS.Models;

public class Fingerprint
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PatientId { get; set; }
    public byte[] TemplateData { get; set; }

    public Patient Patient { get; set; }
}
