
# Demos

### Creating an ASP.NET Core Application

* Use the "File > New Project" dialog to create an empty ASP.NET Core project
* Click on the "Web" category to view the available ASP.NET project templates
* Targeting .NET Core vs the full .NET Framework
 * Typically you'll want to target .NET Core
 * This gives you the ability to develop or deploy your application onto non-Windows platforms
 * Also gives you the ability to use the Docker tools in Visual Studio
 * If you need a feature that's not part of .NET then target the full .NET framework
* In the second dialog, you can select the specific ASP.NET Core template that you want to use
 * Notice that you can select the ASP.NET Core version that you want to target in the drop down list at the top of the dialog
 * You can also enable Docker support or select authentication options

### Dependencies

* In Solution Explorer, references are now listed under "Dependencies" and grouped by their type (i.e. NuGet packages, SDKs, projects, etc.)

### New Project System (csproj)

* The previous project.json based project system has been replaced with a new MSBuild-based (csproj) project system
* This is the NOT the csproj that you know and... probably generally dislike

#### MSBuild Improvements

* Many improvements have been made
* You can "live" edit the project file
 * Right click on the project and select "Edit [project name].csproj"
* The number of elements has been dramatically reduced
* Every file in the project folder is now automatically included thanks to a default set of common globs
 * VS will automatically pick up changes from disk (i.e. when you switch branches or add/remove/rename files and folders)
 * This one change will reduce many of the merge conflicts that .NET developers have dealt with for years
* Package references are now included instead of being in a separate file
 * Install the Project File Tools extension to get IntelliSense for NuGet packages

_Demo: Remove a package... and add it back again_

#### project.json Migration

* If you have ASP.NET Core projects that use a project.json file, you can use VS or the .NET Core CLI to migrate to the new project format
* csproj is not coming to VS 2015 and VS 2017 will not work with project.json
 * Migrating to csproj commits you to using VS 2017

### Reviewing the Project Files

* Let's review the files in our project
* Our project only contains two files: Program.cs and Startup.cs

#### Program.cs

* The Program class only contains a single method
* You're probably looking at the `static void Main` method and thinking "this looks like a console app!"... and you'd be right!
* As I mentioned a bit ago, .NET Core only supports a single app model... console apps
* An ASP.NET Core application is a console app that builds and runs a web host

#### Web Host Builder

```
var host = new WebHostBuilder()
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
    .UseStartup<Startup>()
    .UseApplicationInsights()
    .Build();

host.Run();
```

* WebHostBuilder is used to create and configue the host for our web application
* The `UseKestrel` method tells the host to use the Kestrel web server
 * Kestrel is a cross-platform managed web server
 * The WebListener server is also available when you know your web application will only be running on Windows servers and you need support for Windows-only features like Windows Authentication
* The `UseContentRoot` method is used to specify the content root for your application
 * The content root is the root of your project, containing all of your code and view files
 * It's important to note that the root of our application is not the same as the web root, which is the "wwwroot" folder in our project
 * The web root is the location of the servable static web content
 * Being able to specify separate application and web roots is a nice improvement
* The `UseIISIntegration` method configures the host so our web application can be integrated with IIS or IIS Express
 * IIS is no longer directly supported meaning that IIS does not host ASP.NET Core apps within its own process
 * IIS is used as a reverse proxy (using the AspNetCoreModule HTTP module) to Kestrel
 * This is the same general approach for hosting Node.js apps in IIS
* The `UseStartup` method tells the host to use the specified Startup class
 * We'll take a look at the Startup class next
* The `Build` method builds the host object and the `Run` method starts it listening for incoming HTTP requests

#### Startup.cs

* The `Startup` class is just a plain class—no base class inheritance or interface implementations
* It includes two specially named methods `ConfigureServices` and `Configure` that the host will look for and call when our app is starting up

##### `ConfigureServices` Method

```
public void ConfigureServices(IServiceCollection services)
{
}
```

* A service is a component that is intended for consumption in your application
* Services are made available through dependency injection or DI
 * ASP.NET Core includes a simple built-in inversion of control (IoC) container that supports constructor injection by default
 * The built-in container can be easily replaced with your container of choice
* Our `ConfigureServices` method isn't configuring any services
 * We'll see an example of service configuration in a bit when we create an MVC application

