using EventsAppServer.Entities;

namespace EventsAppServer.Endpoints
{
    public class ReportEndpoint
    {
        private readonly AppContext _context;

        public ReportEndpoint(AppContext context)
        {
            _context = context;
        }

        public void CreateReport(PostReport report)
        {
            _context.Add(report);
            _context.SaveChanges();
        }

        public List<PostReport> ReadReports()
        {
            return _context.PostReports.ToList();
        }

        public void UpdateReport(PostReport newReport)
        {
            IEnumerable<PostReport> items = 
                from report in _context.PostReports
                where report.Id == newReport.Id
                select report;

            PostReport? item = items.FirstOrDefault();
            item.UserId = newReport.UserId;
            item.PostId = newReport.PostId;
            item.Message = newReport.Message;
            item.GroupId = newReport.GroupId;
            _context.SaveChanges();
        }

        public void DeleteReport(Guid id)
        {
            IEnumerable<PostReport> items =
                from report in _context.PostReports
                where report.Id == id
                select report;

            PostReport? item = items.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}
