# Remote Controller 
The project is a windows application, which gives an ability to run commands from remote application using http requests (e.g. IFTTT).

The application stays in a system tray area, where you can trigger custom commands as well.

The program has following settings:

1. Load program at Windows startup
2. Security code
3. Custom port number
4. Supported commands can be dynamically added using command plugins

Remote controller has following commands already implemented:
1. WakeOnLanCommand
2. LocalNetworkHttpRequestCommand
3. RunWindowsExecutableCommand
4. ShutdownCommand

[![Build status](https://ci.appveyor.com/api/projects/status/p8g0uov2y768r60f?svg=true)](https://ci.appveyor.com/project/karpach/remote-controller)