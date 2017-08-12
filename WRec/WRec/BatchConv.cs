using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using System.Windows.Forms;

namespace Charlotte
{
	public class BatchConv
	{
		private string _recFile;
		private string _batFile;

		public BatchConv(string recFile, string batFile)
		{
			_recFile = recFile;
			_batFile = batFile;
		}

		private class Word
		{
			// Kind と Prms
			//
			// MC X Y == マウスカーソル移動 (@init)
			// D VK == キー又はボタンを下げる。(@init)
			// U VK == キー又はボタンを上げる。(@init)
			// S VK == キー・ストローク
			// C VK == クリック
			// 2C VK == ダブルクリック
			// KB VK 1 == キー・下げる。(@fin)
			// KB VK 0 == キー・上げる。(@fin)
			// MB mbKind 1 == ボタン・下げる。(@fin)
			// MB mbKind 0 == ボタン・上げる。(@fin)
			// T millis == 待ち (@init/fin)

			public string Kind;
			public int[] Prms;

			public bool 改行マーク = false;

			public Word(string kind, params int[] prms)
			{
				this.Kind = kind;
				this.Prms = prms;
			}

			public bool IsSame(Word other)
			{
				if (other == null)
					return false;

				if (other.Kind != this.Kind)
					return false;

				if (ArrayTools.IsSame(other.Prms, this.Prms, IntTools.Comp) == false)
					return false;

				return true;
			}
		}

		private List<Word> _words = new List<Word>();
		private List<List<Word>> _wdTable = new List<List<Word>>();

		public void Perform()
		{
			using (StreamReader reader = new StreamReader(_recFile, StringTools.ENCODING_SJIS))
			{
				for (; ; )
				{
					string line = reader.ReadLine();

					if (line == null)
						break;

					List<string> tokens = StringTools.Tokenize(line, " ", false, true);

					for (int index = 0; index < tokens.Count; )
					{
						switch (tokens[index++])
						{
							case "M":
								_words.Add(new Word("MC", int.Parse(tokens[index]), int.Parse(tokens[index + 1])));
								index += 2;
								break;

							case "D":
								_words.Add(new Word("D", int.Parse(tokens[index++])));
								break;

							case "U":
								_words.Add(new Word("U", int.Parse(tokens[index++])));
								break;

							default:
								throw null;
						}
					}
					_words.Add(new Word("T", 10));
				}
			}

			// Kind Prms @init

			if (Gnd.I.IgnoreVk16To18)
				this.EraseIgnoreVk16To18();

			if (Gnd.I.KeyRec == Gnd.KeyRec_e.記録しない)
				this.EraseKey();

			if (Gnd.I.MouseRec == Gnd.MouseRec_e.記録しない)
				this.EraseMouse();

			this.Erase停止ボタンを押したと思しき左クリック();
			this.TrimT();
			this.OrderUD();
			this.MkClick();
			this.MkDblClick();
			this.MkStroke();
			this.ChangeT();

			if (Gnd.I.MouseRec == Gnd.MouseRec_e.ボタン操作のみ)
				this.Trimマウス移動();

			this.TrimT();
			this.SetT改行マーク();
			this.MkTrueCommand();

			if (Gnd.I.MouseRec == Gnd.MouseRec_e.ボタン操作のみ)
				this.InsertMarginBetweenMCMB();

			// Kind Prms @fin

			this.ToWdTable();

			if (Gnd.I.Recまとめ)
				this.Toまとめ();

			using (StreamWriter writer = new StreamWriter(_batFile, false, StringTools.ENCODING_SJIS))
			{
				foreach (List<Word> words in _wdTable)
				{
					if (Gnd.I.SndInputBatLocal)
						writer.Write("SndInput.exe");
					else
						writer.Write("\"" + StringTools.Combine(BootTools.SelfDir, "SndInput.exe") + "\"");

					foreach (Word word in words)
					{
						writer.Write(" ");
						writer.Write(word.Kind);

						foreach (int prm in word.Prms)
						{
							writer.Write(" ");
							writer.Write(prm.ToString());
						}
					}
					writer.Write("\r\n");
				}
			}
		}

		private void EraseIgnoreVk16To18()
		{
			int[] ignoreVks = new int[]
			{
				16, // Shift
				17, // Control
				18, // Alt
			};

			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if ((wd.Kind == "D" || wd.Kind == "U") && ignoreVks.Contains(wd.Prms[0]))
				{
					_words.RemoveAt(index);
					index--;
				}
			}
		}

