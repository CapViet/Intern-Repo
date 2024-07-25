using Microsoft.AspNetCore.Cors.Infrastructure;
using StudentManagerMVC.Models;
using StudentManagerMVC.Repositories.ScoresRepo;

namespace StudentManagerMVC.Services.ScoreServ
{
    public class ScoresService : IScoresService
    {
        private readonly IScoresRepository _scoresRepository;

        public ScoresService(IScoresRepository scoresRepository)
        {
            _scoresRepository = scoresRepository;
        }

        public IEnumerable<Scores> GetAllScores(string sortOrder)
        {
            var scores = _scoresRepository.GetAllScores();

            switch (sortOrder)
            {
                case "id_desc":
                    scores = scores.OrderByDescending(s => s.ID);
                    break;
                case "MathScore":
                    scores = scores.OrderBy(s => s.MathScore);
                    break;
                case "math_desc":
                    scores = scores.OrderByDescending(s => s.MathScore);
                    break;
                case "ChemScore":
                    scores = scores.OrderBy(s => s.ChemScore);
                    break;
                case "chem_desc":
                    scores = scores.OrderByDescending(s => s.ChemScore);
                    break;
                case "PhysScore":
                    scores = scores.OrderBy(s => s.PhysScore);
                    break;
                case "phys_desc":
                    scores = scores.OrderByDescending(s => s.PhysScore);
                    break;
                default:
                    scores = scores.OrderBy(s => s.ID);
                    break;
            }

            return scores.ToList();
        }

        public Scores GetScoreById(int id)
        {
            return _scoresRepository.GetScoreById(id);
        }

        public void AddScore(Scores score)
        {
            _scoresRepository.AddScore(score);
            _scoresRepository.Save();
        }

        public void UpdateScore(Scores score)
        {
            _scoresRepository.UpdateScore(score);
            _scoresRepository.Save();
        }

        public void DeleteScore(int id)
        {
            _scoresRepository.DeleteScore(id);
            _scoresRepository.Save();
        }
    }
}