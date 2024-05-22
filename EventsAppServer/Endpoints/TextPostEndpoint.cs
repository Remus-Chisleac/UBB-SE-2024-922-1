using EventsAppServer.Entities;

namespace EventsAppServer.DbEndpoints
{
    public class TextPostEndpoint
    {
        private readonly AppContext _context;

        public TextPostEndpoint(AppContext context)
        {
            _context = context;
        }

        public void CreateTextPost(TextPost textPost)
        {
            _context.Add(textPost);
            _context.SaveChanges();
        }

        public List<TextPost> ReadTextPosts()
        {
            return _context.TextPosts.ToList();
        }

        public void UpdateTextPost(TextPost newTextPost)
        {
            IEnumerable<TextPost> items = 
                from textPost in _context.TextPosts
                where textPost.Id == newTextPost.Id
                select textPost;

            TextPost? item = items.FirstOrDefault();
            item.Content = newTextPost.Content;
            item.Author = newTextPost.Author;
            item.Score = newTextPost.Score;
            item.Status = newTextPost.Status;
            item.Awards = newTextPost.Awards;
            item.IsDeleted = newTextPost.IsDeleted;
            _context.SaveChanges();
        }

        public void DeleteTextPost(Guid id)
        {
            IEnumerable<TextPost> items =
                from textPost in _context.TextPosts
                where textPost.Id == id
                select textPost;

            TextPost? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}
