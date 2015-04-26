# orleans
Orleans is a framework created by Microsoft to handle complex operations in distributed systems. The basic concept is to use actors instead of objects doing single threaded operations to avoid the issues of concurrency. The actors communicate by sending and receiving messages asynchronously using queues. An actor is never created nor destroyed at runtime, instead it's activated or deactivated. In the Orleans context, actors are called grains. Grains live in silos, and can communicate with grains in other silos by serializing their messages over the wire.

Each time a method is invoked on a grain, OnActivateAsync() is invoked right before it to activate the grain. OnDeactivateAsync() is also used, more infrequently.

<h2>Projects</h2>

HR - Sample project for learning Orleans.

<h2>Prerequisites</h2>

Microsoft Project Orleans SDK
</br>
Visual Studio 2013
</br>
.NET 4.5 or higher
</br>
Azure SDK for .NET (to use Orleans in the cloud)
</br></br>
More info. can be found at https://github.com/dotnet/orleans
