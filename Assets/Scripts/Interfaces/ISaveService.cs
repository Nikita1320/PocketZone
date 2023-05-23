public interface ISaveService
{
    public void Save(string key, object data);
    public void Load<T>(string key, out T data, T defaultData);
}
