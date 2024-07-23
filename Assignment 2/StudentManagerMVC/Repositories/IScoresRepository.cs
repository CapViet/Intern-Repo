using StudentManagerMVC.Models;
using static StudentManagerMVC.Models.ViewModel;

namespace StudentManagerMVC.Repositories
{
    public interface IScoresRepository
    {
        IEnumerable<Scores> GetAllScores();
        Scores GetScoreById(int id);
        void AddScore(Scores score);
        void UpdateScore(Scores score);
        void DeleteScore(int id);
        void Save();

    }
}
