using System.Text.Json;
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
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer);
        File.WriteAllText(FilePath, serialized);
        _dataContainer = null;
    }
}