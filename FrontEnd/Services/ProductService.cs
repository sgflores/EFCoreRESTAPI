using FrontEnd.Services.Interfaces;
using FrontEnd.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class ProductService : BaseService, IProductService
    {
        // register service as addScoped @Startup.cs
        // scoped means
        public ProductService(TokenAuthenticationStateProvider tokenAuthenticationStateProvider)
         : base(tokenAuthenticationStateProvider)
        {

        }

        public async Task<List<ProductsViewModel>> GetListAsync()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await GetDataAsync("Products", param)
                .ConfigureAwait(false);

            List<ProductsViewModel> products = new List<ProductsViewModel>();

            
            if (!string.IsNullOrEmpty(response))
            {
                products = JsonConvert.DeserializeObject<List<ProductsViewModel>>(response);
            }

            return products;
        }

        public Task<List<ProductsViewModel>> GetListAsync(Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductsViewModel> GetSingleAsync(int Id)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await GetDataAsync($"Products/{Id}", param)
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(response))
            {
                ProductsViewModel product = JsonConvert.DeserializeObject<ProductsViewModel>(response);
                return SanitizeViewModel(product);
            }
            // not found
            throw new NotImplementedException();
        }

        public async Task<bool> Add(ProductsViewModel t)
        {
            var json = JsonConvert.SerializeObject(t);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostDataAsync($"Products", content)
                .ConfigureAwait(false);

            return true;
        }

        public async Task<bool> Update(int Id, ProductsViewModel t)
        {
            var json = JsonConvert.SerializeObject(t);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PutDataAsync($"Products/{Id}", content)
                .ConfigureAwait(false);

            return true;

        }

        public async Task<bool> Delete(int Id)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();

            var response = await DeleteDataAsync($"Products/{Id}", param)
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(response))
            {
                return true;
            }
            // not found
            throw new NotImplementedException();
        }

        // string empty null values
        public ProductsViewModel SanitizeViewModel(ProductsViewModel t)
        {
            ProductsViewModel model = new ProductsViewModel()
            {
                Id = t.Id,
                CategoryId = t.CategoryId,
                // StrCategoryId = t.CategoryId != null ? t.CategoryId.ToString() : string.Empty,
                Category = t.Category,
                Name = t.Name ?? string.Empty,
                Color =  t.Color ?? string.Empty,
                UnitPrice = t.UnitPrice,
                AvailableQuantity = t.AvailableQuantity
            };
            return model;
        }

    }
}
