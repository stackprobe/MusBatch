using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Charlotte.Tools
{
	public class XNode
	{
		public string Name;
		public string Value;
		public List<XNode> Children = new List<XNode>();

		public XNode()
			: this(null)
		{ }

		public XNode(string name)
			: this(name, null)
		{ }

		public XNode(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		public static XNode Load(string xmlFile)
		{
			XNode node = new XNode();
			Stack<XNode> parents = new Stack<XNode>();

			using (XmlReader reader = XmlReader.Create(xmlFile))
			{
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							{
								XNode child = new XNode(reader.LocalName);

								node.Children.Add(child);
								parents.Push(node);
								node = child;

								bool singleTag = reader.IsEmptyElement;

								while (reader.MoveToNextAttribute())
									node.Children.Add(new XNode(reader.Name, reader.Value));

								if (singleTag)
									node = parents.Pop();
							}
							break;

						case XmlNodeType.Text:
							node.Value = reader.Value;
							break;

						case XmlNodeType.EndElement:
							node = parents.Pop();
							break;

						default:
							break;
					}
				}
			}
			if (parents.Count != 0)
				throw new Exception("Xmlフォーマットエラー：ルートタグに復帰できません。");

			if (node.Children.Count != 1)
				throw new Exception("Xmlフォーマットエラー：ルートタグの個数に問題が有ります。[" + node.Children.Count + "]");

			node = node.Children[0];
			PostLoad(node);
			return node;
		}

		public static void PostLoad(XNode node)
		{
			node.Name = NValFltr(node.Name);
			node.Value = NValFltr(node.Value);

			{
				int clnPos = node.Name.IndexOf(':');

				if (clnPos != -1)
					node.Name = node.Name.Substring(clnPos + 1);
			}

			foreach (XNode child in node.Children)
				PostLoad(child);
		}

		public static string NValFltr(string value)
		{
			if (value == null)
				value = "";

			return value.Trim();
		}

		public void Save(string xmlFile)
		{
			File.WriteAllText(xmlFile, GetString(), Encoding.UTF8);
		}

		private const string SAVE_INDENT = "\t";
		private const string SAVE_NEW_LINE = "\n";

		public string GetString()
		{
			StringBuilder buff = new StringBuilder();
			buff.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			buff.Append(SAVE_NEW_LINE);
			this.AddTo(buff, "");
			return buff.ToString();
		}

		private void AddTo(StringBuilder buff, String indent)
		{
			string name = this.Name;
			string value = this.Value;

			if (string.IsNullOrEmpty(name))
				name = "node";

			if (this.Children.Count != 0)
			{
				buff.Append(indent);
				buff.Append("<");
				buff.Append(name);
				buff.Append(">");
				buff.Append(SAVE_NEW_LINE);

				foreach (XNode child in this.Children)
				{
					child.AddTo(buff, indent + SAVE_INDENT);
				}
				if (string.IsNullOrEmpty(value) == false)
				{
					buff.Append(indent);
					buff.Append(SAVE_INDENT);
					buff.Append(value);
					buff.Append(SAVE_NEW_LINE);
				}
				buff.Append(indent);
				buff.Append("</");
				buff.Append(name);
				buff.Append(">");
				buff.Append(SAVE_NEW_LINE);
			}
			else if (string.IsNullOrEmpty(value) == false)
			{
				buff.Append(indent);
				buff.Append("<");
				buff.Append(name);
				buff.Append(">");
				buff.Append(value);
				buff.Append("</");
				buff.Append(name);
				buff.Append(">");
				buff.Append(SAVE_NEW_LINE);
			}
			else
			{
				buff.Append(indent);
				buff.Append("<");
				buff.Append(name);
				buff.Append("/>");
				buff.Append(SAVE_NEW_LINE);
			}
		}

		private const string PATH_DLMTRS = "/\\";

		public List<XNode> GetNodes(string path)
		{
			List<XNode> ret = new List<XNode>();
			this.CollectNodes(StringTools.Tokenize(path, PATH_DLMTRS, false, true), 0, ret);
			return ret;
		}

		private void CollectNodes(List<string> pathTokens, int ptIndex, List<XNode> dest)
		{
			if (ptIndex < pathTokens.Count)
			{
				foreach (XNode child in this.Children)
					if (child.Name == pathTokens[ptIndex])
						child.CollectNodes(pathTokens, ptIndex + 1, dest);
			}
			else
				dest.Add(this);
		}

		public XNode GetNode(string path)
		{
			List<XNode> ret = this.GetNodes(path);

			if (ret.Count < 1)
				throw new Exception("Xmlフォーマットエラー：パスが見つかりません。[" + path + "]");

			if (1 < ret.Count)
				throw new Exception("Xmlフォーマットエラー：パスを特定出来ません。[" + path + "]");

			return ret[0];
		}
	}
}
