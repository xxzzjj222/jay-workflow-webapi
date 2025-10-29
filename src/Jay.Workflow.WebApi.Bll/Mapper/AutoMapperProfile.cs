using AutoMapper;
using Jay.Workflow.WebApi.Model.Dtos.Request.User;
using Jay.Workflow.WebApi.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Bll.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            #region 用户
            CreateMap<User, GetUsersPageResp>();
            #endregion 用户
        }
    }
}
