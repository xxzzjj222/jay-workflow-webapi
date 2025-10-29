using Jay.Workflow.WebApi.Common.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.Workflow.WebApi.Model.Dtos.Business
{
    public class MigrateVersionInfoDto : BusinessDto
    {
        public string Version { get; set; }

        public int VersionNum { get; set; }

        public string VersionFilePath { get; set; }
    }
}
