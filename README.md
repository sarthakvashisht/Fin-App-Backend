This backend can be used as an example for
Posting UserPosts and Commenting on them, the Database used is SSMS.
It also has JWT which Enables Authentication and Authorization.
It has Certain Modules which in turn Support different API's such as 
 1. LOGIN & Register Module-> It has 2 API's for registering and Login.
 2. Posts-> It has CRUD Operations for the user.
 3. Comments-> It also has CRUD operations and has navigation properties which binds the comment to that stock.


Packages to install for this to work.
 1. Microsoft.AspNetCore.Authentication.JwtBearer
 2. Microsoft.AspNetCore.Identity.EntityFrameworkCore
 3. Microsoft.AspNetCore.Mvc.NewtonsoftJson
 4. Microsoft.EntityFrameworkCore.Design
 5. Microsoft.EntityFrameworkCore.SqlServer
 6. Microsoft.EntityFrameworkCore.Tools
 7. Microsoft.Extensions.Identity.Core
