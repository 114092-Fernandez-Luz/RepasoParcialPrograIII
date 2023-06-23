using EjemDBFirst.Business.Docentes;
using EjemDBFirst.Data;
using EjemDBFirst.Resultado.Docentes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EjemDBFirst.Business.Docentes.PutDocenteBusiness;

namespace EjemDBFirst.Controllers
{
   
    [ApiController]
    public class DocenteController : ControllerBase
    {
        private readonly ContextDB _contexto;
        private readonly IMediator _mediator;

        public DocenteController(ContextDB contexto, IMediator mediator)
        {
            _contexto = contexto;
            _mediator = mediator;
        }

        //[HttpGet]
        //[Route("api/docentes/getDocentesById/{id}")]
        //public async Task<ListadoDocentes> GetPersonasById(int id)
        //{
        //    return await _mediator.Send(new GetByIdBusiness.GetDocenteByIdComando
        //    {
        //        IdDocente = id
        //    });
        //}

        [HttpGet]
        [Route("api/docentes/getDocentes")]
        public async Task<ListadoDocentes> GetPersonas()
        {
            return await _mediator.Send(new GetAllDocentesBusiness.GetAll_DocenteComando());

        }
        [HttpPut]
        [Route("api/docentes/actualizarDocentes")]
        public async Task<ListadoDocentes> ActualizarDocente([FromBody] PutDocenteComando putDocenteComando)
        {
            return await _mediator.Send(putDocenteComando);
        }
    }
}
