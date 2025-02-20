using Nexus.Crypto.SDK;
using SdkWebSample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INexusApiGetAccessToken, GetToken>();
builder.Services.AddNexusCryptoSdk(o =>
{
    o.ThrowOnMissingAccessToken = false;
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/customers", async (NexusAPIService api) =>
{
    var customers = await api.GetCustomers([]);

    return customers.Values.Records;
});

app.Run();