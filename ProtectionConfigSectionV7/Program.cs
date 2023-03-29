using System;
using System.Configuration;
using ProtectedConfigSectionV7.Configuration;

namespace ProtectedConfigSectionV7
{
	class Program
	{
		static void Main(string[] args)
		{
			ConfigReader reader = new ConfigReader();
			Console.WriteLine($"Name={reader.Section.SectionInformation.Name} IsProtected={reader.Section.SectionInformation.IsProtected} IsLocked={reader.Section.SectionInformation.IsLocked}.");
			Console.WriteLine($"Setting Values: Key={reader.Section.Key} Value={reader.Section.Value}");

			if(args.Length > 0)
			{
				var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var configFilePath = conf.FilePath;
				switch(args[0])
				{
				case "/P":
					ConfigReader readerP = new ConfigReader(configFilePath);
					readerP.Protect("DataProtectionConfigurationProvider");
					break;
				case "/U":
					ConfigReader readerU = new ConfigReader(configFilePath);
					readerU.Unprotect();
					break;
				}
			}
		}
	}
}
