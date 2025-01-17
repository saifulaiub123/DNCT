using System.Runtime.InteropServices.JavaScript;
using Asp.Versioning;
using Dnct.Application.Features.TableColConfiguration.Query.GetAllTableColConfig;
using Dnct.Application.Features.UserQuery.Query.GetAllTableColConfig;
using Dnct.Application.Features.UserQuery.Query.GetUserQuery;
using Dnct.WebFramework.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Mediator;
using Dnct.Application.Features.TableColConfiguration.Commands.CreateMultiple;
using Dnct.Domain.Model;

namespace Dnct.Web.Api.Controllers.V1
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/table-col-config")]

    [Authorize]
    public class TableColConfigurationController(Mediator.ISender sender) : BaseController
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTableColConfigQuery query)
        {
            var result = await sender.Send(query);

            return base.OperationResult(result);
        }
        [HttpPost("createMulti")]
        public async Task<IActionResult> CreateMulti([FromBody] CreateMultipleTblColConfigCommand command)
        {
            //var command = new CreateMultipleTblColConfigCommand();
            ////command.Data = Data;
            var reuslt = await sender.Send(command);

            return Ok(reuslt);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int tbleColConfigId, [FromQuery] int tableConfigId)
        {
            var command = new GetAllTableColConfigQuery();
            await sender.Send(command);

            return Ok();
        }

        [HttpPost("validateSystax")]
        public async Task<IActionResult> ValidateSystax([FromBody] CreateMultipleTblColConfigCommand command)
        {
            var result = new List<object>();
            Random random = new Random();



            var list = new List<KeyValuePair<int, int>>();
            

            foreach (var item in command.Data)
            {
                list.Add(new KeyValuePair<int, int>(item.TblColConfgrtnId, random.Next(0, 2)));
                result.Add(new { TblColConfgrtnId = item.TblColConfgrtnId, Value = random.Next(0, 2) });
            }
            return Ok(list);
        }
    }
}
