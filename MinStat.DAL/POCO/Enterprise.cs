using System;

namespace MinStat.DAL.POCO
{
    [Serializable]
    public class Enterprise
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FederalSubjectId { get; set; }
    }
}
