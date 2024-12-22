using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Mapper;

public interface IDTOMapper<T, TRequest, TResponse> where TResponse : BaseResponse
{
  T MapFromRequest(TRequest request);
  TResponse MapToResponse(T source);
}
