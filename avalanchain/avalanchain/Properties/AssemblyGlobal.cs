using System;
namespace avalanchain
{
	public static class AssemblyGlobal
	{
		public const string Company = "Avalanchain";

		public const string ProductLine = "Avalanchain";

		public const string Year = "2017";

		public const string Copyright = Company + " - " + Year;

#if DEBUG
		public const string Configuration = "Debug";
#elif RELEASE
		public const string Configuration = "Release";
#else
		public const string Configuration = "Unkown";
#endif
	}
}
