\# Autonomous UGV System Specifications \& Test Results



\## 1. Hardware Stack

\- \*\*Companion Computer:\*\* Raspberry Pi 5 (8GB)

\- \*\*Flight Controller / Lower Level Driver:\*\* Pixhawk (MAVLink protocol over Serial)

\- \*\*Camera Sensor:\*\* Monochrome Global Shutter Camera (Optimized for contrast \& edge detection)

\- \*\*Positioning:\*\* GPS / GNSS Module



\## 2. Software \& Framework Stack

\- \*\*Edge AI Model:\*\* YOLOv8 Nano (Quantized for edge deployment)

\- \*\*Control Strategy:\*\* Proportional (P) Steering Controller \& Finite State Machine (FSM)

\- \*\*Web Platform:\*\* ASP.NET Core MVC \& Real-Time SignalR Websockets

\- \*\*Desktop Application:\*\* C# .NET Windows Forms (Modular UserControl Architecture)

\- \*\*Database:\*\* PostgreSQL (Port 5432, UTC timestamping, Connection Pooling enabled)



\## 3. Test Bench \& Field Results

\- \*\*REST API Latency:\*\* HTTP POST JSON command latency between control center and Flask API stays under miliseconds.

\- \*\*SignalR Throughput:\*\* High-frequency telemetry packets streamed without bandwidth bottleneck.

\- \*\*Fail-Safe \& Watchdog:\*\* P-Controller resets steering angle to neutral within milliseconds of object loss; E-Stop zeroes motor PWMs immediately upon trigger.

