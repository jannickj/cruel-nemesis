using System;
using System.IO;

namespace XmasEngineExtensions.LoggerExtension
{
	public class Logger
	{
		public Func<DebugLevel,StreamWriter> StreamSelector { get; protected set; }

		public Logger (StreamWriter logstream, DebugLevel MaxDebugLevel)
		{
			StreamSelector = (dl => (dl <= MaxDebugLevel) ? logstream : null);
		}

		public Logger (Func<DebugLevel, StreamWriter> streamSelector)
		{
			this.StreamSelector = streamSelector;
		}

		public void LogStringWithTimeStamp (string str, DebugLevel debugLevel)
		{
			StreamWriter stream = StreamSelector (debugLevel);
			if (stream != null) {
				stream.WriteLine ("{0} : [{1}] {2}", TimeStamp(), debugLevel.ToString(), str);
				stream.Flush ();
			}
		}
		
		internal string TimeStamp()
		{
			return DateTime.Now.ToString ("[dd/MM-yyyy HH:mm:ss]");
		}
	}
}

