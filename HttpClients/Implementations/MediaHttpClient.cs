using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class MediaHttpClient : IMediaService
{
    private readonly HttpClient _client;

    public MediaHttpClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task CreateAsync(MediaCreationDto dto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/medias", dto);
        if (!response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            throw new Exception(result);
        }
    }

    public async Task<ICollection<Media>> GetAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/medias");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Media> medias = JsonConvert.DeserializeObject<ICollection<Media>>(content,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            })!;
        return medias;
    }

    public async Task UpdateAsync(MediaUpdateDto dto)
    {
        string dtoAsJson = JsonConvert.SerializeObject(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application.json");
        HttpResponseMessage response = await _client.PatchAsJsonAsync("/medias", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"/medias/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<Media> GetById(int id)
    {
        HttpResponseMessage response = await _client.GetAsync($"/medias/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Media media = JsonConvert.DeserializeObject<Media>(content, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        })!;
        return media;
    }
}