
namespace Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId
{
    public class GetDatabasesByServerIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NodeType { get; set; } = "Database";

    }
}