		private void EraseKey()
		{
			int[] clickVk = new int[]
			{
				1, // 左クリック
				2, // 右クリック
				4, // 中央クリック
			};

			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if ((wd.Kind == "D" || wd.Kind == "U") && clickVk.Contains(wd.Prms[0]) == false)
				{
					_words.RemoveAt(index);
					index--;
				}
			}
		}

		private void EraseMouse()
		{
			int[] clickVk = new int[]
			{
				1, // 左クリック
				2, // 右クリック
				4, // 中央クリック
			};

			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "MC" || ((wd.Kind == "D" || wd.Kind == "U") && clickVk.Contains(wd.Prms[0])))
				{
					_words.RemoveAt(index);
					index--;
				}
			}
		}

		private void Erase停止ボタンを押したと思しき左クリック()
		{
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "U" && wd.Prms[0] == 1 && IsTrailT(index + 1) < 300)
				{
					_words.RemoveAt(index);
					index--;
				}
			}
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "D" && wd.Prms[0] == 1 && Isそのうち左ボタン放す(index + 1) == false)
				{
					_words.RemoveAt(index);
					index--;
				}
			}
		}

		private int IsTrailT(int startPos)
		{
			int totalT = 0;

			for (int index = startPos; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "T")
					totalT += wd.Prms[0];
			}
			return totalT;
		}

		private bool Isそのうち左ボタン放す(int startPos)
		{
			for (int index = startPos; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "U" && wd.Prms[0] == 1)
					return true;
			}
			return false;
		}

		private void TrimT()
		{
			for (int index = 1; index < _words.Count; index++)
			{
				Word wd1 = _words[index - 1];
				Word wd2 = _words[index];

				if (wd1.Kind == "T" && wd2.Kind == "T")
				{
					wd1.Prms[0] += wd2.Prms[0];
					_words.RemoveAt(index);
					index--;
				}
			}
		}

		private void OrderUD()
		{
			for (int index = 1; index < _words.Count; index++)
			{
				Word wd1 = _words[index - 1];
				Word wd2 = _words[index];

				if (
					(wd1.Kind == "D" && wd2.Kind == "U") ||
					(wd1.Kind == "D" && wd2.Kind == "D" && IsShiftVk(wd1) == false && IsShiftVk(wd2)) ||
					(wd1.Kind == "U" && wd2.Kind == "U" && IsShiftVk(wd1) && IsShiftVk(wd2) == false)
					)
				{
					ArrayTools.Swap(_words, index - 1, index);

					if (2 <= index)
						index -= 2;
				}
			}
		}

		private bool IsShiftVk(Word ot)
		{
			int[] shiftVks = new int[]
			{
				16, // Shift
				160, // L Shift
				161, // R Shift
				17, // Control
				162, // L Control
				163, // R Control
				18, // Alt
				164, // L Alt
				165, // R Alt
				240, // CapsLock
				91, // Windows
			};

			return shiftVks.Contains(ot.Prms[0]);
		}

		private int FindT(int tMillis, int startPos, Word target) // ret: -1 == not found
		{
			if (tMillis <= 0) // 即タイムアウト
				return -1;

			int elapsed = 0;

			for (int index = startPos; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "T")
				{
					elapsed += wd.Prms[0];

					if (tMillis < elapsed) // ? タイムアウト
						break;
				}
				else if (wd.IsSame(target))
				{
					return index;
				}
			}
			return -1;
		}

		private void MkClick()
		{
			int[] clickVk = new int[]
			{
				1, // 左クリック
				2, // 右クリック
				4, // 中央クリック
			};

			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "D" && clickVk.Contains(wd.Prms[0]))
				{
					int endIndex = FindT(Gnd.I.RecStrokeMillis, index + 1, new Word("U", wd.Prms));

					if (endIndex != -1)
					{
						wd.Kind = "C";
						_words.RemoveAt(endIndex);
					}
				}
			}
		}

		private void MkDblClick()
		{
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "C")
				{
					int endIndex = FindT(Gnd.I.RecDblClickMillis, index + 1, wd);

					if (endIndex != -1)
					{
						wd.Kind = "2C";
						_words.RemoveAt(endIndex);
					}
				}
			}
		}

		private void MkStroke()
		{
			int[] ignoreVks = new int[]
			{
				16, // Shift
				160, // L Shift
				161, // R Shift
				17, // Control
				162, // L Control
				163, // R Control
				18, // Alt
				164, // L Alt
				165, // R Alt
				240, // CapsLock
				243, // IME on
				244, // IME off
			};

			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "D" && ignoreVks.Contains(wd.Prms[0]) == false)
				{
					int endIndex = FindT(Gnd.I.RecStrokeMillis, index + 1, new Word("U", wd.Prms));

					if (endIndex != -1)
					{
						wd.Kind = "S";
						_words.RemoveAt(endIndex);
					}
				}
			}
		}

		private void ChangeT()
		{
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "T")
				{
					wd.Prms[0] /= 10;
					wd.Prms[0] *= Gnd.I.SamplingMillis;
				}
			}
		}

		private void Trimマウス移動()
		{
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "MC" && Is次のマウス移動までにボタン操作がある(index + 1) == false)
				{
					_words.RemoveAt(index);
					index--;
				}
			}
		}

		private bool Is次のマウス移動までにボタン操作がある(int startPos)
		{
			for (int index = startPos; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "MC")
					break;

				if (wd.Kind == "C") return true;
				if (wd.Kind == "2C") return true;
				if (wd.Kind == "D" && IsMouseButtonVk(wd.Prms[0])) return true;
				if (wd.Kind == "U" && IsMouseButtonVk(wd.Prms[0])) return true;
			}
			return false;
		}

		private bool IsMouseButtonVk(int vk)
		{
			return vk == 1 || vk == 2 || vk == 4;
		}

		private void SetT改行マーク()
		{
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				if (wd.Kind == "T")
					wd.改行マーク = true;
			}
		}

		private void MkTrueCommand()
		{
			for (int index = 0; index < _words.Count; index++)
			{
				Word wd = _words[index];

				switch (wd.Kind)
				{
					case "D":
						switch (wd.Prms[0])
						{
							case 1:
							case 2:
							case 4:
								wd = new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 1);
								break;

							default:
								wd = new Word("KB", wd.Prms[0], 1);
								break;
						}
						_words[index] = wd;
						break;

					case "U":
						switch (wd.Prms[0])
						{
							case 1:
							case 2:
							case 4:
								wd = new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 0);
								break;

							default:
								wd = new Word("KB", wd.Prms[0], 0);
								break;
						}
						_words[index] = wd;
						break;

					case "C":
						_words.RemoveAt(index);
						_words.InsertRange(index, new Word[]
						{
							new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 1),
							new Word("T", Gnd.I.ClickMillis),
							new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 0),
						});
						break;

					case "2C":
						_words.RemoveAt(index);
						_words.InsertRange(index, new Word[]
						{
							new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 1),
							new Word("T", Gnd.I.ClickMillis),
							new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 0),
							new Word("T", Gnd.I.DblClickMillis),
							new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 1),
							new Word("T", Gnd.I.ClickMillis),
							new Word("MB", VkToマウスボタンKind(wd.Prms[0]), 0),
						});
						break;

					case "S":
						_words.RemoveAt(index);
						_words.InsertRange(index, new Word[]
						{
							new Word("KB", wd.Prms[0], 1),
							new Word("T", Gnd.I.StrokeMillis),
							new Word("KB", wd.Prms[0], 0),
						});
						break;

					default:
						break;
				}
			}
		}

		private int VkToマウスボタンKind(int vk)
		{
			switch (vk)
			{
				case 1: return 1; // 左ボタン
				case 2: return 3; // 右ボタン
				case 4: return 2; // 中央ボタン
			}
			throw null;
		}

		private void InsertMarginBetweenMCMB()
		{
			for (int index = 1; index < _words.Count; index++)
			{
				Word wd1 = _words[index - 1];
				Word wd2 = _words[index];

				// MC MB はあるけど、MB MC は無いような気がする。
				if (
					(wd1.Kind == "MC" && wd2.Kind == "MB") ||
					(wd1.Kind == "MB" && wd2.Kind == "MC")
					)
				{
					_words.Insert(index, new Word("T", 5)); // insert margin
					index++;
				}
			}
		}

		private void ToWdTable()
		{
			List<Word> row = null;

			foreach (Word word in _words)
			{
				if (row == null)
				{
					row = new List<Word>();
					_wdTable.Add(row);
				}
				row.Add(word);

				if (word.改行マーク)
					row = null;
			}
			_words = null; // もう使わない。
		}

		private void Toまとめ()
		{
			const int WORD_MAX = 100;

			for (int c = 0; c < 10; c++) // 2bs -- 多分このループ要らない。
			{
				for (int index = 1; index < _wdTable.Count; index++)
				{
					List<Word> row1 = _wdTable[index - 1];
					List<Word> row2 = _wdTable[index];

					if (row1.Count + row2.Count <= WORD_MAX)
					{
						row1.AddRange(row2);
						_wdTable.RemoveAt(index);
						index--;
					}
				}
			}
		}
	}
}
