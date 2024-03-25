using BackEndApi.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

using BackEndApi.Services.Interfaces;
using BackEndApi.Services.Implementacion;

using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbclientesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



#region PETICIONES API REST
app.MapGet("/cliente/getall", async (
    IClienteService _clienteService,
    IMapper _mapper
    ) =>
{
    var listaCliente = await _clienteService.GetAll();
    var listaClienteDTO = _mapper.Map<List<ClienteDTO>>(listaCliente);

    if (listaClienteDTO.Count > 0)
        return Results.Ok(listaClienteDTO);
        
    else
        return Results.NotFound();
});

app.MapGet("/cliente/getbyid", async (
    int id,
    IClienteService _clienteService,
    IMapper _mapper
    ) =>
{
    var cliente = await _clienteService.Get(id);
    var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

    if (clienteDTO is not null)
        return Results.Ok(clienteDTO);

    else
        return Results.NotFound();
});

app.MapGet("/cliente/search", async (
    string name,
    IClienteService _clienteService,
    IMapper _mapper
    ) =>
{
    var listaCliente = await _clienteService.Search(name);
    var listaClienteDTO = _mapper.Map<List<ClienteDTO>>(listaCliente);

    if (listaClienteDTO.Count > 0 )
        return Results.Ok(listaClienteDTO);

    else
        return Results.NotFound();
});

app.MapPost("/cliente/create", async (
    ClienteDTO modelo,
    IClienteService _clienteService,
    IMapper _mapper
    ) =>
{
    var _cliente = _mapper.Map<Cliente>(modelo);
    var _clienteNuevo = await _clienteService.Insert(_cliente);

    if (_clienteNuevo.Id != 0)
        return Results.Ok(_mapper.Map<ClienteDTO>(_clienteNuevo));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

app.MapPut("/cliente/update/{id}", async (
    int id,
    ClienteDTO modelo,
    IClienteService _clienteService,
    IMapper _mapper
    ) =>
{
    var _cliente = await _clienteService.Get(id);

    if(_cliente is null) return Results.NotFound();

    var _clienteModificado = _mapper.Map<Cliente>(modelo);

    _cliente.Nombres = _clienteModificado.Nombres;
    _cliente.Apellidos = _clienteModificado.Apellidos;
    _cliente.FechaDeNacimiento = _clienteModificado.FechaDeNacimiento;
    _cliente.Cuit = _clienteModificado.Cuit;
    _cliente.Domicilio = _clienteModificado.Domicilio;
    _cliente.TelefonoCelular = _clienteModificado.TelefonoCelular;
    _cliente.Email = _clienteModificado.Email;

    var respuesta = await _clienteService.Update(_cliente);

    if (respuesta)
        return Results.Ok(_mapper.Map<ClienteDTO>(_cliente));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

app.MapDelete("/cliente/delete/{id}", async(
    int id,
    IClienteService _clienteService,
    IMapper _mapper
    ) =>
{
    var cliente = await _clienteService.Get(id);

    if (cliente is null) return Results.NotFound();

    var respuesta = await _clienteService.Delete(cliente);

    if (respuesta)
        return Results.Ok();
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

#endregion

app.UseCors("NuevaPolitica");

app.Run();

