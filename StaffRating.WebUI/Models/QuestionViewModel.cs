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
        public string text { get; set; }

        [DisplayName("Сложность")]
        [Required]
        public int rating { get; set; }

        public long testid{ get; set; }
    }
}
