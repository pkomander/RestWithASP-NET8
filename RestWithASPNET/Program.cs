using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using RestWithASPNET.Repository.Data;
using RestWithASPNET.Repository.Services.Interface;
using RestWithASPNET.Repository.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Context>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("RestWithAspNetConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IGenericService, GenericRepository>();
builder.Services.AddScoped<IPersonService, PersonRepository>();
builder.Services.AddScoped<IBookService, BookRepository>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
