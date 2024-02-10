using Vanilla.Shared.Dtos;
using System.Net;

namespace Vanilla.Shared.Service;

public interface IRedemetService
{
    public Task<(AerodromoRedemetDto response, HttpStatusCode httpStatusCode)> GetAsync(string local, DateTime date);
}
