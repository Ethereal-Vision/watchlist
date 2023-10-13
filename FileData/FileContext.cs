using Newtonsoft.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<Media>? Medias
    {
        get
        {
            LazyDataLoad();
            return _dataContainer?.Medias;
        }
    }

    private void LazyDataLoad()
    {
        if (_dataContainer == null)
        {
            LoadData();
        }
    }

    public void LoadData()
    {
        if (_dataContainer != null) return;

        if (!File.Exists(FilePath))
        {
            _dataContainer = new()
            {
                Medias = new List<Media>(),
            };
            return;
        }
        string content = File.ReadAllText(FilePath);
        _dataContainer = JsonConvert.DeserializeObject<DataContainer>(content, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
    }

    public void SaveChanges()
    {
        string serialized = JsonConvert.SerializeObject(_dataContainer, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        });
        File.WriteAllText(FilePath, serialized);
        _dataContainer = null;
    }
}