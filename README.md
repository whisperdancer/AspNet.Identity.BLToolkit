AspNet.Identity.BLToolkit
=========================

BLToolkit provider for Asp.Net Identity 2.0
This implementation uses MsSql server. However, BLToolkit has providers for other databases. See the BLToolkit documentation for details. 

Notes:

The default database that comes with VS 2013 templates uses Entity Framework and the database has a string key for the AspNetUsers table. Identity 2.0 allows for the ability to use any key type. This implemtation uses an <int> key type.

To use a different key change the IdentyUser class. It now uses an int like this
public class IdentityUser : IUser<int>
 
For example, to use a string you would change the class to  public class IdentityUser : IUser<string> As <string> type is the default, you can just use this 
public class IdentityUser : IUser
 
Ensure the type of the key matches:    public int Id { get; set; } Change to    public string Id { get; set; }
 
You will also need to change some of the classes in the UserTable class to reflect the new key type. BLToolkit uses nice clean sql so it will be easy to see what methods need to change. Just tweak the required sql. The Insert method will need to change the type from int to string, etc.

Then many of the classed in the web app will need to be modified to work with the BLToolkit provider as well as with an <int> type. Fortunately I have done this in the sample app so its ready to use out of the box. It was built with the latest VS 2013 default MVC templates and then modified to fit with the BLToolkit provider. EF has been completely removed. 

The scripts for the database are included in the database1 project and can be used to recreate the database for your project. 
References:
http://www.asp.net/identity/overview/extensibility/implementing-a-custom-mysql-aspnet-identity-storage-provider
http://www.asp.net/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity


