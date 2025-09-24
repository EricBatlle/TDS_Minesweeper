using UnityEngine;

namespace Utils
{
	public abstract class BasePlayerPrefsRepository<T> : IRepository<T> where T : new()
	{
		protected abstract string PlayerPrefsKey { get; }
		private T inMemoryData;
		
		protected virtual string Serialize(T data) => JsonUtility.ToJson(data);
		protected virtual T Deserialize(string json) => string.IsNullOrEmpty(json) ? Create() : JsonUtility.FromJson<T>(json);

		public T Get()
		{
			if (inMemoryData != null)
			{
				return inMemoryData;
			}

			var serializedData = PlayerPrefs.GetString(PlayerPrefsKey);
			inMemoryData = Deserialize(serializedData);
			return inMemoryData;
		}

		public T Create()
		{
			inMemoryData = new T();
			PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(inMemoryData));
			PlayerPrefs.Save();
			return inMemoryData;
		}

		public T Update(T data)
		{
			inMemoryData = data;
			PlayerPrefs.SetString(PlayerPrefsKey, Serialize(inMemoryData));
			PlayerPrefs.Save();
			return inMemoryData;
		}
	}
}