using TimeTracker.WebAPi.Models;

namespace TimeTracker.WebAPi.Services.Data;

public interface IDataService<T>
{
  Task<IEnumerable<T>> GetItems(Pagination pagination);
  Task<T> GetItem(int id);
  Task<T> InsertItem(T item);
  Task<T> UpdateItem(int id, T updatedItem);
  Task DeleteItem(int id);
}
