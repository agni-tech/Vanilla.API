using AutoMapper;
using Vanilla.Domain.UserGroups.Dtos;
using Vanilla.Domain.UserGroups.Entities;
using Vanilla.Domain.UserGroups.Interfaces;
using Vanilla.Shared.Helpers;
using MySaviors.Helpers.Extensions;

namespace Vanilla.Domain.UserGroups.Services;

public class UserGroupService : IUserGroupService
{
    private readonly IUserGroupRepository _repository;
    private readonly IMapper _mapper;

    public UserGroupService(IUserGroupRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<UserGroupResponseDto>> GetAsync() =>
        _mapper.Map<List<UserGroupResponseDto>>(await _repository.ToListAsync());

    public async Task<UserGroupResponseDto> GetAsync(int id) =>
        _mapper.Map<UserGroupResponseDto>(await _repository.GetByIdAsync(id));

    public async Task<UserGroupResponseDto> AddAsync(UserGroupDto dto)
    {
        var result = _mapper.Map<UserGroup>(dto);

        if (await Validate(result))
            return _mapper.Map<UserGroupResponseDto>(await _repository.AddAsync(result));

        return _mapper.Map<UserGroupResponseDto>(result);
    }

    public async Task<UserGroupResponseDto> UpdateAsync(int id, UserGroupDto dto)
    {
        var result = _mapper.Map<UserGroup>(await _repository.GetByIdAsync(id));

        result.Name = dto.Name;
        result.Description = dto.Description;

        if (result.Validate())
            _repository.Update(result);

        return _mapper.Map<UserGroupResponseDto>(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        if (result == null) return false;

        _repository.Remove(result);
        return true;
    }

    private async Task<bool> Validate(UserGroup userGroup)
    {
        if (!userGroup.Validate())
            return false;

        var repo = await _repository.GetAsyncAsNoTracking(q => q.Name == userGroup.Name);
        if (repo.HaveAny())
        {
            NotificationsWrapper.AddMessage("NAME_ALREADY_EXISTS");
            return false;
        }
        return true;
    }
}