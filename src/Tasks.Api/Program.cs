using Tasks.Api.Configure;

var builder = WebApplication.CreateBuilder(args);

builder.AddBuilder();

builder.Services.AddLogger(builder.Configuration);
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();