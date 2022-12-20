<a name="readme-top"></a>

# Voclex

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#current-state-of-the-application">Current State Of The Application</a></li>
        <li>
          <a href="#technical-details">Technical Details</a>
          <ul>
            <li><a href="#list-of-the-technologies-used">List Of The Technologies Used</a></li>
          </ul>
        </li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#quickstart-with-docker">Quickstart With Docker</a></li>
        <li><a href="#debugging-with-visual-studio">Debugging With Visual Studio</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

*(Combined from the first syllables of Vocabulary and Lexicon.)* 

**Voclex is a tool for learning vocabulary with [spaced-based repetition](https://en.wikipedia.org/wiki/Spaced_repetition) flashcards.**

**Main goal**: a tool to **maximize efficiency** of the process of learning new words by automating time-consuming routines such as searching in dictionaries, looking for pictures and pasting them in your app of choice (or even writing down by hand on a piece of paper), etc. The focus of an application should also be not only automating all of the possible routines, but using the most efficient techniques for learning and memorizing new words, such as spaced repetition flashcards with:

•	audio pronunciation of the word (or a phrase) 

•	word spelling in the targeted language and in IPA

•	picture or a gif that can be described by the term

•	usage examples 

•	definition in the targeted language 

•	translation to the native language. 

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Current State Of The Application

The project is still in active development and currently is just a simplest possible form of a flashcard application. Features intended to achieve the aforementioned goal will be added soon, as well as many others (authentication, authorization, different storage options to mention a few). 

Also, the look of the application is very basic as I'm not a designer and my `CSS` and `Bootstrap` skills are very limited, but this will be improved in the future as well. :)

![Usage example](https://github.com/helgezes/Voclex/blob/main/Examples/Example%201.gif)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Technical Details

The main programming language used in the application is `C#`, but there are also some `JavaScript` libraries. 

The project consists of 4 major parts:

•	A `Web API` backend server written in `ASP.NET Core`. It stores information in an `SQL` database with the help of `Entity Framework Core` ORM.

• A	`Razor Class Library` with pages written in `Blazor`, using `HTML`, `CSS`, `Bootstrap` for markup. It retrieves the necessary data from the `Web API` server.

• A `Blazor WebAssembly` project that uses the `Razor Class Library` to  create a `WebAssembly` frontend application that a user can access with a browser.

• A `.Net MAUI` project that uses the `Razor Class Library` to create a `Mobile` (both `iOS` and `Android`) and a `Desktop` standalone application, that a user can install on their device.

_With this architecture we are able to create a cross-platform application that shares most of its codebase across all platforms, thus greatly reducing our efforts to create and maintain it._

<sub>
  This is not a comprehensive list of the actual .csproj projects, it's just those logical parts that I wanted to be highlighted here.
</sub>

#### List Of The Technologies Used
`C#`, `ASP.NET Core`, `SQL`, `Entity Framework Core`, `Blazor`, `.NET MAUI`, `HTML`, `CSS`

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started
All of the instructions below assume that you have cloned this repository onto your machine. Additionally, for all of the instructions except for **Debug With Multiple Startup Projects**, you will need to have a [Docker Desktop](https://www.docker.com/products/docker-desktop/) instance installed and running.

### Quickstart With Docker
1. Navigate to `Voclex` folder inside the root of the repository.
2. Execute `docker-compose up` inside this folder.

This will run the `Web API` on `http://localhost:61072/` and the `Blazor WebAssembly Client` on `http://localhost:5181/`.

Type `http://localhost:5181/` in your browser to access the application UI.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Debugging With Visual Studio

To debug the application with Visual Studio you have 3 options.

#### Debug With Multiple Startup Projects
1. Open `Voclex.sln` in [Visual Studio](https://visualstudio.microsoft.com/downloads/).
2. Select both the `BlazorWebAssembly` and the `WebApi` projects as a startup projects using the [multiple startup projects](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2022) feature.
3. Press `Start Debugging` (F5 on Windows by default).

#### Debug The Client Application

1. Navigate to `Voclex` folder inside the root of the repository.
2. Run `docker build -t voclexwebapi -f ".\WebApi\Dockerfile" .`.
3. Run `docker run -p "61072:80" -e ASPNETCORE_ENVIRONMENT=Development voclexwebapi`. This will run  the `Web API` on `http://localhost:61072/`.
4. Open `Voclex.sln` in [Visual Studio](https://visualstudio.microsoft.com/downloads/).
5. Select the `BlazorWebAssemblyClient` project as a startup project.
6. Press `Start Debugging`.
7. Go to `http://localhost:61072/`

#### Debug The Web API
1. Open `Voclex.sln` in [Visual Studio](https://visualstudio.microsoft.com/downloads/).
2. Select the `WebApi` project as a startup project.
3. Press `Start Debugging` (F5 on Windows by default).
4. Go to `http://localhost:61072/swagger`.

Alternatively, you can do some requests from the client application and debug their effects on the Web API. 

5. Run `docker build -t voclexblazorclient -f ".\BlazorWebAssemblyClient\Dockerfile" .`
6. Run `docker run -p "5181:80" voclexblazorclient`.
7. Go to `http://localhost:5181/`.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the  GPL-3.0 license. See `LICENSE` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

<p align="right">(<a href="#readme-top">back to top</a>)</p>
