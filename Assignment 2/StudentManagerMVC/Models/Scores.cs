namespace StudentManagerMVC.Models
{
    public class Scores
    {
        public int ID { get; set; }
        public int MathScore { get; set; }
        public int ChemScore { get; set; }
        public int PhysScore { get; set; }

        // Navigation property to link with the existing table
        public Student Student { get; set; }
    }
}
