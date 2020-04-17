using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services.Interfaces
{
    public interface ICommonService<T>
    {
        public Task<List<T>> GetListAsync();
        public Task<List<T>> GetListAsync(Dictionary<string, string> parameters);
        public Task<T> GetSingleAsync(int Id);
        public Task<bool> Update(int Id, T t);
        public T SanitizeViewModel(T t);

    }
}
