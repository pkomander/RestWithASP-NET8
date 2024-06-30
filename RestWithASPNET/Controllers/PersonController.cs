using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Domain;
using RestWithASPNET.DTO.PersonDto;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Repository.Services.Interface;

namespace RestWithASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var retorno = await _personService.FindById(id);
                
                if (retorno == null)
                {
                    return NotFound("O Person não foi encontrado!");
                }

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _personService.FindAll());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Post([FromBody] CreatePersonDto personDto)
        {
            try
            {
                if (personDto == null)
                    return BadRequest("Nenhum item foi passado na requisição!");

                var retorno = await _personService.Create(personDto);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Update([FromBody] UpdatePersonDto personDto, int id)
        {
            try
            {
                if (personDto == null)
                    return BadRequest("Nenhum item foi passado na requisição!");

                var retorno = await _personService.Update(personDto, id);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var retorno = await _personService.Delete(id);

                if (retorno == false)
                    return BadRequest("O Person não foi encontrado!");

                return Ok("Person deletado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
