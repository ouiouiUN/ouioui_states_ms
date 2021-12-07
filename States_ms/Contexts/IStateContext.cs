namespace States_ms.Contexts
{
    using MongoDB.Driver;
    using States_ms.Models;
    public interface IStateContext
    {
        IMongoCollection<State> States { get; }
    }
}