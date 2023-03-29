using System;
using ProtectedConfigSectionV48.Configuration;

namespace ProtectedConfigSectionV48
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
				switch(args[0])
				{
				case "/P":
					ConfigReader readerP = new ConfigReader(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
					readerP.Protect("RsaProtectedConfigurationProvider");
					break;
				case "/U":
					ConfigReader readerU = new ConfigReader(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
					readerU.Unprotect();
					break;
				}
			}
		}
	}
}