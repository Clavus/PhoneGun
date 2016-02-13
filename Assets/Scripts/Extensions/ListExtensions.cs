using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListExtensions
{
	public static T PickRandom<T>(this IList<T> source)
	{
		if (source.Count == 0)
			return default(T);

		int i = Random.Range(0, source.Count);
		return source[i];
	}

	public static T PopRandom<T>(this IList<T> source)
	{
		if (source.Count == 0)
			return default(T);

		int i = Random.Range(0, source.Count);
		T value = source[i];
		source.RemoveAt(i);

		return value;
	}

	public static T First<T>(this IList<T> source)
	{
		if (source.Count == 0)
			return default(T);

		return source[0];
	}

	public static T Last<T>(this IList<T> source)
	{
		if (source.Count == 0)
			return default(T);

		return source[source.Count - 1];
	}

	public static T PopFirst<T>(this IList<T> source)
	{
		if (source.Count == 0)
			return default(T);

		T value = source.First();
		source.RemoveAt(0);

		return value;
	}

	public static T PopLast<T>(this IList<T> source)
	{
		if (source.Count == 0)
			return default(T);

		T value = source.Last();
		source.RemoveAt(source.Count - 1);

		return value;
	}

	public static T Pop<T>(this IList<T> source, int index)
	{
		if (source.Count <= index)
			return default(T);

		T value = source[index];
		source.RemoveAt(index);

		return value;
	}

	public static List<T> Clone<T>(this List<T> source)
	{
		return new List<T>(source);
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		int index = list.Count;
		while (index > 1)
		{
			index--;
			int newPos = Random.Range(0, index + 1);
			T temp = list[newPos];
			list[newPos] = list[index];
			list[index] = temp;
		}
	}

	public static void SwitchPlaces<T>(this IList<T> list, int indexOne, int indexTwo)
	{
		if (indexOne < 0 || indexTwo < 0 || indexOne == indexTwo || indexOne >= list.Count || indexTwo >= list.Count)
			return;

		T temp = list[indexOne];
		list[indexOne] = list[indexTwo];
		list[indexTwo] = temp;
	}
}