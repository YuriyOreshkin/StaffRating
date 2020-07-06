using StaffRating.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRating.WebUI.Models
{
    public class QuestionViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Текст")]
        [StringLength(1000)]
        [Required]
        [UIHint("TextArea")]
        public string text { get; set; }

        [DisplayName("Сложность")]
        [Required]
        [DefaultValue(1)]
        [UIHint("Rating")]
        public short rating { get; set; }

        [DisplayName("№п/п")]
        public short ordernum { get; set; }

        public long testid{ get; set; }

        public QUESTION ToEntity(QUESTION question)
        {
            question.ID = this.id;
            question.ORDERNUM = this.ordernum;
            question.TEXT = this.text;
            question.RATING = this.rating;
            question.TESTID = this.testid;

            return question;
        }
    }
}
