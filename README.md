# Data-Migration-Tool
The tool manages copying data from SQL Server Database into MongoDB and vice versa.

The task description is located in "Task" folder or by the link https://imgur.com/a/FsiVGCR.






Current list of todo:
1. Adding, deleting, editing of enitities at front-end.
2. Implement drag'n'drop.
3. Add age calculation at MongoDb side (currently returns 0 or any other inserted age). (done)
4. Remove hard-coded connection string for MongoDb. (done)
5. Remove yield return for returning the whole list of emails from MongoDb. (done)
6. Clear up repositories moving additional logic to services. (half-way)
7. Add basic validation (Model.IsValid) at controllers. (done)

Current list of bugs:
1. TransferToSqlServer returns 500 code (cannot insert explicit value for identity even though it adds new record to a table (wtf?!)). (fixed)
2. Drag'n'drop doesn't copy rows but only moves, despite providing 'copy' option with the value of 'true'. (Am i doing somethind wrong?)
3. MongoDb's Email model contains field UserId, even though it has been unmapped in BsonClassMap.RegisterMapClass() configuration method.

Epilogue.
Thank you if you've read so far. The given task has been really interesting and enlightening. It's been a pleasure working on it, even though I can say without the slightest doubt that I've failed it.

Have a nice day!
