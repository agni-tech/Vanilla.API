using Vanilla.Domain.Options.Entities;
using Vanilla.Domain.Options.Interfaces;
using Vanilla.Infra.Data.Contexts;
using Vanilla.Shared.Repository;


namespace Vanilla.Infra.Data.Repositories
{
    public class BuildingRepository : BaseRepository<int, Option>, IOptionRepository
    {
        public BuildingRepository(VanillaContext context) : base(context)
        {
        }
    }
}
