namespace StudentManagerMVC.Models
{
    public class Score
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public decimal MathScore { get; set; }
        public decimal ChemScore { get; set; }
        public decimal PhysScore { get; set; }
        public decimal AverageScore { get; set; }

        public required Student Student { get; set; }
    }
}
