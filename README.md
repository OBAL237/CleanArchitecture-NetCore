# CQRS, .NET Core, MediatR, Clean Aechitecture.
### *Sample .NET Core REST API CQRS implementation using Clean Architecture*
> **Tech Stack: .NET Core, Swagger, AutoMapper, MediatR, EntityFrameworkCore, NSubstitute, MSTest**
## Découpage projet:
### Backend.API : 
#### Dépendances:
##### Packages:
```
<PackageReference Include="Microsoft.VisualStudio.Azure.Containers.Tools.Targets" Version="1.11.1" />
<PackageReference Include="Swashbuckle.AspNetCore.Swagger" Version="6.2.3" />
<PackageReference Include="Swashbuckle.AspNetCore.SwaggerGen" Version="6.2.3" />
<PackageReference Include="Swashbuckle.AspNetCore.SwaggerUI" Version="6.2.3" />
```
##### Projets:
```
<ProjectReference Include="..\Backend.ApplicationCore\Backend.ApplicationCore.csproj" />
<ProjectReference Include="..\Backend.Infrastructure\Backend.Infrastructure.csproj" />
<ProjectReference Include="..\Backend.Persistence\Backend.Persistence.csproj" />
```
### Backend.ApplicationCore: 
#### Dépendences:
##### Packages:
```
<PackageReference Include="AutoMapper" Version="10.1.1" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="8.1.1" />
<PackageReference Include="MediatR" Version="7.0.0" />
<PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="7.0.0" />
<PackageReference Include="Microsoft.AspNetCore.Http" Version="2.2.2" />
<PackageReference Include="Microsoft.AspNetCore.Http.Abstractions" Version="2.2.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.13" />
```
##### Projets: 
`<ProjectReference Include="..\Backend.Domain\Backend.Domain.csproj" />`
### Backend.Domain : 
#### Dépendences:
##### Packages: Nothing
##### Projets: Nothing
### Backend.Infrastructure : 
#### Dépendences:
##### Packages: Nothing
##### Projets:
```
<ProjectReference Include="..\Backend.ApplicationCore\Backend.ApplicationCore.csproj" />
<ProjectReference Include="..\Backend.Domain\Backend.Domain.csproj" />
```
### Backend.Persistence : 
#### Dépendences:
##### Packages:
```
 <PackageReference Include="Microsoft.AspNet.Identity.EntityFramework" Version="2.2.3" />
 <PackageReference Include="Microsoft.AspNetCore.Http.Abstractions" Version="2.2.0" />
 <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="5.0.10" />
 <PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.13" />
 <PackageReference Include="Microsoft.Extensions.Configuration.Abstractions" Version="6.0.0" />
 <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="6.0.0" />
 <PackageReference Include="MySql.Data.EntityFrameworkCore" Version="8.0.22" />
 <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="5.0.0-alpha.2" />
 <PackageReference Include="System.Data.SqlClient" Version="4.8.3" />
```
##### Projets:
` <ProjectReference Include="..\Backend.ApplicationCore\Backend.ApplicationCore.csproj" />`
### Backend.MSTest : 
#### Dépendences:
##### Packages:
```
 <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.9.4" />
 <PackageReference Include="MSTest.TestAdapter" Version="2.2.3" />
 <PackageReference Include="MSTest.TestFramework" Version="2.2.3" />
 <PackageReference Include="coverlet.collector" Version="3.0.2" />
 <PackageReference Include="NSubstitute" Version="4.2.2" />
```
##### Projets:
```
 <ProjectReference Include="..\Backend.ApplicationCore\Backend.ApplicationCore.csproj" />
 <ProjectReference Include="..\Backend.Infrastructure\Backend.Infrastructure.csproj" />
```
