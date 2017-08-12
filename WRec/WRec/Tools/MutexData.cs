using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MutexData : IDisposable
	{
		private Mutex _m;

		public MutexData(string name)
		{
			_m = new Mutex(false, name);
		}

		public void WaitForever()
		{
			_m.WaitOne();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="millis">-1 == INFINITE</param>
		/// <returns>ロックした。</returns>
		public bool WaitForMillis(int millis)
		{
			return _m.WaitOne(millis);
		}

		public void Unlock()
		{
			_m.ReleaseMutex();
		}

		public void Dispose()
		{
			if (_m != null)
			{
				_m.Dispose();
				_m = null;
			}
		}

		public class Section : IDisposable
		{
			public Section(string name)
				: this(new MutexData(name), true)
			{ }

			private MutexData _md;
			private bool _autoDispose;

			public Section(MutexData md, bool autoDispose = false)
			{
				_md = md;
				_autoDispose = autoDispose;
				_md.WaitForever();
			}

			public void Dispose()
			{
				if (_md != null)
				{
					_md.Unlock();

					if (_autoDispose)
						_md.Dispose();

					_md = null;
				}
			}
		}
	}
}
