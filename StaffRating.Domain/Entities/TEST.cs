using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRating.Domain.Entities
{
    public class TEST
    {
        public TEST()
        {
            this.QUESTIONS = new HashSet<QUESTION>();
            this.TESTSDATES = new HashSet<TESTDATES>();
        }
        public long ID { get; set; }
        public string NAME { get; set; }
        public bool ONCE { get; set; }
        public int DURATION { get; set; }
        public long CATEGORYID { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }
        public virtual ICollection<QUESTION> QUESTIONS { get; set; }
        public virtual ICollection<TESTDATES> TESTSDATES { get; set; }

    }
}
