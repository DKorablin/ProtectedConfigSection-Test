using System;
using System.Configuration;

namespace ProtectedConfigSectionV48.Configuration
{
	internal class ConfigReader
	{
		public ProtectedKeysSection Section { get; set; }
		public ConfigReader()
			: this(null)
		{
		}
		public ConfigReader(String path)
		{
			if(path == null)
				Section = (ProtectedKeysSection)ConfigurationManager.GetSection(ProtectedKeysSection.Name);
			else
			{
				System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
				{
					ExeConfigFilename = path,
				}, ConfigurationUserLevel.None);
				Section = (ProtectedKeysSection)configuration.GetSection(ProtectedKeysSection.Name);
			}
		}

		public void Protect(String protectionProvider)
		{
			if(Section.SectionInformation.IsProtected)
				return;
			//if(Section.SectionInformation.IsLocked)

			Section.SectionInformation.ProtectSection(protectionProvider);
			Section.SectionInformation.ForceSave = true;
			Section.CurrentConfiguration.Save(ConfigurationSaveMode.Full);
		}
		public void Unprotect()
		{
			if(!Section.SectionInformation.IsProtected)
				return;
			//if(Section.SectionInformation.IsLocked)

			Section.SectionInformation.UnprotectSection();
			Section.SectionInformation.ForceSave = true;
			Section.CurrentConfiguration.Save(ConfigurationSaveMode.Full);
		}
	}
}