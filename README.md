# DailyForWindows

This is a minimal WPF implementation that mimics the core features of the macOS **Daily** application.

## Features

- Runs in the system tray and automatically starts on login.
- Periodically prompts the user with a dialog asking "What are you doing?".
- Entries are appended to a CSV log file under `%APPDATA%\DailyForWindows\log.csv`.
- History and settings windows allow managing past entries and configuration.
- Settings are stored in `%APPDATA%\DailyForWindows\settings.json`.

## Building

```
dotnet build DailyForWindows.sln
```

## Testing

```
dotnet test DailyForWindows.sln
```

The tests cover the timer and log service components.
