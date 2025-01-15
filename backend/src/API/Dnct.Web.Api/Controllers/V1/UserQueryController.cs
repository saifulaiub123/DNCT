using Asp.Versioning;
using Dnct.Application.Features.Table.Commands.Create;
using Dnct.Application.Features.Table.Commands.DDLProcess;
using Dnct.Application.Features.Table.Commands.DeleteUserQueryCommand;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.WebFramework.BaseController;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/user-query")]

    [Authorize]
    public class UserQueryController(ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetUserQueryQuery();
            var command = await sender.Send(query);

            return base.OperationResult(command);
        }
        [HttpGet("autoPopulate")]
        public async Task<JsonResult> AutoPopulate([FromQuery] int tableConfigId, [FromQuery] int queryId)
        {
            var records = new List<SqlRecord>
            {
                new SqlRecord
                {
                    ColunmId = 1,
                    SqlTxt = "SELECT * FROM Table1",
                    Att1 = "Attribute1_Value1",
                    Att2 = "Attribute2_Value1"
                },
                new SqlRecord
                {
                    ColunmId = 2,
                    SqlTxt = "SELECT * FROM Table2 WHERE Column = 'Value'",
                    Att1 = "Attribute1_Value2",
                    Att2 = "Attribute2_Value2"
                },
                new SqlRecord
                {
                    ColunmId = 3,
                    SqlTxt = "INSERT INTO Table3 (Column1, Column2) VALUES ('Value1', 'Value2')",
                    Att1 = "Attribute1_Value3",
                    Att2 = "Attribute2_Value3"
                }
            };

            return new JsonResult(records);
        }
        [HttpGet("validateQuery")]
        public async Task<JsonResult> ValidateQuery()
        {
            var records = new List<SqlRecord>
            {
                new SqlRecord
                {
                    ColunmId = 1,
                    SqlTxt = "SELECT * FROM Table1",
                    Att1 = "Attribute1_Value1",
                    Att2 = "Attribute2_Value1"
                },
                new SqlRecord
                {
                    ColunmId = 2,
                    SqlTxt = "SELECT * FROM Table2 WHERE Column = 'Value'",
                    Att1 = "Attribute1_Value2",
                    Att2 = "Attribute2_Value2"
                },
                new SqlRecord
                {
                    ColunmId = 3,
                    SqlTxt = "INSERT INTO Table3 (Column1, Column2) VALUES ('Value1', 'Value2')",
                    Att1 = "Attribute1_Value3",
                    Att2 = "Attribute2_Value3"
                }
            };

            return new JsonResult(records);
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate([FromBody] CreateOrUpdateUserQueryCommand command)
        {
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int userQueryId, [FromQuery] int tableConfigId)
        {
            var command = new DeleteUserQueryCommand
            {
                UserQueryId = userQueryId,
                TableConfigId = tableConfigId
            };
            var result = await sender.Send(command);

            return base.OperationResult(result);
        }
    }

    public class SqlRecord
    {
        public int ColunmId { get; set; }
        public string SqlTxt { get; set; }
        public string Att1 { get; set; }
        public string Att2 { get; set; }
    }
}