# DbClient

DbClient is a simple, lightweight and flexible package for interacting with various databases in .NET applications. This library allows you to execute queries and gives you control over the mapping of results to C# objects seamlessly.

## Features

- Easy database connection handling by providing connection strings directly in the constructor
- Supports executing various SQL queries including SELECT, INSERT, UPDATE and DELETE
- Supports the use of stored procedures with inputs
- Control over mapping of results to C# objects
- Asynchronous support for non-blocking operations

## Clients

### SqlDbClient

The SqlDbClient class allows connections to Azure databases and SQL Server databases by providing a connection string to connect to the database server.
The two main methods **Get** which gets rows from a table and **Execute** which executes queries/stored procedures on a table
There are 2 command types - **Text** which is used when specifying raw SQL queries and **StoredProcedure** which is used for stored procedures.
Synchronous and asynchronous methods are provided.
ISqlDbClient type is also provided.

### How to use

First create an instance of the SqlDbClient class and provide a valid connection string to properly initialise the client class.
Call either get or execute methods and provide the query or stored procedure, command type and inputs. 
Inputs can be omitted if there are none and command type can also be omitted if only retriving data from a table.

### Examples
