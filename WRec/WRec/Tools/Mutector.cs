using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class Mutector : IDisposable
	{
		private const string COMMON_ID = "{fab3c841-8891-4273-8bd1-50525845fea7}"; // shared_uuid@g

		private enum M_INDEX
		{
			Sender,
			Recver,
			Sync_0,
			Sync_1,
			Sync_2,
			Bit_0_0,
			Bit_0_1,
			Bit_0_2,
			Bit_1_0,
			Bit_1_1,
			Bit_1_2,

			Max, // num of M_INDEX
		};

		private MutexData[] _mtxs = new MutexData[(int)M_INDEX.Max];
		private bool[] _statuses = new bool[(int)M_INDEX.Max];

		public Mutector(string name)
		{
			string ident = COMMON_ID + "_" + SecurityTools.GetSHA512_128String(StringTools.ENCODING_SJIS.GetBytes(name));

			for (int index = 0; index < (int)M_INDEX.Max; index++)
				_mtxs[index] = new MutexData(ident + "_" + index);
		}

		public void Dispose()
		{
			if (_mtxs != null)
			{
				this.Clear();

				for (int index = 0; index < (int)M_INDEX.Max; index++)
				{
					_mtxs[index].Dispose();
					_mtxs[index] = null;
				}
				_mtxs = null;
				_statuses = null;
			}
		}

		public void Clear(bool status = false)
		{
			for (int index = 0; index < (int)M_INDEX.Max; index++)
				this.Set(index, status);
		}

		public void Set(int index, bool status)
		{
			if (_statuses[index] != status)
			{
				if (status)
					_mtxs[index].WaitForever();
				else
					_mtxs[index].Unlock();

				_statuses[index] = status;
			}
		}

		public bool TrySet(int index)
		{
			if (_statuses[index]) throw null; // この系は無いはず！

			if (_mtxs[index].WaitForMillis(0))
			{
				_statuses[index] = true;
				return true;
			}
			return false;
		}

		public bool Get(int index)
		{
			if (_statuses[index]) throw null; // この系は無いはず！

			if (_mtxs[index].WaitForMillis(0))
			{
				_mtxs[index].Unlock();
				return false;
			}
			return true;
		}

		public class Sender : IDisposable
		{
			private Mutector _m;

			public Sender(string name)
			{
				_m = new Mutector(name);
			}

			/// <summary>
			/// Recver.Perform() 実行中ではない -> 即例外
			/// 別の Sender 送信中 -> 終わるまで待つ。
			/// </summary>
			/// <param name="message"></param>
			public void Send(byte[] message)
			{
				if (message == null)
					throw new ArgumentNullException();

				_m.Set((int)M_INDEX.Sender, true);

				try
				{
					// Recver.Perform() 実行中かどうか検査
					{
						if (
							_m.TrySet((int)M_INDEX.Sync_0) &&
							_m.TrySet((int)M_INDEX.Sync_1) &&
							_m.TrySet((int)M_INDEX.Sync_2)
							)
							throw new Exception("recver is not running");

						_m.Set((int)M_INDEX.Sync_0, false);
						_m.Set((int)M_INDEX.Sync_1, false);
						_m.Set((int)M_INDEX.Sync_2, false);
					}

					this.SendBit(true, true);

					for (int index = 0; index < message.Length; index++)
					{
						for (int bit = 1 << 7; bit != 0; bit >>= 1)
						{
							if ((message[index] & bit) != 0)
								this.SendBit(false, true);
							else
								this.SendBit(true, false);
						}
					}
					this.SendBit(false, false);
					this.SendBit(false, false);
					this.SendBit(false, false);
				}
				finally
				{
					_m.Clear();
				}
			}

			private int _m0 = 0;
			private int _m1 = 1;

			private void SendBit(bool b0, bool b1)
			{
				_m0++;
				_m1++;
				_m0 %= 3;
				_m1 %= 3;

				_m.Set((int)M_INDEX.Sync_0 + _m1, true);
				_m.Set((int)M_INDEX.Sync_0 + _m0, false);
				_m.Set((int)M_INDEX.Bit_0_0 + _m1, b0);
				_m.Set((int)M_INDEX.Bit_1_0 + _m1, b1);
			}

			public void Dispose()
			{
				if (_m != null)
				{
					_m.Dispose();
					_m = null;
				}
			}
		}

		public class Recver : IDisposable
		{
			private Mutector _m;

			public int RecvSizeMax = 20000000;

			public Recver(string name)
			{
				_m = new Mutector(name);
			}

			private IRecver _recver = null;

			public void SetRecver(IRecver recver)
			{
				_recver = recver;
			}

			/// <summary>
			/// 別の Recver 受信中 -> 即例外
			/// </summary>
			public void Perform()
			{
				if (_recver == null)
					throw new ArgumentNullException();

				if (_m.TrySet((int)M_INDEX.Recver) == false)
					throw new Exception("already Perform() running");

				try
				{
					for (; ; )
					{
						this.RecvBit(0, 1, 2);
						this.RecvBit(1, 2, 0);
						this.RecvBit(2, 0, 1);

						if (2000 <= _elapsed)
						{
							_elapsed -= 2000;

							if (_recver.Interlude() == false)
								break;
						}
					}
				}
				finally
				{
					_m.Clear();
				}
			}

			private int _millis = 0;
			private int _elapsed = 0;

			private void RecvBit(int m0, int m1, int m2)
			{
				_m.Set((int)M_INDEX.Sync_0 + m1, true);

				// idling
				{
					if (_m.Get((int)M_INDEX.Sync_0 + m2) == false)
					{
						if (_millis < 200)
							_millis++;

						Thread.Sleep(_millis);
					}
					else
						_millis = 0;

					_elapsed += _millis + 1;
				}

				_m.Set((int)M_INDEX.Sync_0 + m0, false);

				{
					bool b0 = _m.Get((int)M_INDEX.Bit_0_0 + m1);
					bool b1 = _m.Get((int)M_INDEX.Bit_1_0 + m1);

					if (b0 && b1)
					{
						_buff = new ByteBuffer();
						_bChr = 0;
						_bIndex = 0;
					}
					else if (_buff == null)
					{
						// noop
					}
					else if (b0)
					{
						this.RecvedBit(0);
					}
					else if (b1)
					{
						this.RecvedBit(1);
					}
					else
					{
						_recver.Recved(_buff.Join());

						_buff = null;
						_bChr = -1;
						_bIndex = -1;
					}
				}
			}

			private ByteBuffer _buff = null;
			private int _bChr = -1;
			private int _bIndex = -1;

			private void RecvedBit(int bit)
			{
				_bChr <<= 1;
				_bChr |= bit;
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

			public void Dispose()
			{
				if (_m != null)
				{
					_m.Dispose();
					_m = null;
				}
			}
		}

		public interface IRecver
		{
			bool Interlude(); // ret: ? 継続する。
			void Recved(byte[] message);
		}
	}
}
