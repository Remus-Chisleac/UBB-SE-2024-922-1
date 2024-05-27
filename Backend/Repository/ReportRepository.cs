using Backend.Repository.Interfaces;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.Repository
{
    public class ReportRepository : IReportRepository
    {
        private ReportEndpoint reportEndpoint;

        public ReportRepository(ReportEndpoint reportEndpoint) : base()
        {
            this.reportEndpoint = reportEndpoint;
        }

        public bool Add(Guid key, PostReport value)
        {
            reportEndpoint.CreatePostReport(value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return reportEndpoint.ReadAllPostReports().Exists(r => r.Id == key);
        }

        public PostReport? Get(Guid key)
        {
            return reportEndpoint.ReadAllPostReports().Find(r => r.Id == key);
        }

        public IEnumerable<PostReport> GetAll()
        {
            return reportEndpoint.ReadAllPostReports();
        }

        public bool Remove(Guid key)
        {
            reportEndpoint.DeletePostReport(key);
            return true;
        }

        public bool Update(Guid key, PostReport value)
        {
            reportEndpoint.UpdatePostReport(key, value);
            return true;
        }
    }
}