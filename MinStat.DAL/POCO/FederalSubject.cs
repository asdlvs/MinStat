using System;

namespace MinStat.DAL.POCO
{
    [Serializable]
    public class FederalSubject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int FederalDistrictId { get; set; }
    }
}
