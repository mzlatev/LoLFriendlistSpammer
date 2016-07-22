using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FriendListCleanser.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		private static System.Resources.ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap logo_v2
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_v2", Resources.resourceCulture);
			}
		}

		internal static Bitmap Paypal_button1
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Paypal_button1", Resources.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Resources.resourceMan = new System.Resources.ResourceManager("FriendListCleanser.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		internal static Bitmap Soft_White_Text_Effect
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Soft White Text Effect", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}