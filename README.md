# AgentOrders
This is my work on task "Agent Orders" given in file assignment_c#.docx.

Instructions to run the app:
1) Open file AgentOrders.sln in Visual Studio;
2) In file Web.config, partition <connectionStrings>, change key connectionString to the instance of your Sql Server; choose any database (preferably empty) on your server;
3) Run the application - it will start to listen on address https://localhost:44358/
4) Depending on your REST client, sometimes it is required to temporary cancel certificate validation in the REST client.

The following endpoints are implemented:
1) Get an agent with the highest total advance amount in certain year:
```yaml
GET
/api/Agent/HighestAdvance?year=XXXX
Response:
{
    "AgentCode": "string"
}
```
2) Get list of orders, one per agent, where each item is N-th oldest order of one agent. If an agent has less than N orders, that agent is not listed.
```yaml
GET
/api/Order/OrdersByIndex?agentCodes=XXXX,YYYY,...&orderIndex=ZZZZ
Response:
[
    {
        "OrderNum": integer,
        "Amount": decimal,
        "AdvanceAmount": decimal,
        "OrderDate": "yyyy-MM-ddTHH:mm:ss",
        "CustomerCode": "string",
        "AgentCode": "string",
        "Description": "string"
    }
]
```
3) Get list of agents who have orders count in the given year more or equal to the given number.
```yaml
GET
/api/Order/AgentsByMinOrders?minCount=X&year=YYYY
Response:
[
    {
        "AgentCode": "string",
        "AgentName": "string",
        "PhoneNo": "string"
    }
]
```

On application start (or, to be precise, on the 1st incoming web request), it checks the provided Sql server database, whether it contains table AGENTS;
if not, it runs script InitDatabase.sql, which provides table structure, data and stored procedures required for the app to run.
For table structure and sample data I have used the ones that were provided in the task description.

Also, bonus task, i.e. api key checking is also implemented.
In order to test the feature, in Web.config file, partition <appSettings>, the non-empty string for "ApiKey" should be provided.
When that value is provided, the application will require each request to contain header "XApiKey" with value equal to the value of the "ApiKey" in Web.config file.

In case something goes wrong in data initialization script, I have also provided backup of my SQL database - in the file Documents\AgentOrders.zip
