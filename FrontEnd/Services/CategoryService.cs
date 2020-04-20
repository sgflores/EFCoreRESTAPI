using FrontEnd.Services.Interfaces;
using FrontEnd.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public async Task<bool> Add(CategoryViewModel t)
        {
            t = SanitizeViewModel(t);
            var json = JsonConvert.SerializeObject(t);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostDataAsync("Categories", content)
                .ConfigureAwait(false);

            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await DeleteDataAsync($"Categories/{Id}", param)
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(response))
            {
                return true;
            }
            // not found
            throw new NotImplementedException();
        }

        public async Task<List<CategoryViewModel>> GetListAsync()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await GetDataAsync("Categories", param)
                .ConfigureAwait(false);

            List<CategoryViewModel> categories = new List<CategoryViewModel>();

            if (!string.IsNullOrEmpty(response))
            {
                categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response);
            }

            return categories;
        }

        public async Task<List<CategoryViewModel>> GetListAsync(Dictionary<string, string> parameters)
        {

            var response = await GetDataAsync("Categories", parameters)
                .ConfigureAwait(false);

            List<CategoryViewModel> categories = new List<CategoryViewModel>();

            if (!string.IsNullOrEmpty(response))
            {
                categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(response);
            }

            return categories;
        }

        public async Task<CategoryViewModel> GetSingleAsync(int Id)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await GetDataAsync($"Categories/{Id}", param)
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(response))
            {
                CategoryViewModel category = JsonConvert.DeserializeObject<CategoryViewModel>(response);
                return SanitizeViewModel(category);
            }
            // not found
            throw new NotImplementedException();
        }

        public CategoryViewModel SanitizeViewModel(CategoryViewModel t)
        {
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = t.Id,
                Name = t.Name ?? string.Empty,
                Status = t.StrStatus == "1" ? true : false, // saved to backend model
                StrStatus = t.Status ? "1" : "0" // display to fronted model
            };
            return model;
        }

        public async Task<bool> Update(int Id, CategoryViewModel t)
        {
            t = SanitizeViewModel(t);
            var json = JsonConvert.SerializeObject(t);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PutDataAsync($"Categories/{Id}", content)
                .ConfigureAwait(false);

            return true;
        }
    }
}
