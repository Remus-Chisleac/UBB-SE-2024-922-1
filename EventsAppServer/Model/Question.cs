namespace EventsAppServer.Entities
{
    public abstract class Question(string text)
        : IHasID
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Text { get; } = text;
    }
}