using practicacliente;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var clientes = new List<cliente>();

//Get
app.MapGet("/cliente", () =>
{
    return clientes;
});

app.MapGet("/cliente/{id}", (int id) =>
{
    var cliente = clientes.FirstOrDefault(c =>  c.Id == id);
    return cliente;
});

//Post
app.MapPost("/cliente", (cliente client) =>
{
    clientes.Add(client);
    return Results.Ok();

});

//put
app.MapPut("/cliente/{id}", (int id, cliente client) =>
{
    var existingClient = clientes.FirstOrDefault(c => c.Id == id);
    if(existingClient != null)
    {
        existingClient.Name = client.Name;
        existingClient.Lastname = client.Lastname;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }

});

//Delete
app.MapDelete("/cliente/{id}", (int id) =>
{
    var existingClient = clientes.FirstOrDefault(c => c.Id == id);
    if (existingClient == null) 
    {
        clientes.Remove(existingClient);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();  
    }
});


app.UseAuthorization();

app.MapControllers();

app.Run();
