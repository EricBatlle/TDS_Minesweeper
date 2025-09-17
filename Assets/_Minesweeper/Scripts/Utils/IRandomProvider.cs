using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils
{
	public interface IRandomProvider
	{
		void SetSeed(int seed);
		int Range(int min, int max);
		float Range(float min, float max);
		Vector3 WithinSphere(Vector3 center, float radius);

		[CanBeNull]
		T PickRandomOrDefault<T>(IList<T> list);

		[CanBeNull]
		T PickRandomOrDefault<T>(IEnumerable<T> source);

		List<int> GetUniqueRandomNumbers(int count, int min, int maxInclusive);
	}
}