using Vanilla.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla.Domain.Options.Dtos
{
    public class OptionResponseDto : BaseResponseDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
