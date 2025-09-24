using System;
using UnityEngine;

namespace Leaderboard
{
    [Serializable]
    public class LeaderboardUser
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string name;
        [SerializeField]
        private float score;

        public int Id => id;
        public string Name => name;
        public float Score => score;

        public LeaderboardUser(string name, float score)
        {
            id = -1;
            this.name = name;
            this.score = score;
        }
		
        public LeaderboardUser(int id, string name, float score)
        {
            this.id = id;
            this.name = name;
            this.score = score;
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
