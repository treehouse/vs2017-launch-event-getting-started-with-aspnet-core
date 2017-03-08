
# Presentation

## TODO

* Determine the 1/2 way point and 10 minutes left mark
* Review MVA course
* Look for VS 2017 features to highlight (even in passing)

## Notes

__Goal: Introduce ASP.NET developers to ASP.NET Core development in Visual Studio 2017__

_"In this module, we'll create a simple app using both the .NET Core command-line interface (CLI) and the latest tooling available in Visual Studio 2017. Along the way, we'll also take a look at some of the key new concepts and features that are part of ASP.NET Core and how they compare to the ASP.NET that you know and love."_

* Avoid talking about the "Visual Studio launch event"
* Avoid talking about "the recent release of Visual Studio 2017"
* Don't worry about "Welcome back" type language – each module stands on its own

## Welcome

* Hi there! My name is James
* In this module, we'll take a look at getting started with ASP.NET Core in Visual Studio 2017

### Setting Expectations

* I'm going to assume in this module that you have previous experience with ASP.NET MVC and Visual Studio
* For instance, I'm going to assume that you're at least somewhat familiar with the MVC design pattern and you've worked with controllers and views before

## Introducing .NET Core and ASP.NET Core

### .NET Core

* Open source cross platform version of .NET
* Support on Windows, macOS, and Linux
* Subset of the full .NET Framework... not 1:1
 * Win Forms, WPF, WCF, and ASP.NET Web Forms are not part of .NET Core
* .NET Core and ASP.NET Core are delivered as a set of NuGet packages
 * Pay-for-what-you-use model
 * Can be included in your app or installed side-by-side user or machine wide
 * If deployed with your app, you only deploy what your application needs
* Only supports a single app model... console apps
 * Other app models can be built on top of it... like ASP.NET Core for instance

### ASP.NET Core

* Significant redesign of ASP.NET
* Complete rewrite of the ASP.NET web framework
* Biggest release of ASP.NET since version 1.0
* No longer based on System.Web.dll
 * Having a clean break from the past gave the ASP.NET team the ability to develop a fast, modern, cross platform web framework
* Runs on .NET Core or the full .NET Framework
 * This gives you the flexibility to use the framework that best supports your situation

### .NET Core Tooling

* In this session, we'll be primarily using the .NET Core tooling available in VS 2017
* The .NET Core tooling also includes the .NET Core command-line interface or CLI
 * Gives you the ability to develop .NET Core applications outside of Visual Studio
 * Important for supporting cross-platform development

#### Visual Studio 2017 Installer

* When running the installer, you'll be prompted to select the workloads that you want to include as part of your installation
* Selecting the "ASP.NET and web development" workload will install the .NET Core tooling

## Demos

## Wrap Up

### What We Didn’t Cover

* Deployment
* Docker
* Entity Framework Core
* Front End Development
* Identity
* View Components

### Resources and Demo Code

A list of additional resources and code for all of the demos shown in this module are available at https://github.com/treehouse/vs2017-launch-event-getting-started-with-aspnet-core 

### Related MVA Courses

Microsoft Virtual Academy has free ASP.NET Core training available. These courses, featuring Scott Hanselman and Maria Naggaga, are definitely worth your time.

* [Introduction to ASP.NET Core 1.0](https://mva.microsoft.com/en-US/training-courses/introduction-to-aspnet-core-10-16841)
* [Intermediate ASP.NET Core 1.0](https://mva.microsoft.com/en-US/training-courses/intermediate-aspnet-core-10-16964)
* [ASP.NET Core 1.0 Cross-Platform](https://mva.microsoft.com/en-US/training-courses/aspnet-core-10-crossplatform-17039)

### Treehouse 7-Day Free Trial

Get full access to our library for 7-days! You can check out my workshop on ASP.NET Core along with other great C# and .NET content.

Getting Started with ASP.NET Core (https://teamtreehouse.com/library/getting-started-with-aspnet-core)

Start learning today at https://teamtreehouse.com.
