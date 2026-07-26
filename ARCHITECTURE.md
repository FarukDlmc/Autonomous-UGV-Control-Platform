# Architecture Overview

This document outlines the software design, communication flow, and state control mechanics of the Autonomous UGV Control Platform.

## 1. Edge Bounded Vision & Control Mechanics

The vehicle leverages an edge computing paradigm to eliminate offloading bottlenecks:
1. **Frame Capture:** Monochromatic frames are retrieved to bypass RGB processing overhead.
2. **Detection & Offset:** YOLOv8 Nano detects objects/obstacles. The bounding box horizontal center ($X$) is compared against the camera frame's optical center ($640\text{ px}$).
3. **P-Control Steering Loop:** 
   $$\text{Error} = \text{Center}_X - \text{Target}_X$$
   $$\text{PWM}_{\text{output}} = \text{SaturationFilter}(\text{PWM}_{\text{neutral}} + K_p \times \text{Error}, 1100, 1900)$$
4. **State Machine Transitions:**
   - `IDLE` $\rightarrow$ `NAVIGATION` $\rightarrow$ `AVOIDANCE` $\rightarrow$ `RECOVERY`

## 2. Real-Time Telemetry Pipeline

- Telemetry packets (Speed, Battery Voltage, GPS Coordinates, CPU Temp, Uptime) are formatted into JSON.
- The Companion Computer posts telemetry data to the ASP.NET Core Backend via REST API.
- The Backend broadcasts incoming packets to connected control stations using **SignalR WebSockets** without page refreshes.

## 3. Data Integrity & Clock Synchronization

Distributed deployments present race conditions and timestamp drift. To guarantee exact chronological sorting and auditing:
- All system events, telemetry frames, and AI detection logs are written to **PostgreSQL (Port 5432)** using **UTC timestamps**.
