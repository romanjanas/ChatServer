# ChatServer


## Overview
Chat server using NuGet package Owin.WebSocket.

## Addressed issues
Project covers following specifications:
* Provide simple HTML page with login and chat window,
* support multiple users,
* communication has to use WebSockets,
* page has to have simple design,
* login only by nickname,
* all components runnable through default solution (.sln),
* documented in README.txt,
* published on GitHub.

## Usage
Solution requires Microsoft Visual Studio (at least version 2013).

Solution is runnable **after project is open** by hitting **F5** key.

After that the default browser is started and displays initial page.

Click on provided link.

### Use executable host OwinHost.exe
OwinHost package is needed:

```PM>install-package OwinHost```

It is possible to start server by running OwinHost from project folder.

Project and solution have both the same name, so commands will be as following:

```
cd ChatServer\ChatServer
..\packages\OwinHost.3.0.1\tools\OwinHost.exe
```

It is valid for latest OwinHost package, the version may differ.

## Example
Chat is available at http://chatserver6908.azurewebsites.net/index.html
