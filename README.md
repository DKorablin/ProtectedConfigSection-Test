# Protected Config Section sample application
This application is made to show how to create custom App/Web.config section and protect it in .NET Framework and .NET 7+ applications.

To access xml configuration sections in .NET Core applications we can use NuGet package: [System.Configuration.ConfigurationManager](https://www.nuget.org/packages/System.Configuration.ConfigurationManager)

For .NET Framework we can use RsaProtectedConfigurationProvider or other supported providers. All RSA keys are stored at:
>C:\Users\All Users\Microsoft\Crypto\RSA\MachineKeys
For .NET Core till .NET 7 all ProtectedConfigurationProvider

## How to create key container
- **aspnet_regiis -pc "MyEncryptionKey" -exp** - Create key container and make it exportable (If you need to transfer it to another machine or store it in secure vault)
- For Web applications: **aspnet_regiis -pa "MyEncryptionKey" "NT AUTHORITY\NETWORK SERVICE"** - Grant access to the key container for the account under whitch the website is running
- **aspnet_regiis -px "MyEncryptionKey" c:\RSA-keys.xml -pri** - Exporting key container to import it to another machine or store it in secure vault for security (with private key [-pri])

## How to import key to different machine
- **aspnet_regiis -pi "MyEncryptionKey" c:\RSA-keys.xml** - Import previosly created key to another or new machine
- For Web applications: **aspnet_regiis -pa "MyEncryptionKey" "NT AUTHORITY\NETWORK SERVICE"** - Grant access to the key container for the account under whitch the website is running

## Possible errors
- *Error while decoding OAEP padding* - Private key was not exported from target machine
- *Object already exists.* - Administration privilege is required to protect section from application (Otherwise use **aspnet_regiis -pe** to protect section)
- *The RSA key container could not be opened* - Access denied to RSA keys or keys are not installed
- *Operation is not supported on this platform.* - On .NET Core and .NET 5+, the RsaProtectedConfigurationProvider and other providers [are not supported](https://learn.microsoft.com/en-us/dotnet/api/system.configuration.rsaprotectedconfigurationprovider). In .NET 7+ only DataProtectionConfigurationProvider is supported.