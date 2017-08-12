using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class Nectar : IDisposable
	{
		private const string COMMON_ID = "{91ed4458-fe67-4093-a430-9dbf09db9904}"; // shared_uuid@g

		private NamedEventData _evData;
		private NamedEventData _evCtrl;
		private NamedEventData _evSend;
		private NamedEventData _evRecv;
		private int _timeoutMillis;

		public int RecvSizeMax = 20000000;

		public Nectar(string name, int timeoutMillis = 5000)
		{
			string ident = COMMON_ID + "_" + SecurityTools.GetSHA512_128String(StringTools.ENCODING_SJIS.GetBytes(name));

			_evData = new NamedEventData(ident + "_Data");
			_evCtrl = new NamedEventData(ident + "_Ctrl");
			_evSend = new NamedEventData(ident + "_Send");
			_evRecv = new NamedEventData(ident + "_Recv");
			_timeoutMillis = timeoutMillis;
		}

		/// <summary>
		/// タイムアウト又は送信に失敗すると例外を投げる。
		/// </summary>
		/// <param name="message"></param>
		public void Send(byte[] message)
		{
			// 前回正常終了しなかった可能性を考慮して、念のためクリア
			{
				_evData.WaitForMillis(0);
				_evCtrl.WaitForMillis(0);
				_evSend.WaitForMillis(0);
				_evRecv.WaitForMillis(0);
			}
			this.SendBit(false, true);

			for (int index = 0; index < message.Length; index++)
			{
				for (int bit = 1 << 7; bit != 0; bit >>= 1)
				{
					this.SendBit((message[index] & bit) != 0, false);
				}
			}
			this.SendBit(true, true);
		}

		private void SendBit(bool data, bool ctrl)
		{
			if (data)
			{
				_evData.Set();
			}
			if (ctrl)
			{
				_evCtrl.Set();
			}
			_evSend.Set();

			if (_evRecv.WaitForMillis(_timeoutMillis) == false)
			{
				// 異常終了なのでクリア
				{
					_evData.WaitForMillis(0);
					_evCtrl.WaitForMillis(0);
					_evSend.WaitForMillis(0);
					_evRecv.WaitForMillis(100); // シビアなタイミングで受信された可能性を考慮して、受信側の _ecRecv セットを少しだけ待つ。
				}

				throw new Exception("送信タイムアウト");
			}
		}

		private ByteBuffer _buff = null;
		private int _bChr = -1;
		private int _bIndex = -1;

		/// <summary>
		/// タイムアウト又は受信に失敗すると例外を投げる。
		/// </summary>
		/// <returns></returns>
		public byte[] Recv()
		{
			for (; ; )
			{
				int ret = this.RecvBit();

				if (ret == 2)
				{
					_buff = new ByteBuffer();
					_bChr = 0;
					_bIndex = 0;
				}
				else if (_buff == null)
				{
					// noop
				}
				else if (ret == 3)
				{
					byte[] message = _buff.Join();

					_buff = null;
					_bChr = -1;
					_bIndex = -1;

					return message;
				}
				else
				{
					_bChr <<= 1;
					_bChr |= ret;
					_bIndex++;

					if (_bIndex == 8)
					{
						if (this.RecvSizeMax <= _buff.Length)
						{
							throw new Exception("受信サイズ超過");
						}
						_buff.Add((byte)_bChr);
						_bChr = 0;
						_bIndex = 0;
					}
				}
			}
		}

		private int RecvBit()
		{
			if (_evSend.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("受信タイムアウト");
			}
			int ret = 0;

			if (_evData.WaitForMillis(0))
			{
				ret |= 1;
			}
			if (_evCtrl.WaitForMillis(0))
			{
				ret |= 2;
			}
			_evRecv.Set();

			return ret;
		}

		public void Dispose()
		{
			if (_evData != null)
			{
				_evData.Dispose();
				_evData = null;
				_evCtrl.Dispose();
				_evCtrl = null;
				_evSend.Dispose();
				_evSend = null;
				_evRecv.Dispose();
				_evRecv = null;
			}
		}

		public class Sender : IDisposable
		{
			private Nectar _n;

			public Sender(string name)
			{
				_n = new Nectar(name, 30000); // 30 秒 -- 受信側が存在すること前提なので、長め。
			}

			/// <summary>
			/// タイムアウト又は送信に失敗すると例外を投げる。
			/// </summary>
			/// <param name="message"></param>
			public void Send(byte[] message)
			{
				_n.Send(message);
			}

			public void Dispose()
			{
				if (_n != null)
				{
					_n.Dispose();
					_n = null;
				}
			}
		}

		public class Recver : IDisposable
		{
			private Nectar _n;

			public Recver(string name)
			{
				_n = new Nectar(name, 2000); // 2 秒 -- タイムアウトしても送信中のメッセージは維持される。interrupt 確保のため、短め。
			}

			public void SetRecvSizeMax(int recvSizeMax)
			{
				_n.RecvSizeMax = recvSizeMax;
			}


			/// <summary>
			/// クライアント応答用？
			/// </summary>
			/// <param name="timeoutMillis"></param>
			/// <returns>null == タイムアウト又は受信に失敗</returns>
			public byte[] Recv(int timeoutMillis = 60000) // 60 秒 -- 相手側の処理時間 + 応答が必ずあることが前提なので、長め。
			{
				for (int c = 0; c < timeoutMillis; c += _n._timeoutMillis)
				{
					byte[] message = this.Receipt();

					if (message != null)
					{
						return message;
					}
				}
				return null;
			}

			/// <summary>
			/// サーバー用？
			/// 受信を待機する場合、時間を空けずに繰り返し呼び出すこと。
			/// </summary>
			/// <returns></returns>
			public byte[] Receipt()
			{
				try
				{
					return _n.Recv();
				}
				catch
				{ }

				return null;
			}

			public void Dispose()
			{
				if (_n != null)
				{
					_n.Dispose();
					_n = null;
				}
			}
		}
	}
}
