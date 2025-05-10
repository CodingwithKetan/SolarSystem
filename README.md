# Solar System App

This application fetches and displays information about planets and their moons, including average moon gravity and temperatures.

##Current Implementation

### Approach
- The current logic **fetches all planets** from a remote server.
- Planets **with moons** are then filtered **in memory** on the client side.
- This is a **naive and non-optimized approach** and should be replaced with a server-side filter to fetch only relevant data.

### Review-Friendly Changes
- Existing functionality has been **temporarily commented out** to simplify the review process and focus on recent changes.

## Known Issues

- The **average temperature** of all moons is being returned as `0` from the backend.
  - This likely requires a **server-side fix or validation of source data**.

## Planned Improvements

- Implement **server-side filtering** to fetch only planets that have moons.
- Fix backend response to return accurate **moon average temperatures**.
- Re-enable and optimize previously commented code once review is complete.

## Sample output 
![image](https://github.com/user-attachments/assets/8561b96a-6631-45a1-8058-4133e07d23fe)
