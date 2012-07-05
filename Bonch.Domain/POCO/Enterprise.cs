using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Bonch.Domain.POCO
{
    [Serializable]
    public class Enterprise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FederationSubject FederationSubject { get; set; }
        [InverseProperty("Enterprise")]
        public virtual ICollection<User> Users { get; set; }
        [InverseProperty("Enterprise")]
        public virtual ICollection<Summary> Summaries { get; set; }

    }
}
