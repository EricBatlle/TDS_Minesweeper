using System;

namespace Game
{
	[Serializable]
	public class User
	{
		public int Id { get; }
		public string Name { get; }
		public float Score { get; }

		public User(string name, float score)
		{
			Id = -1;
			Name = name;
			Score = score;
		}
		
		public User(int id, string name, float score)
		{
			Id = id;
			Name = name;
			Score = score;
		}

		public User() : this("DefaultName", 10)
		{
			
		}

		public override string ToString()
		{
			return $"User {Id}: {Name}, Score: {Score}";
		}
	}
}