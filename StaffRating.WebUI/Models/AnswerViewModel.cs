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
    public class AnswerViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Текст")]
        [StringLength(1000)]
        [Required]
        [UIHint("TextArea")]
        public string text { get; set; }

        [DisplayName("Верно?")]
        public bool good { get; set; }

        [DisplayName("№п/п")]
        public short ordernum { get; set; }

        public long questionid { get; set; }

        public ANSWER ToEntity(ANSWER answer)
        {
            answer.ID = this.id;
            answer.TEXT = this.text;
            answer.GOOD = this.good;
            answer.ORDERNUM = this.ordernum;
            answer.QUESTIONID = this.questionid;

            return answer;
        }

    }
}