##### Application Services

* Even though we aren't configuring any services in this method, there are still services that we can request
* ASP.NET Core provides application services that we can use during the application's startup
* These services can be injected into the Startup class's constructor or its `Configure` or `ConfigureServices` methods

##### `Configure` Method

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole();

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
}
```

* The `Configure` method is used to configure the HTTP request pipeline
* Three application services are being injected into the `Configure` method
 * IApplicationBuilder - Used to configure the middleware pipeline
 * IHostingEnvironment - Provides information about the current environment
 * ILoggerFactory - Used to configure logging providers

##### Middleware Pipeline

* The ASP.NET Core request pipeline is composed using middleware
 * If you're familiar with how middleware works in Node or OWIN then you'll feel right at home with ASP.NET Core middleware

_Let's comment out everything but the call to `app.run`... now we have exactly one middleware component_

### Running Our App

* Running without debugging provides you with an "edit-refresh" workflow

_Demo: Run the app without debugging and show that you can make a change to a code file and refresh the browser to see the change_

* Remember that IIS is being used as a reverse proxy, so it's receiving all of the requests and forwarding them on to Kestrel which in turn handles all of the requests for our application
* We can run our application using Kestrel by selecting our application in the "Play" button drop down
 * When we do that, we'll see our application start up in a console window before our selected browser is opened
 * We can see in the console that our application has started up and is now listening for requests

### Build Up Approach

* ASP.NET Core uses a "build up" approach instead of a "tear down" approach
* Nothing is enabled by default
* Disable/remove all middleware
* Set a breakpoint to show that the `Configure` method is called when we run our app
* But we get a 404 "page not found" status code

#### Error Handling

* Disable the developer exception page and show the error status code that we receive... but no content
* Bring back the developer exception page

#### Static Files

* What if we just wanted to serve up an HTML page?
* wwwroot is the location for all static content for your application
 * Only static content in that folder can be served
* Add index.html page to the wwwroot folder
* Try to browse to it
* Add support for static files
* Add support for default files

### .NET Core CLI

* Working with project and solution files doesn't require Visual Studio
 * The CLI allows developers across platforms to work with VS solutions
* The .NET CLI is shipped as part of the .NET Core SDK
 * Set of commands that allows you to create, build, run, publish, test, and package .NET Core apps from the command line

#### Working with Projects and Solutions Using the .NET Core CLI

* Use `dotnet new` to create a class library project
 * New templating engine
 * Show a list of the available templates
* Add the project to the solution while we watch for the project to show up in the Solution Explorer

```
dotnet new --help
dotnet new classlib -n SharedLibrary -o SharedLibrary
dotnet sln add SharedLibrary/SharedLibrary.csproj
dotnet sln list
dotnet sln remove SharedLibrary/SharedLibrary.csproj
```

* Switch back to VS and reload the solution
* Add a reference to the ASP.NET Core project
* Show the updated csproj file

```
<ItemGroup>
  <ProjectReference Include="..\SharedLibrary\SharedLibrary.csproj" />
