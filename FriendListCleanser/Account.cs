using System;
using System.Runtime.CompilerServices;

namespace FriendListCleanser
{
	public class Account
	{
		public int FriendCount
		{
			get;
			set;
		}

		public string Pass
		{
			get;
			set;
		}

		public string Status
		{
			get;
			set;
		}

		public string User
		{
			get;
			set;
		}

		public Account(string user, string pass, int friendCount, string status)
		{
			this.User = user;
			this.Pass = pass;
			this.FriendCount = friendCount;
			this.Status = status;
		}
	}
}