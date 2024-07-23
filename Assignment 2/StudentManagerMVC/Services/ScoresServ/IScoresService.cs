using StudentManagerMVC.Models;
using static StudentManagerMVC.Models.ViewModel;

namespace StudentManagerMVC.Services.ScoreServ
{
    public interface IScoresService
    {
        IEnumerable<Scores> GetAllScores(string sortOrder);
        Scores GetScoreById(int id);
        void AddScore(Scores score);
        void UpdateScore(Scores score);
        void DeleteScore(int id);

    }

}
