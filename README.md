# MyContacts

MyContacts - is application, providing possibility to manage your contacts, phone numbers, conferentions and calls.


# How to use this app

To start using this app please, follow instructions.


## Connection string

First you have to download this repo.
Then you should change default connection string. Default connection string locates in "../MyContacts/appsettings.json".

From this:

```json
"ConnectionStrings": {
    "default": "Data Source=URAQDMPC\\SQLEXPRESS; Database=MyContactsDb; Persist Security Info=false; User Id='sa'; Password ='sa'; MultipleActiveResultSets=true; Trusted_Connection=false;"
  }
  
```

To this:

```json
"ConnectionStrings": {
    "default": "@Your MS SQL Server connection string@"
  }

```

Warning! This application uses only MS SQL.


## Enter in system

Then you should use one of the following numbers to "login":

- 88005553535
- 89054122233

Done! You are ready to use this app!
