using Vanilla.Domain.Options.Dtos;
using Vanilla.Domain.Options.Interfaces;
using Vanilla.Domain.UserGroups.Dtos;
using Vanilla.Domain.UserGroups.Interfaces;
using Vanilla.Shared.Resources;

namespace Vanilla.Shared.Helpers;

public static class TableServiceFactory
{
    public static (dynamic service, dynamic dto) GetInstance(string table)
    {
        switch (table)
        {
            case StringResource.UserGroupTableName:
                return (DependencyInjectionHandler.GetInstance<IUserGroupService>(), new UserGroupDto());

            case StringResource.BuildingTableName:
                return (DependencyInjectionHandler.GetInstance<IOptionService>(), new OptionDto());


            default:
                return (null, null);
        }

    }
}
