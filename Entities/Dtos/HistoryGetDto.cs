namespace Entities.Dtos
{
    public class HistoryGetDto
    { // Complex Type - Data Transfer Object for get list of objects in Different Language
        public int Id { get; set; }
        public string Dc_Date { get; set; }
        public string Dc_Category { get; set; }
        public string Dc_Event { get; set; }
    }
}