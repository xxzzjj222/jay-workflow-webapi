using Jay.NC.Workflow.WebApi.Model.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jay.NC.Workflow.WebApi.Model.Dtos.Business
{
    public class MigrateVersionInfoDto : BusinessDto
    {
        public string Version { get; set; }

        public int VersionNum { get; set; }

        public string VersionFilePath { get; set; }
    }
}
