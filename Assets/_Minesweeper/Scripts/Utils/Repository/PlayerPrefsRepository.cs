using UnityEngine;

namespace Utils
{
	public abstract class PlayerPrefsRepository<T> : IRepository<T> where T : new()
	{
		protected abstract string PlayerPrefsKey { get; }
		private T inMemoryData;

		public T Get()
		{
			if (inMemoryData != null)
			{
				return inMemoryData;
			}

			var serializedData = PlayerPrefs.GetString(PlayerPrefsKey);
			return serializedData == string.Empty ? Create() : JsonUtility.FromJson<T>(serializedData);
		}

		public T Create()
		{
			inMemoryData = new T();
			PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(inMemoryData));
			return inMemoryData;
		}

		public T Update(T data)
		{
			inMemoryData = data;
			PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(inMemoryData));
			return inMemoryData;
		}
	}
}