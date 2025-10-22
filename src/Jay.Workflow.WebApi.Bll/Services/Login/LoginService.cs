using Jay.Workflow.WebApi.Bll.Interfaces.Login;
using Jay.Workflow.WebApi.Common.Exceptions;
using Jay.Workflow.WebApi.Dal.Interfaces.User;
using Jay.Workflow.WebApi.Model.Dtos.Request.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Services.Login
{
    public class LoginService:ILoginService
    {
        private readonly IUserDalService _userDalService;

        public LoginService(IUserDalService userDalService)
        {
            _userDalService = userDalService;
        }

        public async Task Login(LoginReq req)
        {
            var user=await _userDalService.GetUserByUserPhoneAsync(req.UserPhone).ConfigureAwait(false);

            if(user == null)
            {
                throw new NotFoundException($"用户{req.UserPhone}不存在！");
            }
        }
    }
}
