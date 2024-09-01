

namespace Dnct.Application.Features.TreeView.Query.GetTableInstanceByDatabaseSourceId
{
    public class GetTableInstanceByDatabaseSourceIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; } = true;
        public string NodeType { get; set; } = "TableInstance";

    }
}
