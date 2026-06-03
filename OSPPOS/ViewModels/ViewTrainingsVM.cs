namespace DMX.ViewModels
{
    public class ViewTrainingsVM
    {
        public string TrainingId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ? CreatedDate { get; set; } 
        public string Description { get; set; }
    }
}
