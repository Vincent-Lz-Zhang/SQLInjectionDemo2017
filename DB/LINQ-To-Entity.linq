<Query Kind="Expression">
  <Connection>
    <ID>07951922-7123-4591-a586-c621cd86209a</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>D:\workcomplex\aspnet\SQLInjectionDemo\SQLInjectionDemo\bin\SQLInjectionDemo.dll</CustomAssemblyPath>
    <CustomTypeName>SQLInjectionDemo.Models.SQLInjectionDemoEntities</CustomTypeName>
    <AppConfigPath>D:\workcomplex\aspnet\SQLInjectionDemo\SQLInjectionDemo\bin\SQLInjectionDemo.dll.config</AppConfigPath>
  </Connection>
</Query>

Admins.Where(x => x.Email == "xx@xxx' OR 1=1 --'" && x.Password == "xxx").Single()