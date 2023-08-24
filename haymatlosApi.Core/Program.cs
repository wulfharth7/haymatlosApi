using haymatlosApi.haymatlosApi.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build();

host.Run(); 

//TODO LIST
//reverse proxy class needed.
//create a class for responses. 
//need elastic search
//maybe add redis or kafka and stuff
//another todo, make the token and roles into another table - more readable code. 