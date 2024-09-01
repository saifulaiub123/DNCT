
namespace Dnct.Application.Features.DatabaseSource.Query.GetDatabaseSourcesById
{
    public class GetDatabaseSourcesByIdResponse
    {
        public int DatabaseSourceId { get; set; }
        public string ConnectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}
