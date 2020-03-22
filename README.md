# Simple Contact Entry System
RESTfull Web API CRUD operations with ASP.NET Core 3.1 and LiteDb

## Build and Run
This simple contact entry system can be build and run from any machine that has [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed.

Navigate to the **Contact.Entry.Api** directory and run following command to launch the the web api. 

`dotnet run`

Make sure port 5001 and 5000 are available for this application to run with. If not, you can update ports in **launchSettings.json** under the **Properties** folder. 

Navigate to the **Contact.Entry.Tests** directory and run following command to run unit tests and integration tests.

`dotnet test`

You can also build and run the projects with *Visual Studio 2019* or *Visual Studio Code*.

## Storage
To minimize runtime dependencies the emmbaded NoSQL database [LiteDb](https://www.litedb.org/) has been used for data storage. You can update database from  **LiteDbOptions** section under the **appsettings.json** file.

Thank you for your time

Shahdat