</ItemGroup>
```

## MVC

* Use Visual Studio 2017 to create an ASP.NET Core MVC application
* Immediately run the app and show it running in the browser

### Another Look at Startup.cs

#### Configuration

```
var builder = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
Configuration = builder.Build();
```

* The constructor is being used to set up our app's configuration
* App configuration is no longer dependent upon the web.config file
* App configuration data stored as key/value pairs can be read from a variety of file formats
* The configuration data sources are loaded in the order that they are defined
* If a key is the same as a key that has already been loaded, then the new value will replace the old value
* Environment variables gives us a convenient way to provide sensitive configuration data—like API keys or database connection strings—at runtime without having to write those values to a file

#### Logging

```
loggerFactory.AddConsole(Configuration.GetSection("Logging"));
loggerFactory.AddDebug();
```

Now we're configuring a debug logger in addition to the console logger.

#### Services

We're calling the `AddMvc` extension method to add the services that MVC requires.

#### MVC Middleware

The `UseMvc` method is being used to add the MVC middleware to the pipeline.

```
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});
```

#### Route Configuration

The `UseMvc` method allows you to pass in a callback for configuring routes. You can also use the `UseMvcWithDefaultRoute` method if you don't need to add any custom routes.

```
app.UseMvcWithDefaultRoute();
```

The route template syntax now allows you to provide default values inline with the parameter definitions.

```
{controller=Home}/{action=Index}/{id?}
```

### Controllers and Views

* Controllers and views for the most part look and feel like they did before
 * Your day-to-day work will not feel much different than it does today
* Controllers still inherit from a Controller base class (but they no longer absolutely need to)
* Action methods return `IActionResult`
* Adding an action method and view just works as you expect it to
 * Right-click on the `View` method and select "Add View" to scaffold the view

```
public class TestController
{
    public string Index()
    {
        return "Hello from the test controller!";
    }
}
```

* Update the _Layout view to include an additional menu item
 * What is this?
 * Where's the Html.ActionLink method call?

```
<li><a asp-area="" asp-controller="Home" asp-action="Customers">Customers</a></li>
```

## Tag Helpers

Tag Helpers enable server-side code to participate in creating and rendering HTML elements in Razor files.

* An HTML-friendly development experience
* A rich IntelliSense environment for creating HTML and Razor markup
* A way to make you more productive and able to produce more robust, reliable, and maintainable code using information only available on the server

[Open the layout page and review tag helpers in it]

Let's also take a look at how Tag Helpers are being used elsewhere in the layout page.

...

Now let's look at an example of a form.

[Open tag helpers project]

This is a pretty basic MVC Razor form. Nothing surprising here.

```
@model TagHelpers.Models.Contact

@{
    ViewData["Title"] = "Add Contact";
}

<h2>@ViewData["Title"].</h2>

@using (Html.BeginForm())
{
    <div class="form-group">
        @Html.LabelFor(m => m.Name, new { @class ="control-label" })
        @Html.TextBoxFor(m => m.Name, new { @class = "form-control" })
    </div>

    <div class="form-group">
        @Html.LabelFor(m => m.Email, new { @class = "control-label" })
        @Html.TextBoxFor(m => m.Email, new { @class = "form-control" })
    </div>

    <div class="form-group">
        @Html.LabelFor(m => m.Phone, new { @class = "control-label" })
        @Html.TextBoxFor(m => m.Phone, new { @class = "form-control" })
    </div>

    <div>
        <button class="btn btn-lg btn-success">Save</button>
    </div>
}
```

Now let's look at the Tag Helpers version. Notice any differences?

```
@model TagHelpers.Models.Contact

@{
    ViewData["Title"] = "Add Contact";
}

<h2>@ViewData["Title"].</h2>

<form method="post">
    <div class="form-group">
        <label asp-for="Name" class="control-label"></label>
        <input asp-for="Name" class="form-control" />
    </div>

    <div class="form-group">
        <label asp-for="Email" class="control-label"></label>
        <input asp-for="Email" class="form-control" />
    </div>

    <div class="form-group">
        <label asp-for="Phone" class="control-label"></label>
        <input asp-for="Phone" class="form-control" />
    </div>

    <div>
        <button class="btn btn-lg btn-success">Save</button>
    </div>
</form>
```

## Custom Tag Helpers

You can also create custom tag helpers

* Add a custom tag helper
* Add the tag helper to our form

```
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpers.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        private const string EmailDomain = "smashdev.com";

        // Can be passed via <email mail-to="..." />. 
        // Pascal case gets translated into lower-kebab-case.
        public string MailTo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag

            var address = MailTo + "@" + EmailDomain;
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
        }
    }
}
```

```
@addTagHelper *, TagHelpers
```

## API Controllers

Let's take a look at API controllers in ASP.NET Core.

[Open API controllers demo project]

* Web API has now been merged into MVC
* MVC and API controllers now share the same base class
* We can now use the `[controller]` and `[action]` tokens in our routes
* We need to be explicit with the verbs that our methods should respond to
* You can now provide the route template in the HTTP verb attributes

### JSON.net Changes

Now defaults to camel-case for your property names.

[Use POSTMAN to show JSON response]

Show how to fix this.

Snippet: _jo

```
services.AddMvc()
        .AddJsonOptions(opt =>
    {
        var resolver  = opt.SerializerSettings.ContractResolver;
        if (resolver != null)
        {
            var res = resolver as DefaultContractResolver;
            res.NamingStrategy = null;  // <<!-- this removes the camelcasing
        }
    });
```
