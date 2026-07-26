# 🚜 Autonomous UGV Control Platform
> **Edge Computing-Based Control, Communication, and Video Streaming Center for Unmanned Ground Vehicles**

![C#](https://img.shields.io/badge/C%23-.NET%20Framework%2FCore-blue)
![Python](https://img.shields.io/badge/Python-3.10%2B-yellow)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC%20%26%20SignalR-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-5434-blue)
![YOLOv8](https://img.shields.io/badge/YOLOv8-Nano%20Edge%20AI-brightgreen)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📋 About the Project

This project is a **cross-platform (Web + Desktop)** control and communication center designed to provide remote monitoring, autonomous navigation, edge AI-based environmental perception, and real-time hardware health tracking for Unmanned Ground Vehicles (UGVs).

The system ensures synchronized, asynchronous communication between the in-vehicle Raspberry Pi 5 companion computer, the Pixhawk flight controller, the ASP.NET Core-based web interface, and the C# WinForms desktop application.

---

## ✨ Key Features

- **🧠 Edge AI & Computer Vision:** Real-time object, obstacle, and human detection using the YOLOv8 Nano model processed from monochrome camera feeds.
- **🎯 P-Controller Based Autonomy:** A Proportional (P) controller and Finite State Machine (FSM) architecture that generates dynamic PWM signals (1100–1900) based on camera frame errors.
- **⚡ Low-Latency Communication:** Flask HTTP REST API endpoints and SignalR WebSocket tunnels that provide millisecond-level data streaming between the server and clients without page refreshes.
- **🛡️ Safety & Fault Tolerance:** Software Watchdog and Emergency Stop (E-Stop) protocols that immediately set motor outputs to neutral in the event of signal loss or video stream freezing.
- **📊 Consistent Data Logging:** Telemetry and system health logging into a PostgreSQL relational database using absolute **UTC** timestamps to prevent data conflicts across distributed clients.

---

## 🏗️ System Architecture

```text
[ Pixhawk Flight Controller ]
          │ (MAVLink / Serial)
          ▼
[ Raspberry Pi 5 (Companion Computer) ]
   ├── YOLOv8 Nano & Vision Module
   ├── P-Controller & State Machine
   └── Flask REST API / MJPEG Streamer
          │ (HTTP POST / REST / Async)
          ▼
[ ASP.NET Core Backend & SignalR Hub ] ────► [ PostgreSQL DB (UTC) ]
          │ (WebSocket / Real-Time)
          ├──► [ Web Control Station (MVC) ]
          └──► [ C# WinForms Desktop Station ]


<img width="1919" height="919" alt="Dashboard" src="https://github.com/user-attachments/assets/b284cf43-3ad9-446c-8ee0-c4a9022d13c0" />
<img width="1918" height="904" alt="Control Panel" src="https://github.com/user-attachments/assets/69d07163-6812-4f86-8578-b980a19f5c58" />
<img width="1909" height="920" alt="System Logs" src="https://github.com/user-attachments/assets/9384ece0-2c6f-4f68-a45d-29afb22778e7" />
<img width="1917" height="911" alt="Telemetry" src="https://github.com/user-attachments/assets/f645996b-76db-4204-9126-be29ae39d333" />
<img width="1916" height="915" alt="AI Vision" src="https://github.com/user-attachments/assets/7c708f9a-43dd-4dff-acbf-752440fafb2b" />
