namespace IIETA.Models
{
    public class Catigorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public int Total_cost { get; set; }
        public int Act_cost { get; set; }
        public bool Is_agent { get; set; }
        public Catigorie()
        {

        }
    }
}
