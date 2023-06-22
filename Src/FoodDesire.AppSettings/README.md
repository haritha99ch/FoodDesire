# Setting Environment Variables

`FoodDesire/Src/FoodDesire.AppSettings`

1. Set SQL Connection String.

    `dotnet user-secrets set "ConnectionStrings:DefaultConnection" CONNECTIONSTRINGS_DEFAULTCONNECTION`

2. Set Azure Blob Storage Connection String (Required for hosting ML model).

    `dotnet user-secrets set "ConnectionStrings:StorageConnection" CONNECTIONSTRINGS_STORAGECONNECTION`

    [Learn more about Azure Blob Storage](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)

3. Set the Client ID for the application registered with Microsoft Identity Platform.

    `dotnet user-secrets set "ClientID" CLIENTID`

    [Learn more about registering an application with Microsoft Identity Platform](https://learn.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app)

4. Json Web Token Signing Key.

    `dotnet user-secrets set "Jwt:SignInKey" JWT_SIGNINKEY`

5. Paypal Client ID.

    `dotnet user-secrets set "PayPal:ClientId" PAYPAL_CLIENTID`

6. Paypal Secret.

    `dotnet user-secrets set "PayPal:Secret" PAYPAL_SECRET`

    [Get started with PayPal APIs](https://developer.paypal.com/api/rest/#create-or-edit-sandbox-and-live-apps)
