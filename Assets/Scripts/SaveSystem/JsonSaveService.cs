using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JsonSaveService : ISaveService
{
    public void Load<T>(string key, out T loadData, T defaultData)
    {
        string path = BuildPath(key);

        if (File.Exists(path))
        {
            using (var fileStream = new StreamReader(path))
            {
                var json = fileStream.ReadToEnd();
                loadData = JsonConvert.DeserializeObject<T>(json);
            }
        }
        else
        {
            loadData = defaultData;
        }
    }

    public void Save(string key, object data)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        using (var fileStream = new StreamWriter(path))
        {
            fileStream.Write(json);
        }
    }
    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}
