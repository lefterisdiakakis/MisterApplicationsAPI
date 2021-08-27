using Domain;
using Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Implementations
{
    public class ApplicationRepositoryDummy : IApplicationMenuRepository
    {
        public Task<List<ApplicationMenu>> FindAll(int UserId, int AppId, string Langunange)
        {
            List<ApplicationMenu> applicationMenus = new List<ApplicationMenu>();
            applicationMenus.Add(new ApplicationMenu
            {
                Id=1,
                Url=null,
                Title = "Call Browser",
                ParentMenuId = null,
                Enable = true
            });

            applicationMenus.Add(new ApplicationMenu
            {
                Id = 78,
                Url = "/Default.aspx",
                Title = "Default",
                ParentMenuId = 1,
                Enable=true
            });

            applicationMenus.Add(new ApplicationMenu
            {
                Id = 7,
                Url = "/Browser-Daily.aspx",
                Title = "Daily",
                ParentMenuId = 1,
                Enable = true
            });

            applicationMenus.Add(new ApplicationMenu
            {
                Id = 7,
                Url = "/Browser-Extended.aspx",
                Title = "Extended",
                ParentMenuId = 1,
                Enable = true
            });

            applicationMenus.Add(new ApplicationMenu
            {
                Id = 81,
                Url = null,
                Title = "Configuration",
                ParentMenuId = null,
                Enable = true
            });

            applicationMenus.Add(new ApplicationMenu
            {
                Id = 82,
                Url = "Configuration-RowsColors.aspx",
                Title = "RowsColors",
                ParentMenuId = 81,
                Enable = true
            });

            return  Task.FromResult(applicationMenus);
        }
    }
}
