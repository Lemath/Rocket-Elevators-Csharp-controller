# Rocket-Elevators-Csharp-Controller

### Description

This project simulate the selection and movement logic for elevators in a commercial building and is written in C#. 

When someone want to call an elevator from the lobby, he will first select his destination. The controller will then decide 
which column of the battery to use and select the best elevator available to fill the request and carry the user to his selected floor.
When someone call an elevator from a floor, the corresponding column will select his best available elevator to pick up the user and bring him back to the lobby.

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

