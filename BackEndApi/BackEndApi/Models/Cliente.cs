﻿using System;
using System.Collections.Generic;

namespace BackEndApi.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateTime? FechaDeNacimiento { get; set; }

    public string Cuit { get; set; } = null!;

    public string? Domicilio { get; set; }

    public string TelefonoCelular { get; set; } = null!;

    public string Email { get; set; } = null!;
}
