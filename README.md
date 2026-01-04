# Hundred Days Of Training API

This repository contains the **backend API** for the *Hundred Days Of Training* workout application.

I built this project both as a **portfolio project** and for **personal use**.  
I enjoy strength training and wanted a system that clearly shows my long-term progression. especially for **weighted pull-ups and push-ups**.

The API powers a couple structured 100-day workout program where daily sets are completed, tracked, and evaluated over time, making progression measurable. 

---

## Role within the system

This API is one component of a **private end-to-end system**:

- A frontend (PWA / web application) is used on my phone and laptop
- The frontend communicates with this backend API
- Both run on a **Raspberry Pi Linux server**
- Access is restricted to a **private Tailscale network**

The API is **not publicly exposed** and is intended for **personal use only**.

---

## System architecture

The diagram below shows how the API fits into the overall system.

- Client devices connect through **Tailscale**
- Requests are routed via **Nginx**
- The ASP.NET Core API handles business logic
- The database is accessible only by the API

All communication happens inside a private network.

<img width="1536" height="1024" alt="System architecture diagram" src="https://github.com/user-attachments/assets/b0e07a44-603a-47ac-b9c2-f5cb6d4f3a42" />

---

## Architecture

The API is built with **ASP.NET Core** and follows **Clean Architecture** principles:

- Clear separation between domain, application, and infrastructure layers
- CQRS-style queries and commands
- Focus on maintainability, testability, and long-term scalability

The API runs locally on the server and is accessed through a reverse proxy.

---

## Security & Access

- The API is **not accessible via the public internet**
- Access is limited to trusted devices via **Tailscale**
- The database is accessible **only** by this API

Security is handled at the **network level**, which is sufficient for personal use.

---

## Tech stack

- **ASP.NET Core / .NET**
- **C#**
- **Clean Architecture**
- **CQRS / MediatR**
- **MariaDB**
- **Linux (Raspberry Pi)**
- **Tailscale (private networking)**

---

## Notes

This repository contains **only the backend API**.  
The frontend and infrastructure configuration are maintained in a separate repository.
