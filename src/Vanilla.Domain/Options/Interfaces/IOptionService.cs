using Vanilla.Domain.Options.Dtos;
using Vanilla.Shared.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla.Domain.Options.Interfaces
{
    public interface IOptionService : IBaseService<int, OptionDto, OptionResponseDto>
    {
    }
}
