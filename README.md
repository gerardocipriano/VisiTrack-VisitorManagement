<a name="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/gerardocipriano/visitrack-visitormanagement">
    <img src="img/visitrack_logo.png" alt="Logo" width="200">
  </a>

<h3 align="center">VisiTrack</h3>

  <p align="center">
    VisiTrack is a visitor management system designed to streamline the process of registering visitors in and out of a facility. It is built with ASP.NET Core, Entity Framework, MVC, Vue, and SignalR.
    <br /><br />
    <a href="https://www.codefactor.io/repository/github/gerardocipriano/visitrack-visitormanagement"><img src="https://www.codefactor.io/repository/github/gerardocipriano/visitrack-visitormanagement/badge" alt="CodeFactor" /></a>
    <br /><br />
    <a href="https://github.com/gerardocipriano/visitrack-visitormanagement/issues">Report Bug</a>
    ·
    <a href="https://github.com/gerardocipriano/visitrack-visitormanagement/issues">Request Feature</a>
  </p>
</div>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

<br />
<div align="center">
  <a href="https://github.com/gerardocipriano/visitrack-visitormanagement">
    <img src="img/visitrack_logo.png" alt="Logo" width="200">
  </a>
  <p align="center">
</div>
VisiTrack is a visitor management system designed to streamline the process of registering visitors in and out of a facility. Before the start of a visit, visitors are registered into the system, creating a record of their presence. This record is used to track the visitor’s entry and exit from the facility, ensuring a clear record of all visitors at any given time.

In operation, the system allows for easy check-in and check-out of visitors, providing real-time verification of visitors currently in the facility. The system is designed with privacy regulations in mind, ensuring that all visitor data is handled in a secure and compliant manner.

The project ends when all the mandatory features are implemented and tested, and the system is ready for deployment. Optional features may be added based on the specific needs of the facility and feedback from users.

Built with ASP.NET Core, Entity Framework, MVC, Vue, and SignalR, VisiTrack is designed to be robust, efficient, and user-friendly, providing a reliable solution for visitor management. It uses a in-memory DB wich will be changed to mysql in future releases.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/gerardocipriano/VisiTrack-visitormanagement.git
   ```
2. Install web depencencies
   ```sh
   cd src\Web
   npm i
   ```
3. Open the Visitrack.sln with Visual Studio 2022, switch to the Web project and run the web server

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->

## Usage

The main screen of the software that presents different options for the user to choose from.
You can login with the following test credential: email1@test.it pw: Prova

### Register new visitor

Fill check in form and then click on submit button

<div align="center">
  <a href="https://github.com/gerardocipriano/visitrack-visitormanagement">
    <img src="img/checkin.png" alt=" screen" height="500">
  </a>
</div>

### Visitors List

From this page you can view and search today registed visitors that are still in the facility.
Tap on check-out when the visitor leaves.

<div align="center">
  <a href="https://github.com/gerardocipriano/visitrack-visitormanagement">
    <img src="img/visitorslist.png" alt=" screen" height="500">
  </a>
</div>

### Reports

From this page you can inspect per week number of visitors.

<div align="center">
  <a href="https://github.com/gerardocipriano/visitrack-visitormanagement">
    <img src="img/reports.png" alt=" screen" height="500">
  </a>
</div>

### Admin Area

From this page you can view, edit and add users that are able to login into Visitrack

<div align="center">
  <a href="https://github.com/gerardocipriano/visitrack-visitormanagement">
    <img src="img/admin.png" alt=" screen" height="500">
  </a>
</div>

<!-- ROADMAP -->

## Roadmap

Minimum mandatory features:

- [x] Visitor check-in
- [x] Visitor check-out
- [x] Verification of visitors currently in the facility

Optional features:

- [x] Search functionality to quickly find visitors
- [x] Generation of detailed reports on visitor presence
- [ ] Manage login user password through admin area

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->

## License

Distributed under the MIT License. See `LICENSE` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->

## Contact

- Massimiliano Battelli <massimilian.battelli@studio.unibo.it><br />

- Gerardo Cipriano <gerardo.cipriano@studio.unibo.it><br />

Project Link: [https://github.com/gerardocipriano/visitrack-visitormanagement](https://github.com/gerardocipriano/visitrack-visitormanagement)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->

[license-url]: https://github.com/gerardocipriano/visitrack-visitormanagementblob/main/LICENSE
[product-screenshot]: img/visitrack_logo.png
