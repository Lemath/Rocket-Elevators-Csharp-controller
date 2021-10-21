# Rocket-Elevators-Csharp-Controller
This is the template to use for the C# commercial controller. In the Commercial_Controller folder, you will find the classes that should be used along with some methods described in the requirements. The necessary files to run some tests are also included, in the Commercial_Controller.Tests folder.

### Installation

As long as you have **.NET 5.0** installed on your computer, nothing more needs to be installed:

The code to run the scenarios is included in the Commercial_Controller folder, and can be executed there with:

`dotnet run <SCENARIO-NUMBER>`

### Running the tests

To launch the tests, make sure to be at the root of the repository and run:

`dotnet test`

With a fully completed project, you should get an output like:

![csharp_test1](https://user-images.githubusercontent.com/56204810/138208324-23bc89b9-2d42-4d5e-a9a7-c379e06eca6b.png)

You can also get more details about each test by adding the `-v n` flag: 

`dotnet test -v n` 

which should give something like: 

![csharp_test2](https://user-images.githubusercontent.com/56204810/138208364-f8fbaf27-fbc8-4355-846f-bb79eed7bcc7.jpg)

