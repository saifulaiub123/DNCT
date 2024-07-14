
namespace Dnct.Application.Features.TreeView.Query.GetDatabasesByServerId
{
    public class GetDatabasesByServerIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; } = true;
        public string NodeType { get; set; } = "Database";

    }
}
