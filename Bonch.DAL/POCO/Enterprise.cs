
using System;
namespace MinStat.Enterprises.DAL.POCO
{

    [Serializable]
    public class Enterprise
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int FederalSubjectId { get; set; }
    }
}
