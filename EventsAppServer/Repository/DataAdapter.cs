namespace EventsAppServer.Adapters
{
    using System.Collections.Generic;
    using EventsAppServer.Attributes;

    public abstract class DataAdapter<T>(string key)
        where T : class
    {
        private string key = key;

        public abstract void Clear();

        public abstract void Add(T item);

        public abstract T Get(Identifier id);

        public abstract List<T> GetAll();

        public abstract void Update(Identifier id, T item);

        public abstract void Delete(Identifier id);

        public abstract bool Contains(Identifier id);
    }
}
