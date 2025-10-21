using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Base
{
    /// <summary>
    /// 业务模块控制器基类
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiExplorerSettings(GroupName ="BusinessServcie")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class BusinessController:ControllerBase
    {

    }
}
