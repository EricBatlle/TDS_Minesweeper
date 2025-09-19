using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils
{
	public class UnityRandomProvider : IRandomProvider
	{
		public void SetSeed(int seed)
		{
			Random.InitState(seed);
		}

		public int Range(int min, int max)
		{
			return Random.Range(min, max);   
		}

		public float Range(float min, float max)
		{
			return Random.Range(min, max);
		}

		public Vector3 WithinSphere(Vector3 center, float radius)
		{
			return center + Random.insideUnitSphere * radius;
		}
        
		public T PickRandomOrDefault<T>(IList<T> list)
		{
			if (list == null || list.Count == 0) {
				return default;
			}

			return list[Range(0, list.Count)];
		}

		public T PickRandomOrDefault<T>(IEnumerable<T> source)
		{
			if (source == null) {
				return default;
			}

			var list = source as IList<T> ?? source.ToList();
			return PickRandomOrDefault(list);
		}
		
		
		public List<int> GetUniqueRandomNumbers(int count, int min, int maxInclusive)
		{
			if (count > maxInclusive - min + 1)
			{
				throw new ArgumentException("The range does not have enough numbers");
			}

			var numbers = Enumerable.Range(min, maxInclusive - min + 1).ToList();

			// Fisher–Yates shuffle using UnityEngine.Random
			for (var i = numbers.Count - 1; i > 0; i--)
			{
				var j = Random.Range(0, i + 1);
				(numbers[i], numbers[j]) = (numbers[j], numbers[i]); // swap
			}

			return numbers.Take(count).ToList();
		}
	}
}