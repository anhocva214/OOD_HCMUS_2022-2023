using financial_management_service;
using financial_management_service.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });
builder.Services.AddResponseCompression();

ServiceRegister.Run(builder);

var allowOrigin = "AllowOrigin";

List<string> lsDomain = new List<string>(builder.Configuration["EnableCors"].Split(','));

builder.Services.AddCors(p => p.AddPolicy(name: allowOrigin, builder =>
{
    builder.WithOrigins(lsDomain.ToArray()).AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(x => lsDomain?.Count(d => d == x) > 0);
}));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app cors
app.UseCors(allowOrigin);

//app cors
app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.MapHealthChecks("/health/ping");

app.UseMiddleware<GlobalExceptionHandler>();

app.UseResponseCompression();

app.Run();

