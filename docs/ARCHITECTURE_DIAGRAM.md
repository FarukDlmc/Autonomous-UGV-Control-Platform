\# System Architecture Diagram (ASCII)



```text

+-------------------------------------------------------------------------+

|                       RPI 5 COMPANION COMPUTER                          |

|                                                                         |

|  \[ Monochrome Cam ] ---> \[ YOLOv8 Nano ] ---> \[ P-Controller (Servo) ]  |

|                                                     |                   |

|  \[ Pixhawk / GPS ]  ---> \[ Telemetry Pub ] ---------+                   |

|                                 |                                       |

|                                 v                                       |

|                          \[ Flask REST API ]                             |

+---------------------------------+---------------------------------------+

&#x20;                                 |

&#x20;                  HTTP POST / JSON / MJPEG Stream

&#x20;                                 |

&#x20;                                 v

+-------------------------------------------------------------------------+

|                      ASP.NET CORE BACKEND SERVICE                       |

|                                                                         |

|         \[ PostgreSQL (Port 5432 / UTC) ] <---> \[ SignalR Hub ]          |

+-------------------------------------------------------------------------+

&#x20;                                                    |

&#x20;                                        WebSocket / TCP Pipeline

&#x20;                                                    |

&#x20;                 +----------------------------------+----------------------------------+

&#x20;                 |                                                                     |

&#x20;                 v                                                                     v

+-----------------------------------+                                 +-----------------------------------+

|     ASP.NET CORE WEB STATION      |                                 |    C# WINFORMS DESKTOP STATION    |

| (Dashboard, Live Map, Analytics)  |                                 | (AIVisionUC, TelemetryUC, E-Stop) |

+-----------------------------------+                                 +-----------------------------------+

