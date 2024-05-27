using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Model;

namespace Moderation.Repository
{
    public class TextPostRepository : ITextPostRepository
    {
        private TextPostEndpoints textPostEndpoints;

        public TextPostRepository(TextPostEndpoints textPostEndpoints) : base()
        {
            this.textPostEndpoints = textPostEndpoints;
        }

        public bool Add(Guid key, TextPost value)
        {
            textPostEndpoints.CreateTextPost(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return textPostEndpoints.ReadAllTextPosts().Exists(a => a.Id == key);
        }

        public TextPost? Get(Guid key)
        {
            return textPostEndpoints.ReadAllTextPosts().Find(a => a.Id == key);
        }

        public IEnumerable<TextPost> GetAll()
        {
            return textPostEndpoints.ReadAllTextPosts();
        }

        public bool Remove(Guid key)
        {
            textPostEndpoints.DeleteTextPost(key);
            return true;
        }

        public bool Update(Guid key, TextPost value)
        {
            textPostEndpoints.UpdateTextPost(value);
            return true;
        }
    }
}
