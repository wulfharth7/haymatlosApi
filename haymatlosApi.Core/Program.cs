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

//need elastic search
//maybe add redis or kafka and stuff
//definitely containerize the backend
//learn how to paginate
//also try to improve endpoints, for example use GET on 1 million users, yes its overengineering for this project but it'll be fun to learn 
//another todo, make the token and roles into another table - more readable code.
















/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();*/
/*builder.Services.AddSwaggerGen(opt =>                //using authorization with swagger ui here.
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});*/
/*builder.Services.AddDbContext<PostgresContext>(rdbs => rdbs.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase")));
builder.Services.AddScoped<UserService>();

*/

/*var key = Encoding.ASCII.GetBytes("this_will_also_change_later");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});*/


/*var app = builder.Build();*/

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
/*app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
*/

