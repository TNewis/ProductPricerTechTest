<H1>Setup</H1>
<H3>Database</H3>
This application is built to use SQL Server. There is a script located in Database\DBSetup.sql that will create the database, tables and stored procedures required.

<H3>Product API</H3>
The API has two settings that must be configured:
ProductDBContext- This is the connection string of your database.
OpenExchangeAppId- This is the App Id for accessing the Open Exchange Rates API. This must be set for currency conversion to function. This app is designed to use only features available in the free version of the API
OpenExchangeRates can be found at https://openexchangerates.org
<H3>Angular Web Application</H3>
The web application should not require any specific configuration to run locally alongside the Product API. If running remotely, there is an apiUrl setting in environment.prod.ts- by default it is configured to run against localhost.

<H2>Settings</H2>
The API contains two settings in app.config:
