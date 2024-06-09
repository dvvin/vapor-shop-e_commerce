<a name="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://vapor-shop.xyz">
    <img src="client\src\assets\images\logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Vapor Shop</h3>

  <p align="center">
    ·
    <a href="https://vapor-shop.xyz">View Demo</a>
    ·
  </p>
</div>

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
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

![Vapor Shop Screenshot](client\src\assets\images\shopPage.png)

Vapor Shop is a simple e-commerce platform designed to provide a seamless online shopping experience for various products. With user friendly features and a secure payment system through Stripe, Vapor Shop ensures customer satisfaction and security.

Here's why Vapor Shop stands out:

- **User-Friendly**: Intuitive and responsive design.
- **Secure Payments**: Integrated with Stripe for secure transactions.
- **Robust Backend**: Powered by .NET 8 with PostgreSQL and Redis for high performance and scalability.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Screenshots
#### Shop Page
![Vapor Shop Screenshot](client\src\assets\images\shopPage.png)

#### Product Page
![Vapor Shop Screenshot](client\src\assets\images\shopProduct.png)

#### Basket Page
![Vapor Shop Screenshot](client\src\assets\images\shopBasket.png)

#### Checkout Page
![Vapor Shop Screenshot](client\src\assets\images\shopCheckout.png)

#### Orders Page
![Vapor Shop Screenshot](client\src\assets\images\shopOrders.png)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

- Angular 17
- Bootstrap 5
- Stripe
- .NET Core 8
- PostgreSQL
- Redis
- Docker
- DigitalOcean
- Linux/Apache2

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

Ensure you have the following installed on your machine:

- Node.js
- .NET SDK
- PostgreSQL
- Redis
- Docker
- Git

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/dvvin/vapor-shop-e_commerce.git
   cd vapor-shop-e_commerce
   ```
2. Install frontend dependencies:
   ```sh
   cd client
   npm install
   ng build
   ng serve
   ```
3. Install backend dependencies:
   ```sh
   cd ..
   dotnet restore
   cd API
   setup Stripe credentials in appsettings.json
   ```
4. Start backend:
   ```sh
   cd ..
   docker-compose up -d
   dotnet run or dotnet watch run
   ```
5. Navigate to https://localhost:4200/ to view the demo.

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Email: davin.davila01@gmail.com

Project Link: [Vapor Shop](https://github.com/dvvin/vapor-shop-e_commerce.git)

<p align="right">(<a href="#readme-top">back to top</a>)</p>
