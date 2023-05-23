interface IUsable<T>
{
    public string UseDescription { get; }
    public bool Use(T player);
}
