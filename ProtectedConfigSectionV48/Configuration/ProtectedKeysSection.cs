using System;
using System.Configuration;

namespace ProtectedConfigSectionV48.Configuration
{
	public class ProtectedKeysSection : ConfigurationSection
	{
		public const String Name = "ProtectedKeysSection";
		private const String KEY = "key";
		private const String VALUE = "value";

		[ConfigurationProperty(KEY, IsRequired = true)]
		public String Key
		{
			get { return (String)base[KEY]; }
			set { base[KEY] = value; }
		}

		[ConfigurationProperty(VALUE, IsRequired = true)]
		public String Value
		{
			get { return (String)base[VALUE]; }
			set { base[value] = value; }
		}
	}
}