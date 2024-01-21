using Ecommerce.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.MobileApp.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseaddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public RestDataService()
        {
            _httpClient = new HttpClient();
            _baseaddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:7084" : "https://localhost:44356";
            _url = $"{_baseaddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }


        public Task AddTodoAsync(Todo item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTodoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            List<Todo> todos = new List<Todo>();

            if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet) 
            {
                Debug.WriteLine("--- No Internet Access ---");
                return todos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
                if(response.IsSuccessStatusCode)
                {
                    string content  = await response.Content.ReadAsStringAsync();
                    todos = JsonSerializer.Deserialize<List<Todo>>(content, _jsonSerializerOptions);
                }else
                {
                    Debug.WriteLine("--- Non Http 2xx Response ---");
                }
            }catch (Exception ex)
            {
                Debug.WriteLine($" Whoops Exception {ex.Message}");
            }

            return todos;
        }

        public Task UpdateTodo(Todo item)
        {
            throw new NotImplementedException();
        }
    }
}
