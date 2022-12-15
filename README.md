<a name="readme-top"></a>

# Voclex

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
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



### Tecnhical Details

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

#### List Of Technologies Used
`C#`, `ASP.NET Core`, `SQL`, `Entity Framework Core`, `Blazor`, `.NET MAUI`, `HTML`, `CSS`

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites


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
