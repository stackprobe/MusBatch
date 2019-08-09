using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class ArrayTools
	{
		public static T[] GetPart<T>(T[] src, int startPos, int count)
		{
			T[] dest = new T[count];
			Array.Copy(src, startPos, dest, 0, count);
			return dest;
		}

		public static T[] Join<T>(T[][] src)
		{
			int count = 0;

			foreach (T[] part in src)
				count += part.Length;

			T[] dest = new T[count];

			foreach (T[] part in src)
			{
				Array.Copy(part, 0, dest, count, part.Length);
				count += part.Length;
			}
			return dest;
		}

		public static void Swap<T>(T[] array, int i, int j)
		{
			T tmp = array[i];
			array[i] = array[j];
			array[j] = tmp;
		}

		public static void Swap<T>(List<T> list, int i, int j)
		{
			T tmp = list[i];
			list[i] = list[j];
			list[j] = tmp;
		}

		public static void Shuffle<T>(T[] array)
		{
			for (int index = array.Length; 1 < index; index--)
			{
				Swap(array, MathTools.Random(index), index - 1);
			}
		}

		public static void Shuffle<T>(List<T> list)
		{
			for (int index = list.Count; 1 < index; index--)
			{
				Swap(list, MathTools.Random(index), index - 1);
			}
		}

		public static void Sort<T>(T[] array, Comparison<T> comp)
		{
			Array.Sort(array, comp);
		}

		public static void Sort<T>(List<T> list, Comparison<T> comp)
		{
			list.Sort(comp);
		}

		public static void Reverse<T>(T[] array)
		{
			int i = 0;
			int j = array.Length;

			while (i < j)
			{
				Swap(array, i, j);

				i++;
				j--;
			}
		}

		public static void Reverse<T>(List<T> list)
		{
			int i = 0;
			int j = list.Count;

			while (i < j)
			{
				Swap(list, i, j);

				i++;
				j--;
			}
		}

		public static void FastRemove<T>(List<T> list, int index)
		{
			list[index] = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}

		public static int IndexOf<T>(T[] array, T target, Comparison<T> comp)
		{
			for (int index = 0; index < array.Length; index++)
				if (comp(array[index], target) == 0)
					return index;

			return -1;
		}

		public static int IndexOf<T>(List<T> list, T target, Comparison<T> comp)
		{
			for (int index = 0; index < list.Count; index++)
				if (comp(list[index], target) == 0)
					return index;

			return -1;
		}

		public static bool Contains<T>(T[] array, T target, Comparison<T> comp)
		{
			return IndexOf<T>(array, target, comp) != -1;
		}

		public static bool Contains<T>(List<T> list, T target, Comparison<T> comp)
		{
			return IndexOf<T>(list, target, comp) != -1;
		}

		public static byte[] ByteSq(int bgnChr, int endChr)
		{
			byte[] buff = new byte[(endChr - bgnChr) + 1];

			for (int index = 0; bgnChr + index <= endChr; index++)
				buff[index] = (byte)(bgnChr + index);

			return buff;
		}

		public static List<T> ShallowCopy<T>(List<T> src)
		{
			List<T> dest = new List<T>();

			foreach (T element in src)
				dest.Add(element);

			return dest;
		}

		public static bool IsSame<T>(T[] a, T[] b, Comparison<T> comp)
		{
			if (a == null && b == null)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Length != b.Length)
				return false;

			for (int index = 0; index < a.Length; index++)
				if (comp(a[index], b[index]) != 0)
					return false;

			return true;
		}

		public static bool IsSame<T>(List<T> a, List<T> b, Comparison<T> comp)
		{
			if (a == null && b == null)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Count != b.Count)
				return false;

			for (int index = 0; index < a.Count; index++)
				if (comp(a[index], b[index]) != 0)
					return false;

			return true;
		}
	}
}
