# Drips Quality Assurance Case

A simple C# test framework. 

## Description

This github repo contains a C# test framework that I created using C# and NUnit. This framework is testing the Magento shopping cart at (https://magento.softwaretestingboard.com/) using Selenium.  Additionally, the repository also includes API tests created using RestSharp for testing (https://reqres.in/).

API Tests can be found at API / Tests.  Selenium Tests are located at Selenium / Tests.

## Getting Started

### Dependencies

* This framework was created using Visual Studio 2022, version 17 with C#10 and .NET6
* Additionally, the test framework is using Chrome as the default browser/driver 
* In order to run tests, you must create an Environment variable on your computer: **AutomationEnvironment** with the value of **QA**

### Installing

Once you download the repo and open the solution file in Visual Studio, please make sure that all nuget packages are installed. This can be done as follows:
1. From Visual Studio, go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution ...
2. Then make sure all the packages are installed for the project.

If you are not using Visual Studio, you can run this repository in Visual Studio Code
1. Make sure that you have .NET Core installed [dotnet-sdk](https://docs.microsoft.com/en-us/dotnet/core/install/)
2. Add the [C# tools extension to visual studio code](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

### Executing program

1. Find the respective Tests folder (either API or Selenium) on any individual test
2. Right-click on the [Test] annotation
3. Select Run Tests
* Alternatively, you can select Test then Run All Tests from the top menu in Visual Studio. 


## Author

Rachel Fuller  

