using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRating.Domain.Entities
{
    public class TESTRATINGS
    {
        
        public long ID { get; set; }
        public short RATING { get; set; }
        public int FACT { get; set; }
        public long TESTID{ get; set; }

        public virtual TEST TEST { get; set; }
        
    }
}
