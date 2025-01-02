
namespace Dnct.Application.Features.UserQuery.Query.GetUserQuery
{
    public class GetUserQueryResponse
    {
        public int? UserQueryId { get; set; }
        public int? TableConfigId { get; set; }
        public string UserQuery { get; set; }
        public int? BaseQueryIndicator { get; set; }
        public int? QueryOrderIndicator { get; set; }
        public DateTime? RowInsertTimestamp { get; set; }
    }
}
