# StopJetLag_Azure
A backend implementation using Azure app services coordinating with Azure Web Apps and Azure SQL.
The solution leverages Azure app service – – both web apps and API apps – – to deliver jet lag advice to the mobile app, as well as a web admin portal with a database API layer to edit StopJetLag plan trip notes. 

Azure Web App 

StopJetLag Admin Portal
The on premise and web-hosted infrastructure for receiving orders, creating and reviewing StopJetLag plans as well as delivering jet lag advice was not cost effective and generated large management overhead of the applications required.  I developed a StopJetLag admin portal to edit trip notes, itinerary and save the changes back in the Azure SQL database. By using the Admin portal,  a StopJetLag team member can work off premise when needed when team scaling is required. In the implementation, I used the following technology
•	.Net core, MVC, and dependency injection
•	Entity Framework core
•	asynchronous programming model
•	eager loading for better performance
I deployed the Admin Portal to an Azure Web App and configured continuous deployment for both VSTS and GitHub. Both integration are working efficiently.
The Admin Portal web app repository: https://github.com/doristchen/StopJetLag_Azure/tree/master/SJL_Web 
Azure API App
The goal of the StopJetLag API layer is to provide a REST interface for the StopJetLag Mobile app (implemented in Xamarin.Forms) to retrieve jet lag advice activity data as well as the created trip notes from the Azure SQL database. I implemented the API layer with .NET Core MVC pattern, and Entity Framework Core. The database has 23 tables and 93 stored procedures, I implemented customized data reader for stored procedure calls in the Azure SQL database to retrieve the required information for the StopJetLag Mobile app. Communication with the mobile client is facilitated by a DTO pattern. Each data transfer object can be used by both the client and the server side, making shared use of objects easier.  REST endpoints were created using.NET Core controllers to be delivered by the Azure API app for both StopJetLag jet lag advice and trip notes. The need to scale automatically as well the ease of development and integration with Azure based on .NET Core made the use of an Azure API App the obvious choice for the infrastructure development.  Here are the technology I used in the implementation
•	.Net core, MVC, and dependency injection 
•	Entity Framework core
•	Stored procedures, data reader for DTO model
•	asynchronous programming model
•	attribute routing
•	eager loading for better performance
The StopJetLag Azure API repository: https://github.com/doristchen/StopJetLag_Azure/tree/master/SJL_AzureAPI
