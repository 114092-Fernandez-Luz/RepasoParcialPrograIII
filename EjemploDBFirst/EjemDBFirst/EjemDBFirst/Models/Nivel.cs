﻿using System;
using System.Collections.Generic;

namespace EjemDBFirst.Models
{
    public partial class Nivel
    {
        public Nivel()
        {
            Docentes = new HashSet<Docente>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Docente> Docentes { get; set; }
    }
}
