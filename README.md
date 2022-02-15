## Intro
The project was created with .NET core 3.5.1 and vsCode. It doesn't have any real world use case since all of the participants are on the same machine!
<br>
I just tried to implement Object Oriented and SOLID best practices including dependency injection with C#.

## Overall Logic
As we know in general usually there should be a web socket server which acts as a central unit for signaling/broadcasting and also as a single source of truth for keeping the state of the session and connections. And this server doesn’t act as a client and clients will connect to it for sending data and receiving notifications/data. But in our case the story is bit different so I’ve decided to make the first client, also the server and state keeper.
<br/>
<br/>
So it works like this:
<br/>
Every time that a client wants to join a chat they simply open a websocket to the given port and if there is no server, then runs the server and starts to act as a broadcaster and state manager, without the user even recognising it.
<br/>
Note about checking if the server is up or not: Since everything runs locally we don’t need to check a couple of edge cases that we do in ethernet/internet connections (like checking if local network is working or not, or maybe we don’t have internet!). So I ended up simply by seeing if a server is up and running on that port or not.


## Design architecture
Since our presentation layer is very simple, I’ve prevented using MVP or MVVM and came up with MCV to focus more on logic and introduced a service layer to separate the logic even in more details.

## Layers
![Architecture diagarm](https://github.com/aliafsahnoudeh/c-sharp-local-chat-solid/blob/main/c-sharp-local-chat-solid_diagram.jpg?raw=true)

## Data flow
The flow is pretty standard, directly from upper layer to down and via notification from down to up. I just tried to use another approach in the infrastructure layer to show another way by using an observer pattern.

## Other notes
Everything is decoupled by using interfaces and orchestrated by a central IOC container.

## How to build & run the app:
- Inside the VSCode simply hit CTRL + SHIFT + B to run the build command. It will create a dll file here: ./bin/Debug/netcoreapp3.1
- By running this command you can run an instance of application:

```
dotnet c-sharp-local-chat-solid.dll
```

## Debugging
Just run this command:

```
dotnet run
```
