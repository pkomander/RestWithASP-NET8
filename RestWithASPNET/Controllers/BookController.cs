using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Domain;
using RestWithASPNET.DTO.BookDto;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Repository.Services.Interface;

namespace RestWithASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var retorno = await _bookService.FindById(id);

                if (retorno == null)
                {
                    return NotFound("O Book não foi encontrado!");
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
                return Ok(await _bookService.FindAll());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Post([FromBody] CreateBookDto bookDto)
        {
            try
            {
                if (bookDto == null)
                    return BadRequest();

                var retorno = await _bookService.Create(bookDto);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto bookDto, int id)
        {
            try
            {
                if (bookDto == null)
                    return BadRequest();

                var retorno = await _bookService.Update(bookDto, id);
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
                var retorno = await _bookService.Delete(id);

                if (retorno == false)
                    return BadRequest("O Book não foi encontrado!");

                return Ok("Book deletado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
