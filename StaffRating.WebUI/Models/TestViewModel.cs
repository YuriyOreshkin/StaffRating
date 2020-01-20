using StaffRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StaffRating.WebUI.Models
{
    public class TestViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Наименование")]
        [StringLength(400)]
        [Required]
        public string name { get; set; }

        [DisplayName("Наименование")]
        public bool once { get; set; }

        [DisplayName("Продолжительность(мин)")]
        [Required]
        public int duration { get; set; }

        public long categoryid { get; set; }

        public TEST ToEntity(TEST test)
        {
            test.ID = this.id;
            test.NAME = this.name;
            test.ONCE = this.once;
            test.DURATION = this.duration;
            test.CATEGORYID = this.categoryid;

            return test;
        }
    }
}