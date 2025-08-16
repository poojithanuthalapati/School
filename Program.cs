var builder = WebApplication.CreateBuilder(args);

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = false; // Make the session cookie HTTP-only
    options.Cookie.IsEssential = true; // Mark the session cookie as essential
    //options.SameSite = SameSiteMode.None;
});

// Add services to the container.
builder.Services.AddControllersWithViews(); // Required for Razor views

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names as-is
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files (e.g., CSS, JS)
app.UseRouting();

app.UseAuthorization();

// Enable session middleware
app.UseSession();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//app.UseStaticFiles();
