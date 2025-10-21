using Microsoft.AspNetCore.Mvc;

namespace Jay.Workflow.WebApi.Service.Controllers.Base
{
    /// <summary>
    /// 工具模块控制器基类
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "UtilService")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UtilController : ControllerBase
    { }
}
