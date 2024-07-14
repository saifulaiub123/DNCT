
using Dnct.Application.Profiles;

namespace Dnct.Application.Features.Server.Query.GetServerInfo
{
    public class GetAllServerResponse//: ICreateMapper<ConnectionsMaster>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; } = true;
        public string NodeType { get; set; } = "Server";
    }
}
