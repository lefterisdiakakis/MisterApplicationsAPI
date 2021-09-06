using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public record IOType : AbstractEntity
    {

    }

    public enum IoTypes
    {
        [Description("Incoming")]
        Incoming = 1,
        [Description("Outgoing")]
        Outgoing = 2
    }
}
