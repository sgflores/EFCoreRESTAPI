
# EFCoreRestAPI

REST CRUD Example using EF Core with Blazor integration

## Code Coverage
1. Entity Framework with DB Migration and seeder
2. BackEnd REST API with Controllers
3. JWT
4. Blazor Frontend
5. Run multiple project in one solution (FrontEnd Blazor / Backend REST API)

## Project BackEnd API Setup
1. Create an ASP.NET Core REST API application
	Follow these steps to create an ASP.NET Core application in Visual Studio 2019:

	Step 1: Go to File > New, and then select Project.

	Step 2: Choose Create a new project.

	Step 3: Select ASP.NET Core Web Application template.

	Step 4:  Enter the Project name, and then click Create. The Project template dialog will be displayed.

	Step 5: Select .NET Core, ASP.NET Core 3.1, and API template.
	Step 6: Click Create. The sample ASP.NET Core API application will be created.
	
2. Install necessary NuGet packages
	Microsoft.EntityFrameworkCore
	Microsoft.EntityFrameworkCore.Design
	Microsoft.EntityFrameworkCore.SqlServer
	Microsoft.EntityFrameworkCore.Tools
	Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 3.1.0
	Install-Package System.IdentityModel.Tokens.Jwt -Version 5.6.0
	Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 3.1.0

3. Data Migrations
4. Create Model Classes (Products, UserInfo) (will be used as our tables in migration)
5. Create ApplicationDbContext class to be used for migration

	    public class InventoryContext : DbContext
	    {
	      public InventoryContext(DbContextOptions<InventoryContext> options)
	         : base(options)
	      {

	      }

	      public DbSet<Products> Products { get; set; }

	      public DbSet<UserInfo> UserInfo { get; set; }
	    }
6. create default connection string in appsettings.json
  
		  "ConnectionStrings": {
		    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Inventory;Trusted_Connection=True;MultipleActiveResultSets=true"
		  }
7. bind ApplicationDbContext to Startup.cs
	
		public void ConfigureServices(IServiceCollection services)
		{
				services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
				services.AddControllers();
		}
	
7. run initial migration
		
		Step 1: open package manager console
		Step 2: run Add-Migration Initial
		Step 3: run Update-Database
	
9. Build REST APIs Controller
	Step 1: Right–click the Controllers folder, choose Add, and then click Controller.

	Step 2: Select API Controller with actions using the Entity Framework template.

	Step 3: Choose the Products model class and InventoryContext context class, and then name the control ProductsController.
	
10. Change launch url in launchSettings.js under properties section
	
		"launchUrl": "api/products",
	
11. Configure JWT
	Step 1: Create an empty API controller called TokenController.

	Step 2: Paste the below JWT configuration into the appsetting.json file.
		
		  "Jwt": {
			"Key": "sdfsdgsfsdfdffg22323cvfgdfg",
			"Issuer": "InventoryAuthenticationServer",
			"Audience": "InventoryServicePostmantClient",
			"Subject":  "InventoryServiceAccessToken"
		  }
	  
	Step 3: Add the action method under TokenController to perform the following operations:

		Accept username and password as input.
		Check users’ credentials with database to ensure users’ identity:
		If valid, access token will be returned.
		If invalid, bad request error will be returned.
		@refer to TokenController@Post
	
	Step 4: generate access token by sending post request to 

		https://localhost:44305/api/token.
	
12. Secure API endpoint

	Step 1: Configure authorization middleware in the startup configureService method.
	
		 services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidAudience = Configuration["Jwt:Audience"],
				ValidIssuer = Configuration["Jwt:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
			};
		});
		
	Step 2: Inject the authorization middleware into the Request pipeline @public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	
		// JWT
        app.UseAuthentication();
		
	Step 3: Add authorization attribute to the controller.
	
		[Authorize]
		
	
## Project FrontEnd Blazor Setup

1. Add new project inside the same solution
			
	Step 1: Create Blazor app
	Step 2: Reference APIService 
			
			Click Dependencies and add reference (InventoryService)
					
2. Create FrontEnd Services that would communicate to our backend API (InventoryService) controllers

	Step 1: Create Services Folder
	Step 2: Create Interfaces Folder and create new interfaces (common/specific,base service)
	Step 3: Create Classes that will implement each corresponding interfaces
	Step 4: Create a BaseService that will be used as parent class for all specific services
	Step 5: Configure HttpClient inside BaseService class
	Step 6: Register all our services in our startup.cs class under ConfigureServices method
	
		services.AddSingleton<IBaseService, BaseService>();
		services.AddSingleton<IProductService, ProductService>();

3. Create FrontEnd ViewModels. Will be used for mapping our backend API (InventoryService) models
5. Create Razor Pages and call FrontEnd Services
	
		Refer to Pages/Products/Index.razor
	
## Deployment of multiple projects
1. In Solution Explorer, select the solution (the top node).
2. Choose the solution node's context (right-click) menu and then choose Properties. The Solution Property Pages dialog box appears.
3. Expand the Common Properties node, and choose Startup Project.
4. Choose the Multiple Startup Projects option and set the appropriate actions.
6. Configure launchSettings for each project 
	
	Step 1: Under FrontEnd Solutions, open Properties/launchSettings.json
and set the port number 
			
			"applicationUrl": "http://localhost:5000",
	
	Step 1: Under BackEnd (InventoryService) Solutions, open Properties/launchSettings.json and set the port number
	
			"applicationUrl": "http://localhost:5001"
		
	The BackEnd API will run on http://localhost:5001 and the FrontEnd webserver will run on another port(http://localhost:5000).


## Dependencies
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/3.1.0?_src=template
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/3.1.0?_src=template
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/3.1.0?_src=template
https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/3.1.0?_src=template

## References
https://www.syncfusion.com/blogs/post/how-to-build-crud-rest-apis-with-asp-net-core-3-1-and-entity-framework-core-create-jwt-tokens-and-secure-apis.aspx
https://wellsb.com/csharp/aspnet/blazor-httpclientfactory-and-web-api/
