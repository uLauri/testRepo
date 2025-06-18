# Application was built using .NET 8 latest EntityFrameWork version. AspnetCore 8+ and .NET 8+ 
is recommended to be installed on your machine to run the project.
# Application uses sqlite db.

#Setup:
* run "dotnet ef database update" before running the application.

#Testing main endpoint:

*use "user" and "password" through SwaggerUI login endpoint to retrieve a bearer token.

*add received token before using the "calculate-fee" endpoint to authorize acces.
