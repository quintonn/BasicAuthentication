# BasicAuthentication

This repository is an attempt at an easy way to add authentication into an Asp.Net web site without having to guess which packages to include or which files to create.

To get started, compile this code and add a reference to it from your project (Nuget comming soon) and then create a class that inherits from **IStartup**

Then in the **Configure** method, add the following code to get started:

    var options = new UserAuthenticationOptions()
    {
    	AccessControlAllowOrigin = "*",
    	AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30), //Access token expires after 30min
    	RefreshTokenExpireTimeSpan = TimeSpan.FromDays(2), //Refresh token expires after 2 days
    	AllowInsecureHttp = false,
    	TokenEndpointPath = new PathString("/api/v1/token"),
    	UserContext = Container.Resolve<UserContext>()
    };
    
    app.UseBasicUserTokenAuthentication(options);