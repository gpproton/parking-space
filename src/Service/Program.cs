var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#if DEBUG
builder.Services.AddSpaYarp();
#endif

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseSwagger()
    .UseSwaggerUI();
else app.UseHsts(); ;

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

#if DEBUG
app.UseSpaYarp();
#endif

app.MapFallbackToFile("index.html");

app.Run();
