using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddDefaultPolicy(policy=>
  //policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
  policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddPersistenceServices();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
