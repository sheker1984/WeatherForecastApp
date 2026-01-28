# WeatherTrackerServices – Coding Exercise

This solution implements the full-stack weather-tracking application described in the assignment.  
It includes:

- A **.NET 10 Web API backend** that:
  - Reads and parses dates from `dates.txt`
  - Validates multiple date formats
  - Calls the **Open-Meteo Historical Weather API**
  - Stores results locally under `WeatherData/`
  - Exposes a single endpoint: `/api/weather`

- A **Razor Pages frontend** that:
  - Calls the backend API
  - Displays weather results in a sortable table
  - Shows loading and error states
  - Allows clicking a row to view details

---

##  How to Run the Backend (API)

### **Prerequisites**
- .NET 10 SDK installed

### **Steps**
```bash
cd src/WeatherApp.Api
dotnet restore
dotnet run
