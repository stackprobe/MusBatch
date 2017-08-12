using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CrossDictionary<K, V>
	{
		private Dictionary<K, V> _kv;
		private Dictionary<V, K> _vk;

		public CrossDictionary()
			: this(new Dictionary<K, V>(), new Dictionary<V, K>())
		{ }

		public CrossDictionary(Dictionary<K, V> kv, Dictionary<V, K> vk)
		{
			_kv = kv;
			_vk = vk;
		}

		public void Put(K key, V value)
		{
			DictionaryTools.Put(_kv, key, value);
			DictionaryTools.Put(_vk, value, key);
		}

		public Dictionary<K, V> Values
		{
			get
			{
				return _kv;
			}
		}

		public Dictionary<V, K> Keys
		{
			get
			{
				return _vk;
			}
		}
	}
}
