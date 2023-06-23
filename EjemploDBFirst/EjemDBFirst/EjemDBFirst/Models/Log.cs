using System;
using System.Collections.Generic;

namespace EjemDBFirst.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateOnly FechaLog { get; set; }
        public string Log1 { get; set; } = null!;
        public int IdDocente { get; set; }
    }
}
