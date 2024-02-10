using Vanilla.Domain.Options.Entities;
using Vanilla.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla.Domain.Options.Interfaces
{
    public interface IOptionRepository : IBaseRepository<int, Option>
    {
    }
}
