using System.IdentityModel.Selectors;
using System.ServiceModel;
using MinStat.Enterprises.DAL;

namespace MinStat.Enterprises.BLL
{
    public class UserValidator
    {
        private IIdentifier _identifier;

        public UserValidator(IIdentifier identifier)
        {
            _identifier = identifier;
        }

        public UserValidator()
            :this(new EnterpriseIdentifier())
        {
        }

        public bool Validate(string username, string password)
        {
            return _identifier.Validate(username, password);
        }
    }
}
