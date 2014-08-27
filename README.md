AspNet.Identity.BLToolkit
=========================

This library is provided "AS IS" with no warranties, and confer no rights. 

BLToolkit storage provider for Asp.Net Identity 2.0.

This library is a drop in replacement for the Entity Framework. A MsSql database is used for the data store. However, BLToolkit has providers for other databases. See the BLToolkit documentation for details. 

Notes:

Set your conn string in globals.cs. 

Default database primary key on AspNetUsers table modified to use an int.

Database scripts and fully working sample mvc5 site included.

References:
http://www.asp.net/identity/overview/extensibility/implementing-a-custom-mysql-aspnet-identity-storage-provider

http://www.asp.net/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity

