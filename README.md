# Introduction
This solution is made of a WCF service with three different hosts (a console app, a web host and a windows service host). It also has a WPF application which make use of the WCF service to do its tasks.
An independent Web API is also included with limited functionality (CRUD only on municipality).

# How To Works With Solution
> In order to work with solution you need to have SQL Server/Express installed on your machine.

  1. Run one of the hosts, they will take care of generating a database for the first time.
  2. WPF application is configure to work with console host application. You can set the solution to start these two together or run the console host outside of Visual Studio and then run the WPF application.
  
> To test Web API a helper application like [Postman](https://www.postman.com/downloads/) is needed

The Web API only expose an endpoint to work with municipalities.
The endpoint address is:
http://localhost:51112/api/municipalities

To add a new municipality, you need to send a POST request to this endpoint with the Content-Type set to "application/json" and a body with the following format:
> {
>    "name": "New Municipality Name"
> }
