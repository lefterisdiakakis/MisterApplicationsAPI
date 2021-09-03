using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public record ApplicationUser
    {
            public int ID { get; set; } //(int, not null)
            public DateTime InsertDateTime { get; set; } //(datetime, not null)
            public int InsertUserID { get; set; } //(int, not null)
            public DateTime UpdateDateTime { get; set; } //(datetime, not null)
            public int UpdateUserID { get; set; } //(int, not null)
            public string Username { get; set; } //(nvarchar(256), not null)
            public string Password { get; set; } //(nvarchar(256), not null)
            public string LastName { get; set; } //(nvarchar(256), not null)
            public string FirstName { get; set; } //(nvarchar(256), not null)
            public string Email { get; set; } //(nvarchar(1024), null)
            public string IPRestriction { get; set; } //(nvarchar(39), null)
            public int LanguageID { get; set; } //(int, not null)
            public int UserTypeID { get; set; } //(int, not null)
            public bool Active { get; set; } //(bit, not null)
            public bool Visible { get; set; } //(bit, not null)
            public bool Deleted { get; set; } //(bit, not null)
            public int ResourceID { get; set; } //(int, not null)
            public string Description { get; set; } //(nvarchar(512), null)
    }
}
