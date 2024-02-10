using AutoMapper;
using Vanilla.Domain.UserGroupFeatures.Dtos;
using Vanilla.Domain.UserGroupFeatures.Entities;
using Vanilla.Domain.UserGroupFeatures.Interfaces;
using Vanilla.Shared.Helpers;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Domain.UserGroupFeatures.Services;

public class UserGroupFeatureService : IUserGroupFeatureService
{
    private readonly IUserGroupFeatureRepository _repository;
    private readonly IMapper _mapper;

    public UserGroupFeatureService(IUserGroupFeatureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserGroupFeatureResponseDto>> GetAsync() =>
        _mapper.Map<List<UserGroupFeatureResponseDto>>(await _repository.ToListAsync());

    public async Task<UserGroupFeatureResponseDto> GetAsync(int id) =>
        _mapper.Map<UserGroupFeatureResponseDto>(await _repository.GetByIdAsync(id));

    public async Task<UserGroupFeatureResponseDto> AddAsync(UserGroupFeatureDto dto)
    {
        var userGroupFeature = _mapper.Map<UserGroupFeature>(dto);

        if (await Validate(userGroupFeature))
            return _mapper.Map<UserGroupFeatureResponseDto>(await _repository.AddAsync(userGroupFeature));

        return _mapper.Map<UserGroupFeatureResponseDto>(userGroupFeature);
    }

    public async Task<UserGroupFeatureResponseDto> UpdateAsync(int id, UserGroupFeatureDto dto)
    {
        var result = _mapper.Map<UserGroupFeature>(await _repository.GetByIdAsync(id));

        result.Name = dto.Name;
        result.Description = dto.Description;

        if (result.Validate())
            _repository.Update(result);

        return _mapper.Map<UserGroupFeatureResponseDto>(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null) return false;

        _repository.Remove(result);
        return true;
    }

    private async Task<bool> Validate(UserGroupFeature userGroupFeature)
    {
        if (!userGroupFeature.Validate())
            return false;

        var repo = await _repository.GetAsyncAsNoTracking(q => q.Name == userGroupFeature.Name);
        if (repo.HaveAny())
        {
            NotificationsWrapper.AddMessage("NAME_ALREADY_EXISTS");
            return false;
        }
        return true;
    }

}