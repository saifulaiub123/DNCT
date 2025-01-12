
namespace Dnct.Application.Features.RunTimeParametersMaster.Query.GetAll
{
    public class GetAllRunTimeParametersMasterResponse
    {
        public int? RuntimeParametersMasterId { get; set; }
        public string ParameterKey { get; set; }
        public string ParameterValue { get; set; }
        public int? TableConfigId { get; set; }
    }
}
