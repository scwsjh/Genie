var builder = WebApplication.CreateBuilder(args).Inject();
builder.Services.AddCaptcha(builder.Configuration);
var app = builder.Build();
app.Run();