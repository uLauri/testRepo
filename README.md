# Application was built using .NET 8 latest EntityFrameWork version. AspnetCore 8+ and .NET 8+ 
is recommended to be installed on your machine to run the project.
# Application uses sqlite db - to properly run the application, you need to run ef migrations these commands through 
Packet Manager Console and an instance of the database should be created for you:


dotnet ef migrations add InitialCreate



dotnet ef database update
