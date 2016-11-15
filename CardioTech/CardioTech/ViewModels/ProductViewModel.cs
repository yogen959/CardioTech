using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CardioTech.Models;

namespace CardioTech.ViewModels
{
    class ProductsViewModel
    {
        private const string Url = "http://cardiotech.com.do/dat/products/{0}/{1}?term={2}";
        public List<Products> GetResultS(string SearchString, int page = 1, int limit = 4)
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var json = client.GetStringAsync(string.Format(Url, new string[] { limit.ToString(), page.ToString(), SearchString }));
                return JsonConvert.DeserializeObject<List<Products>>(json.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<List<Products>> GetResult(string SearchString, int page = 1, int limit = 2)
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var json = await client.GetStringAsync(string.Format(Url, new string[] { limit.ToString(), page.ToString(), SearchString }));
                return JsonConvert.DeserializeObject<List<Products>>(json.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
