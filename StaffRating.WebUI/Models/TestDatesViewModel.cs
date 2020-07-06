using StaffRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StaffRating.WebUI.Models
{
    public class TestDatesViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Начало")]
        [Required]
        public DateTime begin { get; set; }

        [DisplayName("Окончание")]
        [Required]
        public DateTime end { get; set; }

        public long  testid { get; set; }

        public TESTDATES ToEntity(TESTDATES testdates)
        {
            testdates.ID = this.id;
            testdates.BEGIN = this.begin;
            testdates.END = this.end;
            testdates.TESTID = this.testid;

            return testdates;
        }
    }
}