using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain;
using RestWithASPNET.DTO.BookDto;
using RestWithASPNET.DTO.PersonDto;
using RestWithASPNET.Repository.Data;
using RestWithASPNET.Repository.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Services.Repository
{
    public class BookRepository : IBookService
    {

        private readonly Context _context;
        private readonly IGenericService _genericService;
        private readonly IMapper _mapper;

        public BookRepository(Context context, IMapper mapper, IGenericService genericService)
        {
            _context = context;
            _mapper = mapper;
            _genericService = genericService;
        }

        public async Task<ReadBookDto> Create(CreateBookDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                _context.Add(book);
                await _context.SaveChangesAsync();

                return _mapper.Map<ReadBookDto>(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadBookDto> FindById(long id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

                return _mapper.Map<ReadBookDto>(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadBookDto>> FindAll()
        {
            try
            {
                var books = await _context.Books.ToListAsync();

                return _mapper.Map<List<ReadBookDto>>(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadBookDto> Update(UpdateBookDto bookDto, int id)
        {
            try
            {
                var model = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                    return null;

                var book = _mapper.Map(bookDto, model);

                _genericService.Update<Book>(book);
                await _genericService.SaveChangesAsync();

                return _mapper.Map<ReadBookDto>(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return false;

                _genericService.Delete<Book>(book);
                await _genericService.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
