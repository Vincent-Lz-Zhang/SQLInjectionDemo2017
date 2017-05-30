# SQL Injection Demo 2017
This ASP.NET MVC 5 application demostrates the SQL injection vunlerability in the cases where database is accessed through direct ADO.NET and Entity Framework as well. The demo was designed to support my presentation on SQL injection at Industry Connect in May 2017.

[![SQLInjectionSlides](https://image.ibb.co/fjVuoF/SQL_Injection.png)](https://docs.google.com/presentation/d/141WIaPKYXoy06Zqf-ke79kvG-g6pJh035JGFAyWOq84/pub?start=false&loop=false&delayms=3000)

## Setup Instructions

  - Setup your SQL Server, create a database named 'SQLInjectionDemo', and run the script in `./DB/Setup.sql` file to create the table and demo data.
  - Update your db info in the connection string in `Web.config`, and the string variable in `HomeController.cs`, as shown below.


```sh
  <connectionStrings>
    <add name="SQLInjectionDemoEntities" connectionString="metadata=res://*/Models.SQLInjection.csdl|res://*/Models.SQLInjection.ssdl|res://*/Models.SQLInjection.msl;provider=System.Data.SqlClient;provider connection string='data source=<your sql server instance name>;initial catalog=SQLInjectionDemo;user id=<your user id>;password=&quot;<your password>&quot;;MultipleActiveResultSets=True;App=EntityFramework'" providerName="System.Data.EntityClient" />
  </connectionStrings>
```

```sh
string dbConnStr = "Data Source=<your sql server instance name>; Initial Catalog=SQLInjectionDemo;User ID=<your user id>;Password=<your password>";
```

Note: if you give your database other name than 'SQLInjectionDemo', you need to update it in the strings shown above.