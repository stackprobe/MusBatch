using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ByteBuffer
	{
		private List<byte[]> Parts = new List<byte[]>();
		private byte[] Buff = null;
		private int Index = -1;
		private int TotalSize = 0;
		private int BuffSize = 16;

		public int Length
		{
			get
			{
				return this.TotalSize;
			}
		}

		public void Clear()
		{
			this.Parts.Clear();
			this.Buff = null;
			this.TotalSize = 0;
		}

		public void Add(byte chr)
		{
			if (this.Buff == null)
			{
				this.Buff = new byte[this.BuffSize];
				this.Index = 0;
			}
			this.Buff[this.Index] = chr;
			this.Index++;

			if (this.BuffSize <= this.Index)
			{
				this.Parts.Add(this.Buff);
				this.Buff = null;

				if (this.BuffSize < 0x200000) // < 2 MB
					this.BuffSize *= 2;
			}
			this.TotalSize++;
		}

		public byte[] Join()
		{
			byte[] dest = new byte[this.TotalSize];
			int wPos = 0;

			foreach (byte[] part in this.Parts)
			{
				Array.Copy(part, 0, dest, wPos, part.Length);
				wPos += part.Length;
			}
			if (this.Buff != null)
			{
				Array.Copy(this.Buff, 0, dest, wPos, this.Index);
				wPos += this.Index;
			}
			if (this.TotalSize != wPos) throw null; // 2bs
			return dest;
		}
	}
}
