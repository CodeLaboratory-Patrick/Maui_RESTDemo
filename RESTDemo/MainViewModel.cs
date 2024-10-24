using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RESTDemo
{
    public class MainViewModel
    {
        HttpClient client;
        JsonSerializerOptions _serializerOptions;
        string baseUrl = "https://6719f572acf9aa94f6a8838e.mockapi.io/";

        public MainViewModel()
        {
            client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
        }
        public ICommand GetAllUsersCommand =>
            new Command(async () =>
            {
                var url = $"{baseUrl}/users";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    //var content = await  response.Content.ReadAsStreamAsync();
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer.DeserializeAsync<List<User>>(responseStream, _serializerOptions);
                    }
                }
            });
        public ICommand GetSingleUserCommand =>
            new Command(async () =>
            {
                var url = $"{baseUrl}/users/25";
                var response = await client.GetStringAsync(url);
            });
    }
}
