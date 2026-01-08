using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsAPI.Models;

var MyAllowspecifitOrngins= "_MyAllowspecifitOrngins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(Options =>
{
    Options.AddPolicy(MyAllowspecifitOrngins, policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5501") 
              .AllowAnyHeader()  
              .AllowAnyMethod(); 

    });
});

builder.Services.AddDbContext<ProductsContext>(x=>x.UseSqlite("Data Source=products.db"));
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<ProductsContext>();
builder.Services.Configure<IdentityOptions>(Options=>

{
    Options.Password.RequiredLength=6;
    Options.Password.RequireNonAlphanumeric=false;
    Options.Password.RequireLowercase=false;
    Options.Password.RequireUppercase=false;
    Options.Password.RequireDigit=false;

    Options.User.RequireUniqueEmail = true;
    Options.User.AllowedUserNameCharacters= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    
    Options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
    Options.Lockout.MaxFailedAccessAttempts=5;

    
});
builder.Services.AddAuthentication(x=>
{
    x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x=>
{
    x.RequireHttpsMetadata=false;
    x.TokenValidationParameters= new TokenValidationParameters
    {
        ValidateIssuer=false,
        ValidIssuer="mehmetyesil.com",
        ValidateAudience=false,
        ValidAudiences=new string[]{"a","b"},
        ValidateIssuerSigningKey=true,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Appsettings:Secret").Value ?? "")),
        ValidateLifetime=true
    };
       
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    // 1. API Başlık ve Versiyon Bilgisi
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });

    // 2. Kilit Butonunu Tanımla (Security Definition)
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen token'ı girin", // Kutucuğun üzerindeki yazı
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // 3. Kilidi Tüm Kapılara Tak (Security Requirement)
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseCors(MyAllowspecifitOrngins);
app.UseAuthorization();

app.MapControllers();

app.Run();
