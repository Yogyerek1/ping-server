# 📡 Ping - Private Messaging Server

<p align="center">
  <b>A self-hosted, decentralized backend for secure, modern communication.</b>
</p>

---

## 🔒 Overview

**Ping** is a decentralized, private messaging infrastructure built to counter mass surveillance and centralized data control. Unlike traditional messaging apps, **Ping does not rely on a central server.** 

This repository contains the **Ping Server**—a lightweight, self-hosted backend API that anyone (individuals, communities, or companies) can spin up independently. Users can connect to your server using the unified **Ping Client**, ensuring full ownership over their data, logs, and communication channels.

> **Privacy First:** What happens on your server, stays on your server. Zero tracking, zero third-party telemetry.

---

## ✨ Key Features

*   **Decentralized Architecture:** Run your own independent communication hub.
*   **Real-time Messaging:** Powered by SignalR for instant, low-latency message delivery.
*   **Secure & Private:** No middleman, no central database harvesting your metadata.
*   **Robust Backend:** Structured with ASP.NET Core Controllers for a clean, scalable, and enterprise-ready API architecture.

---

## 🛠️ Tech Stack

| Technology | Purpose |
| :--- | :--- |
| **.NET 10 / ASP.NET Core** | Modern web framework |
| **Web API Controllers** | Structured, scalable, and fully featured routing architecture |
| **SignalR** | Real-time bi-directional communication WebSocket layer |
| **Entity Framework Core** | Database abstraction and management |

---

## 🚀 Getting Started

### Prerequisites
*   [.NET SDK](https://dotnet.microsoft.com/download) (Version 10.0 or newer)
*   A code editor like **VS Code**

### Installation

Clone the repository to your local machine:
```bash
git clone https://github.com/Yogyerek1/ping-server.git
cd ping-server
```

Restore the dependencies:
```bash
dotnet restore
```

Run the server:
```bash
dotnet run
```

## 📄 License & Commercial Use

Ping is currently under exclusive copyright protection.

*   **Personal & Community Use:** Free to host, modify, and run for private, non-commercial purposes.
*   **Commercial Use:** If you are a business looking to integrate Ping into a commercial application or a proprietary environment, please contact the repository owner for licensing terms.
