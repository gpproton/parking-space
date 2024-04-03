using Microsoft.OpenApi.Models;
using ParkingSpace.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(typeof(Lazy<>), typeof(Lazy<>));
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = $"Parking Space", Version = "v1" });
});
builder.Services.RegisterDataContext();
builder.Services.RegisterModules();
#if DEBUG
builder.Services.AddSpaYarp();
#endif

var app = builder.Build();

 app.UseSwagger()
    .UseSwaggerUI(opt => {
        const string path = "/swagger/v1/swagger.json";
        opt.SwaggerEndpoint(path, "Parking Space V1 Docs");
    });

if (!app.Environment.IsDevelopment())
    app.UseHsts();

const string swaggerTittle = "Parking Space V1 Docs";
app.UseReDoc(c => {
    c.DocumentTitle = swaggerTittle;
});

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.RegisterApiEndpoints();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

#if DEBUG
if (UseProxy) app.UseSpaYarp();
#endif
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program {
    public static bool UseProxy { get; set; } = true;
}