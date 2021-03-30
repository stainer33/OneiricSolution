using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
namespace Oneiric.CoreSetup.CoreSetupModels.Roles
{
  public  class RoleLists
    {
            public string FilterCount { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Action { get; set; }

  public class RoleUpdateInfo
        {
            public string UserName { get; set; }
            public string Id { get; set; }
            public string CurrentRole { get; set; }
            public List<SelectListItem> RoleList { get; set; }
            public string CreatedBy { get; set; }
        }
    }
}
