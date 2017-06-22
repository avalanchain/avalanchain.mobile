using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace avalanchain
{
	public class User
	{
		public string Name {
			get;
			set;
		}

		public string Avatar {
			get;
			set;
		}

		public User (string name, string avatar)
		{
			Name 	 = name;
			Avatar	 = avatar;
		}
	}
}

