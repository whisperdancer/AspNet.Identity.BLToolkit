AspNet.Identity.BLToolkit
=========================

BLToolkit provider for Asp.Net Identity 2.0

Note:

The database has been altered from the default. It uses an INT as the primary key for the Users table. You will need to tweak the code to use it with the default string ID type. The scripts for the database are included in the database1 project for you to recreate it.

Includes a solution with a sample mvc5 website that works with the bltoolkit provider.

This provider is based on the great work on the authors in this article.  http://www.asp.net/identity/overview/extensibility/implementing-a-custom-mysql-aspnet-identity-storage-provider

and credit to this article to help me get it integrated with an mvc5 site.
http://www.asp.net/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity


