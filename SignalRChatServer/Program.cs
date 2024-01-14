using SignalRChatServer.Hubs;

var builder = WebApplication.CreateBuilder(args);


// add SignalR library
builder.Services.AddSignalR();

// configure CORS to accept all client requests from browser
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowCredentials()
																			 .AllowAnyHeader()
																			 .AllowAnyMethod()
																			 .SetIsOriginAllowed(x => true)));

var app = builder.Build();

app.UseCors();
app.UseRouting();

app.MapHub<ChatHub>("/chatHub");

app.MapGet("/", () => "Hello World!");

app.Run();
