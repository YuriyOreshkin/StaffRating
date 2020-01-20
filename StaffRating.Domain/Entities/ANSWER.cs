using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRating.Domain.Entities
{
    public class ANSWER
    {
        public long ID { get; set; }
        public string TEXT { get; set; }
        public bool GOOD { get; set; }

        public long QUESTIONID { get; set; }
        public virtual QUESTION QUESTION { get; set; }

    }
}
