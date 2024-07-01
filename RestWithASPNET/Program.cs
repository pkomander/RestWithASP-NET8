using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithASPNET.Domain.Identity;
using RestWithASPNET.Helpers;
using RestWithASPNET.Hypermedia.Enricher;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Repository.Data;
using RestWithASPNET.Repository.Services.Interface;
using RestWithASPNET.Repository.Services.Repository;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddDbContext<Context>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("RestWithAspNetConnection")));

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddScoped<IGenericService, GenericRepository>();
builder.Services.AddScoped<IPersonService, PersonRepository>();
builder.Services.AddScoped<IBookService, BookRepository>();
builder.Services.AddScoped<IAccountService, AccountRepository>();
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenRepository>();
builder.Services.AddScoped<IUtil, Util>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
})
.AddXmlSerializerFormatters();

//Adicionando referencia para execução do HATEOAS na biblioteca RestWithASPNET.Hypermedia
var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);


//aplicando as configuracoes de senha
//todos estao marcados false pois nao esta sendo necessario no momento que a senha contenha
//caracteres especiais, letra maiuscula e etc
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
}).AddRoles<Role>()
.AddRoleManager<RoleManager<Role>>()
.AddSignInManager<SignInManager<User>>()
.AddRoleValidator<RoleValidator<Role>>()
.AddEntityFrameworkStores<Context>()
.AddDefaultTokenProviders();

var tokenKey = "super-secret-key";
var keyBytes = Encoding.UTF8.GetBytes(tokenKey);
if (keyBytes.Length < 64) // 64 bytes = 512 bits
{
    var paddedKeyBytes = new byte[64]; // Tamanho mínimo da chave para HMAC-SHA512
    Array.Copy(keyBytes, paddedKeyBytes, keyBytes.Length);
    keyBytes = paddedKeyBytes;
}

var key = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espaço] então coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
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

app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader()
   .AllowAnyMethod()
   .AllowAnyOrigin());

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");

app.Run();
