using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public record AbstractEntity
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleEL { get; set; }
        public string TitleDE { get; set; }
        public string TitleFR { get; set; }
    }
}
