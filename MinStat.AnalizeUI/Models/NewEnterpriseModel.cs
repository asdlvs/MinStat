using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MinStat.AnalizeUI.Models
{
    public class NewEnterpriseModel
    {
        public int FederalSubjectId { get; set; }
        public int FederalDistrictId { get; set; }
        [Required(ErrorMessage = "Необходимо указать")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Необходимо указать")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$")]
        public string Mail { get; set; }
    }
}