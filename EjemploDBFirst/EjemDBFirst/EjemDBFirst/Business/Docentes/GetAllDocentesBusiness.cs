using EjemDBFirst.Data;
using EjemDBFirst.Resultado.Docentes;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

using static EjemDBFirst.Business.Docentes.PutDocenteBusiness;

namespace EjemDBFirst.Business.Docentes
{
    public class GetAllDocentesBusiness
    {
        public class GetAll_DocenteComando : IRequest<ListadoDocentes>
        {
            
        }

        public class EjecutarValidacion : AbstractValidator<GetAll_DocenteComando>
        {
            
        }

        
       public class Manejador : IRequestHandler<GetAll_DocenteComando, ListadoDocentes>
        {
            private readonly ContextDB _contexto;
            private readonly IValidator<GetAll_DocenteComando> _validator;
            public Manejador(ContextDB contextDb, IValidator<GetAll_DocenteComando> validator)
            {
                _contexto = contextDb;
                _validator = validator;
            }

            public async Task<ListadoDocentes> Handle(GetAll_DocenteComando comando, CancellationToken cancellation)
            {
                var result = new ListadoDocentes();
                var validacion = await _validator.ValidateAsync(comando);

                if (!validacion.IsValid)
                {
                    var errores = string.Join(Environment.NewLine, validacion.Errors);
                    result.SetMensajeError(errores, HttpStatusCode.InternalServerError);
                    return result;
                }

                var docentes = await _contexto.Docentes
                    .Include(d => d.IdNivelNavigation)
                    .Where(c => c.Edad >= 30 && c.Edad <= 40 && c.Email.Contains("outlook"))
                    .Where(d => d.IdNivelNavigation.Nombre.Contains("Secundario"))
                    .FirstOrDefaultAsync();

                if (docentes != null)
                {

                    var itemDocente = new ItemDocente
                    {
                        Id = docentes.Id,
                        Nombre = docentes.Nombre,
                        Apellido = docentes.Apellido,
                        Edad = docentes.Edad,
                        Email = docentes.Email,
                        Nivel = docentes.IdNivelNavigation.Nombre
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
