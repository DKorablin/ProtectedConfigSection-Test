# Protected Config Section sample application
This application is made to show how to create custom App/Web.config section and protect it in .NET Framework and .NET 7+ applications.
Sample RSA-keys.xml added to project as a samle, do not use it in prodution environment.

For .NET Framework we can use RsaProtectedConfigurationProvider or other supported providers. All RSA keys are stored at:
>C:\Users\All Users\Microsoft\Crypto\RSA\MachineKeys

## How to create RSA key container
- **aspnet_regiis -pc "MyEncryptionKey" -exp** - Create key container and make it exportable (If you need to transfer it to another machine or store it in secure vault)
- For Web applications: **aspnet_regiis -pa "MyEncryptionKey" "NT AUTHORITY\NETWORK SERVICE"** - Grant access to the key container for the account under whitch the website is running
- **aspnet_regiis -px "MyEncryptionKey" c:\RSA-keys.xml -pri** - Exporting key container to import it to another machine or store it in secure vault for security (with private key [-pri])

## How to import RSA key container to different machine
- **aspnet_regiis -pi "MyEncryptionKey" c:\RSA-keys.xml** - Import previosly created key to another or new machine
- For Web applications: **aspnet_regiis -pa "MyEncryptionKey" "NT AUTHORITY\NETWORK SERVICE"** - Grant access to the key container for the account under whitch the website is running

## Possible errors
- *Error while decoding OAEP padding* - Private key was not exported from target machine
- *Object already exists.* - Administration privilege is required to protect section from application (Otherwise use **aspnet_regiis -pe** to protect section)
- *The RSA key container could not be opened* - Access denied to RSA keys or keys are not installed
- *Operation is not supported on this platform.* - In .NET Core and .NET 5+, the RsaProtectedConfigurationProvider and other providers [are not supported](https://learn.microsoft.com/en-us/dotnet/api/system.configuration.rsaprotectedconfigurationprovider). In .NET 7+ only DataProtectionConfigurationProvider is supported out of the box.

## Limitations
To access xml configuration sections in .NET Core applications we can use NuGet package: [System.Configuration.ConfigurationManager](https://www.nuget.org/packages/System.Configuration.ConfigurationManager)
but for .NET Core till .NET 7 all ProtectedConfigurationProvider are not supported.
From .NET 7+ RsaProtectedConfigurationProvider is not supported.

RsaProtectedConfigurationProvider and DataProtectionConfigurationProvider [is not supported](https://learn.microsoft.com/en-us/answers/questions/274041/dataprotectionconfigurationprovider-not-working-on) in Azure Web site & web job [solution with Pkcs12Provider](https://www.fatalerrors.org/a/config-file-encryption-method-for-azure-web-site-and-web-job.html).
