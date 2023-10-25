using ApiSteaKK;
using SteApi.Server.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.RegisterConfig();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSignalR();

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
})
.AddNewtonsoftJson();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
