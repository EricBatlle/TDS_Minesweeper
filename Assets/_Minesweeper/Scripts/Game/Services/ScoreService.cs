using System;

namespace Game
{
	public class ScoreService
	{
		public event Action ScoreUpdated;

		private readonly IScoreRepository scoreRepository;

		public ScoreService(IScoreRepository scoreRepository)
		{
			this.scoreRepository = scoreRepository;
		}

		public void IncreaseScore(int amount)
		{
			var updatedScore = scoreRepository.Get() + amount;
			scoreRepository.Update(updatedScore);
			ScoreUpdated?.Invoke();
		}
		
		public int GetScore()
		{
			return scoreRepository.Get();
		}
		
		public void ResetScore()
		{
			scoreRepository.Delete();
			ScoreUpdated?.Invoke();
		}
	}
}