using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Charlotte.Tools
{
	public class HttpRequest
	{
		public const string HTTP_10 = "HTTP/1.0";
		public const string HTTP_11 = "HTTP/1.1";

		private string _domain = "localhost";
		private int _portNo = 80;
		private string _path = "/";
		private string _version = HTTP_11;
		private Dictionary<string, string> _headerFields = new Dictionary<string, string>();
		private byte[] _body = null; // null -> GET, not null -> POST
		private int _sendTimeoutMillis = 20000; // 0 -> infinite
		private int _recvTimeoutMillis = 20000; // 0 -> infinite
		private string _proxyDomain = null; // null -> no proxy
		private int _proxyPortNo = -1;
		private bool _useIEProxy = false;
		private bool _head; // true -> HEAD, false -> GET or POST

		public HttpRequest()
		{ }

		public HttpRequest(string url)
		{
			this.SetUrl(url);
		}

		public int ResBodySizeMax = 20000000; // 20 MB

		public void SetUrl(string url)
		{
			if (url.StartsWith("https://"))
			{
				throw new Exception("not compatible with HTTPS!");
			}
			if (url.StartsWith("http://"))
			{
				url = url.Substring(7);
			}
			int index = url.IndexOf('/');

			if (index != -1)
			{
				_domain = url.Substring(0, index);
				_path = url.Substring(index);
			}
			else
			{
				_domain = url;
				_path = "/";
			}
			index = _domain.IndexOf(':');

			if (index != -1)
			{
				_portNo = int.Parse(_domain.Substring(index + 1));
				_domain = url.Substring(0, index);
			}
			else
			{
				_portNo = 80;
			}
		}

		public void SetDomain(string domain)
		{
			_domain = domain;
		}

		public void SetPortNo(int portNo)
		{
			_portNo = portNo;
		}

		public void setPath(string path)
		{
			_path = path;
		}

		public void SetVersion(string version)
		{
			_version = version;
		}

		public void SetAuthorization(string user, string password)
		{
			String plain = user + ":" + password;
			String enc = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
			this.SetHeaderField("Authorization", "Basic " + enc);
		}

		public void SetHeaderField(string name, string value)
		{
			_headerFields.Add(name, value);
		}

		public void SetBody(byte[] body)
		{
			_body = body;
		}

		public void SetSendTimeoutMillis(int millis)
		{
			_sendTimeoutMillis = millis;
		}

		public void SetRecvTimeoutMillis(int millis)
		{
			_recvTimeoutMillis = millis;
		}

		public void SetProxy(string domain, int portNo)
		{
			_proxyDomain = domain;
			_proxyPortNo = portNo;
		}

		public void SetIEProxy()
		{
			_useIEProxy = true;
		}

		public void setHeadFlag(bool head)
		{
			_head = head;
		}

		public HttpResponse Head()
		{
			_body = null;
			_head = true;
			return this.Perform();
		}

		public HttpResponse Get()
		{
			return this.Post(null);
		}

		public HttpResponse Post(byte[] body)
		{
			_body = body;
			_head = false;
			return this.Perform();
		}

		public HttpResponse Perform()
		{
			if (_version != HTTP_10)
			{
				if (_portNo == 80)
				{
					this.SetHeaderField("Host", _domain);
				}
				else
				{
					this.SetHeaderField("Host", _domain + ":" + _portNo);
				}
			}
			if (_body != null)
			{
				this.SetHeaderField("Content-Length", "" + _body.Length);
			}
			this.SetHeaderField("Connection", "close");

			if (_useIEProxy)
			{
				Uri proxy = WebRequest.GetSystemWebProxy().GetProxy(new Uri(this.GetUrl()));

				if (_domain != proxy.Host || _portNo != proxy.Port)
				{
					_proxyDomain = proxy.Host;
					_proxyPortNo = proxy.Port;
				}
			}
			using (TcpClient client = _proxyDomain == null ?
				new TcpClient(_domain, _portNo) :
				new TcpClient(_proxyDomain, _proxyPortNo)
				)
			{
				client.SendTimeout = _sendTimeoutMillis;
				client.ReceiveTimeout = _recvTimeoutMillis;

				using (NetworkStream ns = client.GetStream())
				{
					if (_head)
					{
						Write(ns, "HEAD ");
					}
					else if (_body == null)
					{
						Write(ns, "GET ");
					}
					else
					{
						Write(ns, "POST ");
					}
					if (_proxyDomain == null)
					{
						Write(ns, _path);
					}
					else if (_proxyPortNo == 80)
					{
						Write(ns, "http://" + _domain + _path);
					}
					else
					{
						Write(ns, "http://" + _domain + ":" + _portNo + _path);
					}
					Write(ns, " ");
					Write(ns, _version);
					Write(ns, "\r\n");

					foreach (string name in _headerFields.Keys)
					{
						String value = _headerFields[name];

						if (value.Contains('\n'))
							value = string.Join("\r\n\t", StringTools.Tokenize(value, "\r\n", false, true));

						Write(ns, name);
						Write(ns, ": ");
						Write(ns, value);
						Write(ns, "\r\n");
					}
					Write(ns, "\r\n");

					if (_body != null)
					{
						Write(ns, _body);
					}
					return new HttpResponse(ns, this.ResBodySizeMax, _head);
				}
			}
		}

		private string GetUrl()
		{
			return "http://" + _domain + ":" + _portNo + _path;
		}

		private static void Write(NetworkStream ns, string str)
		{
			Write(ns, Encoding.ASCII.GetBytes(str));
		}

		private static void Write(NetworkStream ns, byte[] bytes)
		{
			ns.Write(bytes, 0, bytes.Length);
		}
	}
}
