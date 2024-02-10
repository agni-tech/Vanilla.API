using AutoMapper;
using Vanilla.Domain.Options.Dtos;
using Vanilla.Domain.Options.Entities;
using Vanilla.Domain.Options.Interfaces;

namespace Vanilla.Domain.Options.Services;

public class OptionService : IOptionService
{
    private readonly IOptionRepository _repository;
    private readonly IMapper _mapper;
    public OptionService(IOptionRepository repository, IMapper mapper)
    => (_repository, _mapper) = (repository, mapper);

    public async Task<OptionResponseDto> AddAsync(OptionDto dto)
    {
        var result = _mapper.Map<Option>(dto);

        if (result.Validate())
            return _mapper.Map<OptionResponseDto>(await _repository.AddAsync(result));

        return _mapper.Map<OptionResponseDto>(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null) return false;
        _repository.Remove(result);
        return true;
    }

    public async Task<List<OptionResponseDto>> GetAsync()
        => _mapper.Map<List<OptionResponseDto>>(await _repository.ToListAsync());

    public async Task<OptionResponseDto> GetAsync(int id)
        => _mapper.Map<OptionResponseDto>(await _repository.GetByIdAsync(id));

    public async Task<OptionResponseDto> UpdateAsync(int id, OptionDto dto)
    {
        var entity = _mapper.Map<Option>(await _repository.GetByIdAsync(id));

        entity.Config = dto.Name;
        entity.Value = dto.Description;

        if (entity.Validate())
            _repository.Update(entity);

        return _mapper.Map<OptionResponseDto>(entity);
    }
}
