global using MesaCorrespondencia.Shared;
global using MesaCorrespondencia.Server.Data;
global using MesaCorrespondencia.Server.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MesaCorrespondencia.Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
        opt => opt.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"),
            b => b.UseOracleSQLCompatibility("11")));
builder.Services.AddScoped<IDeptoueRepository, DeptoueRepository>();
builder.Services.AddScoped<IOficiosRepository, OficiosRepository>();
builder.Services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();
builder.Services.AddScoped<IOficiosParametroRepository, OficiosParametroRepository>();
builder.Services.AddScoped<IFunctionsRepository, FunctionsRepository>();
builder.Services.AddScoped<IGCloudSRepository, GCloudSRepository>();


builder.Services.AddScoped<IAuthRepository, AuthRepository>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
            new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        }
    );
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

//Authentication Middleware
app.UseAuthentication();
app.UseAuthorization();
///


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();