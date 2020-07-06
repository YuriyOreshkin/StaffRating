using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRating.Domain.Entities
{
    public class TESTDATES
    {
        public long ID { get; set; }
        public DateTime  BEGIN {get;set;}
        public DateTime END { get; set; }

        public long TESTID { get; set; }
        public virtual TEST TEST { get; set; }
    }
}
