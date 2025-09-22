using System;

namespace Leaderboard
{
    [Serializable]
    public class LeaderboardUser
    {
        public int Id { get; }
        public string Name { get; }
        public float Score { get; }

        public LeaderboardUser(string name, float score)
        {
            Id = -1;
            Name = name;
            Score = score;
        }
		
        public LeaderboardUser(int id, string name, float score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        public LeaderboardUser() : this("DefaultName", 10)
        {
			
        }

        public override string ToString()
        {
            return $"User {Id}: {Name}, Score: {Score}";
        }
    }
}
