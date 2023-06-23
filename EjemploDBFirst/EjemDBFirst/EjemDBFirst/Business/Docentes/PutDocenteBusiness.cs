using EjemDBFirst.Data;
using EjemDBFirst.Resultado.Docentes;
using FluentValidation;
using MediatR;
using static EjemDBFirst.Business.Docentes.GetAllDocentesBusiness;
using System.Net;
using Microsoft.EntityFrameworkCore;
using EjemDBFirst.Models;

namespace EjemDBFirst.Business.Docentes
{
    public class PutDocenteBusiness
    {
        public class PutDocenteComando : IRequest<ListadoDocentes>
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int Edad { get; set; }
            public string Email { get; set; }
        }

        public class EjecutarValidacion : AbstractValidator<PutDocenteComando>
        {
            public EjecutarValidacion()
            {
                RuleFor(d => d.Id).NotEmpty().WithMessage("El campo id debe ser obligatorio");
                RuleFor(d => d.Nombre).NotEmpty().WithMessage("El campo nombre debe ser obligatorio");
                RuleFor(d => d.Apellido).NotEmpty().WithMessage("El campo nombre debe ser obligatorio");
                RuleFor(d => d.Edad).NotEmpty().WithMessage("El campo nombre debe ser obligatorio");
                RuleFor(d => d.Email).NotEmpty().WithMessage("El campo nombre debe ser obligatorio");
            }
        
        }

        public class Manejador : IRequestHandler<PutDocenteComando, ListadoDocentes>
        {

            private ContextDB _contexto;
            private readonly IValidator<PutDocenteComando> _validator;

            public Manejador(ContextDB contexto, IValidator<PutDocenteComando> validator)
            {
                _contexto = contexto;
                _validator = validator;

            }
            public async Task<ListadoDocentes> Handle(PutDocenteComando comando, CancellationToken cancellationToken)
            {
                var result = new ListadoDocentes();

                var validacion = await _validator.ValidateAsync(comando);
                if (!validacion.IsValid)
                {
                    var errores = string.Join(Environment.NewLine, validacion.Errors);
                    result.SetMensajeError(errores, HttpStatusCode.InternalServerError);
                    return result;
                }

                var docente = await _contexto.Docentes.Where(d => d.Id == comando.Id).Include(n => n.IdNivelNavigation).FirstOrDefaultAsync();


                if (docente != null)
                {
                    docente.Apellido = comando.Apellido;
                    docente.Nombre = comando.Nombre;
                    docente.Email = comando.Email;
                    docente.Edad = comando.Edad;
                    docente.Id = comando.Id;

                    var nuevoLog = new Log
                    {
                        FechaLog = DateOnly.FromDateTime(DateTime.Now),
                        IdDocente = docente.Id,
                        Log1 = "actualizacion completada el dia de la fecha"
                    };
                    await _contexto.Logs.AddAsync(nuevoLog);
                    _contexto.Update(docente);
                    await _contexto.SaveChangesAsync();
                    var itemDocente = new ItemDocente
                    {
                        Id = docente.Id,
                        Apellido = docente.Apellido,
                        Nombre = docente.Nombre,
                        Email = docente.Email,
                        Edad = docente.Edad,
                        Nivel = docente.IdNivelNavigation.Nombre
                    };

                    result.ListDocentes.Add(itemDocente);

                    return result;
                }
                var mensajeError = "Docentes no encontrados";
                result.SetMensajeError(mensajeError, HttpStatusCode.NotFound);
                return result;



            }
        }
     }
}
