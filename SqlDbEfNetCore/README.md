# Load SOLIDWORKS Properties From SQL Database in Entity Framework Core

This example demonstrates how to load description, vendor and type properties from the SQL database using the Entity Framework Core.

* Add-in read the value of *PartNo* property and looks up the data in the SQL table.
* Add-in writes the values to the corresponding properties

use the script below to create SQL table.

~~~ sql
CREATE TABLE [dbo].[Parts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartNumber] [varchar](10) NULL,
	[Description] [varchar](250) NULL,
	[Vendor] [varchar](25) NULL,
	[Type] [varchar](50) NULL
) ON [PRIMARY]
~~~

Specify the connection string and the part number property as conttants in the 

~~~ cs
private const string PART_NO_PRP = "PartNo";
private const string SQL_CONNECTION_STRING = "Server=localhost;Database=data;Trusted_Connection=True;";
~~~