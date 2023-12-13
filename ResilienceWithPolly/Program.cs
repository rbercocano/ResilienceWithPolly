using Polly.Extensions.Http;
using Polly;
using ResilienceWithPolly.HttpClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IPostClient, PostClient>()
    .AddPolicyHandler(GetRetryPolicy())
    .AddPolicyHandler(GetCicuitBreakerPolicy());
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
    .WaitAndRetryAsync(retryCount: 3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}
IAsyncPolicy<HttpResponseMessage> GetCicuitBreakerPolicy()
{
    return HttpPolicyExtensions.HandleTransientHttpError().CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));
}