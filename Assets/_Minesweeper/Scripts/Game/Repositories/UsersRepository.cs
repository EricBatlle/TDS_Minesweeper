using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Utils;

namespace Game
{
	public class UsersRepository : BaseInMemoryRepository<List<User>>
	{
		public User Create(string name, float score)
		{
			var allUsers = Get();
			var lastId = 0;
			if (allUsers.Any())
			{
				lastId = allUsers.OrderByDescending(user => user.Id).First().Id;
			}
			var newUser = new User(lastId + 1, name, score);
			Update(newUser);
			return newUser;
		}

		[CanBeNull]
		public User Get(User user)
		{
			return Get().FirstOrDefault(u => u.Id == user.Id);
		}
		
		public void Update(User user)
		{
			var inMemoryData = Get();
			inMemoryData.Add(user);
			Update(inMemoryData);
		}
	}
}