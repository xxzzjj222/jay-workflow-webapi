using Jay.Workflow.WebApi.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Request.User
{
    public class CreateUserReq:RequestDto
    {
        public string UserName { get; set; } = string.Empty;

        public string UserPhone { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
