using Auth.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Chamada aos métodos de extensão que criamos
builder.Services.ConfigureAuthentication();

/* Chamada ao método de extensão criado em um outro projeto 
do tipo classlib, para configurar o container de Injeção de Dependências.*/
builder.Services.ConfigureDependencyInjection();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x =>
x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

/* A configuração precisa seguir esta ordem, primeiro 
Authentication e depois Authorization*/
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();