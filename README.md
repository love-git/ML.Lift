#Install:

Visual Studio Professional 2015 v14.0.25431.01 Update 3

[.Net Core SDK](https://www.microsoft.com/net/core#windows)

#Build:

ML.Lift.sln

#Notes:

I only wrote a single verticle and did not get even that fully implemented.
The vertical I worked on is the CallBox Manager.
I got enough to talk through the architecture and show an example of my coding standards.


##Layers
###Api
The Api has a CallBoxesController which just has the manager injected into it.  All the controller does is handle any http issues including the routing.  It should catch exceptions and log as well, which it does not do yet.
The Api has a Startup almost all setup.  I did not get the log4net all wired in yet.

###Domain
The manager is the entry into the domain code.  This code should be done, minus all unit/integration testing that should be there.  There is no testing at all in the solution yet.

###Repositories
The Repositories should be complete as well.  They are implemented in Mongo.  I did supply a Fake repository in the Structures vertical and just have the constructure build a bunch of test structure data and store it in memory.

###Utils
This layer is really not a layer, these are things that cross cut the layers.  Validation and Localization for example.

###Composition
This is just the autofac module for the callboxes vertical.  It also has a small static class for Mongo Class registration to get polymorphic document stuff done.

###Proxies
Dont think I did anything here, this is similiar to the domain lib.  It implements the manager api from the abstractions lib.  It will implement it calling the endpoints that the api provides and wrapping up all the http stuff to do that.  This is what the presentation layer would consume if it is in .Net code.  If it were javascript or something it would just hit the endpoints and use its own proxies that it would need to define.
