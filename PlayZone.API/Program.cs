using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using PlayZone.API.Services;
using PlayZone.BLL.Interfaces.Budget_Related;
using PlayZone.BLL.Interfaces.User_Related;
using PlayZone.BLL.Interfaces.Worktime_Related;
using PlayZone.BLL.Services.Budget_Related;
using PlayZone.BLL.Services.User_Related;
using PlayZone.BLL.Services.Worktime_Related;
using PlayZone.DAL.Interfaces.Budget_Related;
using PlayZone.DAL.Interfaces.User_Related;
using PlayZone.DAL.Interfaces.Worktime_Related;
using PlayZone.DAL.Repositories.Budget_Related;
using PlayZone.DAL.Repositories.User_Related;
using PlayZone.DAL.Repositories.Worktime_Related;

var builder = WebApplication.CreateBuilder(args);

// Injection de la connection DB
builder.Services.AddTransient<NpgsqlConnection>(service =>
{
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new NpgsqlConnection(connectionString);
});

/*-----------------------------------------*/

//Injection des services BLL - User_Related
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();

//Injection des services BLL - Worktime_Related
builder.Services.AddScoped<IWorktimeService, WorktimeService>();
builder.Services.AddScoped<IWorktimeCategoryService, WorktimeCategoryService>();

//Injection des services BLL - Budget_Related
builder.Services.AddScoped<IPrevisionRentreeService, PrevisionRentreeService>();

//Injection des services API
builder.Services.AddScoped<JwtService>();

/*-----------------------------------------*/

//Injection des services DAL - User_Related
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

//Injection des services DAL - Worktime_Related
builder.Services.AddScoped<IWorktimeRepository, WorktimeRepository>();
builder.Services.AddScoped<IWorktimeCategoryRepository, WorktimeCategoryRepository>();

//Injection des services DAL - Budget_Related
builder.Services.AddScoped<IPrevisionRentreeRepository, PrevisionRentreeRepository>();

/*-----------------------------------------*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            // TODO Cleanup swagger https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file#add-security-definitions-and-requirements
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
// Configuration de l'auth et du JWT
builder.Services.AddAuthentication(option =>
    {
        // Indique que le système d'authentification et de permission va se baser sur le schema du JWT Bearer
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(option =>
    {
        // Configure la validation du token
        option.TokenValidationParameters = new TokenValidationParameters
        {
            // Vérifie que la clé utilisée pour signer le token est valide (TRUE ! Important !)
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            // Vérifie que le token provient du bon émetteur (optionnel)
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            // Vérifie que le token provient du bon public (optionnel)
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            // Vérifie que le token n'a pas encore expiré
            ValidateLifetime = true,
            //ClockSkew = TimeSpan.Zero
        };
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
