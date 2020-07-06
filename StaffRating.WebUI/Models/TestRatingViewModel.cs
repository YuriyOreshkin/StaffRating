using StaffRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StaffRating.WebUI.Models
{
    public class TestRatingViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Сложность")]
        [Required]
        public short rating { get; set; }

        [DisplayName("Фактическое")]
        [UIHint("Integer")]
        public int fact { get; set; }

        [DisplayName("Максимальное")]
        public int maximum { get; set; }
        public long testid { get; set; }

        public TESTRATINGS ToEntity(TESTRATINGS testratings)
        {
            testratings.ID = this.id;
            testratings.FACT = this.fact;            

            return testratings;
        }
    }
}