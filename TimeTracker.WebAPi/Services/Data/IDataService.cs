using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Data;

public interface IDataService<T>
{
  Task<IEnumerable<T>> GetItemsAsync(Pagination pagination);
  Task<T> GetItemAsync(int id);
  Task<T> InsertItemAsync(T item);
  Task<T> UpdateItemAsync(int id, T updatedItem);
  Task DeleteItemAsync(int id);
}
