using TUSO.Web.HttpClients;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<HomeHttpClient>();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookies")
                   .AddCookie("Cookies", config =>
                   {
                       config.Cookie.Name = "__Tusoinfo__";
                       config.LoginPath = "/Login";
                       config.AccessDeniedPath = "/Forbidden";
                       config.LogoutPath = "/Logout";
                   });
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
