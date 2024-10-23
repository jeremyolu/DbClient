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

### How to use

First create an instance of the SqlDbClient class and provide a valid connection string to properly initialise the client class.

<pre>
  // initialisation of client class with connection string
  var client = new SqlDbClient(@"Server=.server;Database=database;Trusted_Connection=True;TrustServerCertificate=True;");

  // getting data from a table with just an SQL query - the method will return an IEnumerable of the type you specify in the delegate. The example below shows courses being returned based on the created Course type
  var courses = await client.GetAsync("SELECT * FROM Courses", (reader) => new Course
  {
      // the reader object has xtention methods to allow you to map your type properties to safe SQL types without having to do any conversion yourelf.
      // These include Int, String, Float, Decimal and UniqueIdentifier - these types are all nullable
      Id = reader.UniqueIdentifier("Id"),
      CourseId = reader.String("CourseId"),
      Title = reader.String("Title"),
      Price = reader.Float("Price").Value
  });

  // getting data from a table with inputs

  // inputs are defined as a dictionary of type string, object - you can add as many inputs as you like based on your query
  var inputs = new Dictionary<string, object?>
  {
      { "courseId", "C1001" }
  };
    
  // command type is needed when passing inputs - example shows text being used as it is a SQL query with the inputs passed in the parameters.
  var courses = await client.GetAsync("SELECT * FROM Courses WHERE CourseId = @CourseId", (reader) => new Course
  {
      Id = reader.UniqueIdentifier("Id"),
      CourseId = reader.String("CourseId"),
      Title = reader.String("Title"),
      Price = reader.Float("Price").Value
 }, CommandType.Text, inputs);

  // getting courses using a stored procedure called "spGetCourses"
  // This particular procedure has an input that filters courses price greater than 10 so a input dictionairy is needed and is passed.
  // If a procedure does not include an input, you can omit the inputs from the method.
  var courses = await client.GetAsync("spGetCourses", (reader) => new Course
  {
      Id = reader.UniqueIdentifier("Id"),
      CourseId = reader.String("CourseId"),
      Title = reader.String("Title"),
      Price = reader.Float("Price").Value
  }, CommandType.StoredProcedure, inputs);
</pre>



