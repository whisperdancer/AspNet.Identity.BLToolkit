AspNet.Identity.BLToolkit
=========================

This library is provided "AS IS" with no warranties, and confer no rights. 

BLToolkit storage provider for Asp.Net Identity 2.0
This implementation uses MsSql server. However, BLToolkit has providers for other databases. See the BLToolkit documentation for details. 

Notes:

Set your conn string in globals.cs. 

The default database that comes with VS 2013 templates uses Entity Framework and the database has a string key for the AspNetUsers table. Identity 2.0 allows for the ability to use any key type. This implemtation uses an int key type.

The scripts for the database are included in the database1 project and can be used to recreate the database for your project. 

Fully working sample mvc5 site included.

References:
http://www.asp.net/identity/overview/extensibility/implementing-a-custom-mysql-aspnet-identity-storage-provider

http://www.asp.net/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity

