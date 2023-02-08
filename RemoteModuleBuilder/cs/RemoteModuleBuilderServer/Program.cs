using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RemoteModuleBuilder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

//builder.Host.ConfigureServices((context, svc) => 
//{
//    svc.AddTransient<IModelBuilder, DummyModelBuilder>();
//});

var app = builder.Build();

app.MapHub<ModelBuilderHub>("/ModelBuilder");

app.Run();
