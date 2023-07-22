using LibraryManagementBackend.Models;
using LibraryManagementBackend.Repositories.Book;
using LibraryManagementBackend.Repositories.Category;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<LibraryManagementDbContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
//     options.AddPolicy("User", policy => policy.RequireAuthenticatedUser());
// });
//
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.SaveToken            = true;
//         options.TokenValidationParameters = new()
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey         = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"])),
//             ValidateIssuer           = true,
//             ValidIssuer              = builder.Configuration["JWT:Issuer"],
//             ValidateAudience         = true,
//             ValidAudience            = builder.Configuration["JWT:Audience"]
//         };
//     }
// );

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title   = "JWT Auth Sample",
        Version = "v1",
    });
    options.AddSecurityDefinition("Bearer", new()
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.ApiKey,
        Scheme       = "Bearer",
        BearerFormat = "JWT",
        In           = ParameterLocation.Header,
        Description  = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer jhfdkj.jkdsakjdsa.jkdsajk\"",
    });
    options.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer",
                },
            },
            new string[] { }
        },
    });
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseForwardedHeaders(new()
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
});

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/Test", () => "Hello World!");

app.Run();