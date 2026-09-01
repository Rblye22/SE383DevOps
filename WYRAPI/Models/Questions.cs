namespace WYRAPI.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public int VotesA { get; set; }
        public int VotesB { get; set; }
        public double PercentA { get; set; }
        public double PercentB { get; set; }
    }
}
