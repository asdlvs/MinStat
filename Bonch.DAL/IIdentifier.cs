namespace MinStat.Enterprises.DAL
{
    public interface IIdentifier
    {
        int EnterpriseId(string username);

        bool Validate(string username, string password);
    }
}
