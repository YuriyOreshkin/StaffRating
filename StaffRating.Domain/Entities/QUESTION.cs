using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRating.Domain.Entities
{
    public class QUESTION
    {
        public QUESTION()
        {
            this.ANSWERS = new HashSet<ANSWER>();
        }
        public long ID { get; set; }
        public short ORDERNUM { get; set; }
        public string TEXT { get; set; }
        public short RATING { get; set; }
        public long TESTID{ get; set; }

        public virtual TEST TEST { get; set; }
        public virtual ICollection<ANSWER> ANSWERS { get; set; }
    }
}
