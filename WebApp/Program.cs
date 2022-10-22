var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();




builder.Configuration.AddAzureAppConfiguration(conf =>
{

    var baseConf = builder.Configuration;
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var azureConf = baseConf.GetValue<string>("AZURE_APPCONFIGURATION_CONNECTIONSTRING");
    conf.Connect(azureConf);
    conf.Select("test", env);
    
});


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
