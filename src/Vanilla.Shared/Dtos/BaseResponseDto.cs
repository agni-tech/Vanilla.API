using FluentValidation.Results;

namespace Vanilla.Shared.Dtos;

public class BaseResponseDto<TId>
{
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
}
