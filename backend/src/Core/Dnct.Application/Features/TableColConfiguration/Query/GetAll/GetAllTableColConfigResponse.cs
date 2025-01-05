
namespace Dnct.Application.Features.UserQuery.Query.GetAllTableColConfig
{
    public class GetAllTableColConfigResponse
    {
        public int TblColConfgrtnId { get; set; }
        public int TblConfgrtnId { get; set; }
        public string ColmnName { get; set; }
        public string DataType { get; set; }
        public string ColmnTrnsfrmtnStep1 { get; set; }
        public char? GenrtIdInd { get; set; }
        public short? IdGenrtnStratgyId { get; set; }
        public short? Type2StartInd { get; set; }
        public short? Type2EndInd { get; set; }
        public short? CurrRowInd { get; set; }
        public string Pattern1 { get; set; }
        public string Pattern2 { get; set; }
        public char? Pattern3 { get; set; }
        public short? LadInd { get; set; }
        public short? JoinDupsInd { get; set; }
        public DateTime? ConfgrtnEffStartTs { get; set; }
        public DateTime ConfgrtnEffEndTs { get; set; }
    }
}
