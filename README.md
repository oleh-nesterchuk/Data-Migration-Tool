# Data-Migration-Tool
The tool manages copying data from SQL Server Database into MongoDB and vice versa.

Current list of todo:
1. Adding, deleting, editing of enitities at front-end.
2. Implement drag'n'drop.
3. Add basic validation (Model.IsValid) at controllers.
4. Add age calculation at MongoDb side (currently returns 0 or any other inserted age).
5. Remove hard-coded connection string for MongoDb.
6. Remove yield return for returning the whole list of emails from MongoDb.
7. Clear up repositories moving additional logic to services.

Current list of bugs:
1. TransferToSqlServer returns 500 code (cannot insert explicit value for identity even though it adds new record to a table (wtf?!)).
2. Drag'n'drop doesn't copy rows but only moves, despite providing 'copy' option with the value of 'true'. (Am i doing somethind wrong?)
3. MongoDb's Email model contains field UserId, even though it has been unmapped in BsonClassMap.RegisterMapClass() configuration method.

Epilogue.
Thank you if you've read so far. The given task has been really interesting and enlightening. It's been a pleasure working on it, even though I can say without the slightest doubt that I've successfully failed it.

Have a nice day!
