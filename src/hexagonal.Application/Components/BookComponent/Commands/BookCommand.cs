using AutoMapper;
using hexagonal.Application.Bases;
using hexagonal.Application.Bases.Interfaces;
using hexagonal.Application.Components.BookComponent.Contracts;
using hexagonal.Application.Components.BookComponent.Core;
using hexagonal.Application.Components.BookComponent.Validations;
using hexagonal.Domain;

namespace hexagonal.Application.Components.BookComponent.Commands;

public class BookCommand : IBookCommand
{
    private readonly IUcBookCreate _createBook;
    private readonly IUcBookDelete _deleteBook;
    private readonly IUcBookEdit _editBook;
    private readonly IMapper _mapper;

    public BookCommand(
        IUcBookDelete deleteBook,
        IUcBookCreate createBook, IUcBookEdit editBook, IMapper mapper)
    {
        _deleteBook = deleteBook;
        _createBook = createBook;
        _editBook = editBook;
        _mapper = mapper;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(BookCreateDto dto)
    {
        var validator = new BookCreateValidation();
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new SingleResultDto<EntityDto>(validationResult);
        }

        var mappedObject = _mapper.Map<Book>(dto);
        var result = await _createBook.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(BookEditDto dto)
    {
        var validator = new BookEditValidation();
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            return new SingleResultDto<EntityDto>(validationResult);
        }

        var mappedObject = _mapper.Map<Book>(dto);
        var result = await _editBook.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(int id)
    {
        var result = await _deleteBook.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}