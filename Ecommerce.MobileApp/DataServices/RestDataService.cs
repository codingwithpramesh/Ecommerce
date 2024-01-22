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


        public async Task AddTodoAsync(Todo item)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("--- No Internet Access ---");
                return;
            }

            try
            {
                string JsonTodo = JsonSerializer.Serialize<Todo>(item, _jsonSerializerOptions);
                StringContent content = new StringContent(JsonTodo,Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully Created Todo");
                }else
                {
                    Debug.WriteLine("Non Http 2xx Response");
                }

            }catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            return;
        }

        public async Task DeleteTodoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("--- No Internet Access ---");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully Deleted Todo");
                }
                else
                {
                    Debug.WriteLine("Non Http 2xx Response");
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Exception {ex.Message}");
            }


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

        public async Task UpdateTodo(Todo item)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("--- No Internet Access ---");
                return;
            }

            try
            {
                string JsonTodo = JsonSerializer.Serialize<Todo>(item, _jsonSerializerOptions);
                StringContent content = new StringContent(JsonTodo, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{item.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully Created Todo");
                }
                else
                {
                    Debug.WriteLine("Non Http 2xx Response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            return;
        }
    }
}
