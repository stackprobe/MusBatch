using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class NamedEventData : IDisposable
	{
		private EventWaitHandle _ewh;

		public NamedEventData(string name)
		{
			_ewh = new EventWaitHandle(false, EventResetMode.AutoReset, name);
		}

		public void Set()
		{
			_ewh.Set();
		}

		public void WaitForever()
		{
			_ewh.WaitOne();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="millis">-1 == INFINITE</param>
		/// <returns>シグナルを受け取った。</returns>
		public bool WaitForMillis(int millis)
		{
			return _ewh.WaitOne(millis);
		}

		public void Dispose()
		{
			if (_ewh != null)
			{
				_ewh.Dispose();
				_ewh = null;
			}
		}
	}
}
