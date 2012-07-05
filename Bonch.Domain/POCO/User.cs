namespace Bonch.Domain.POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;


    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        [ForeignKey("Enterprise")]
        public int EnterpriseId { get; set; }
        [InverseProperty("Users")]
        [XmlIgnore]
        public virtual Enterprise Enterprise { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public string Login
        {
            get
            {
                return this.Mail;
            }
            set
            {
                this.Mail = value;
            }

        }

        public bool IsDataFilled()
        {
            return !String.IsNullOrEmpty(FirstName)
                && !String.IsNullOrEmpty(LastName)
                && !String.IsNullOrEmpty(Phone);
        }
    }
}
