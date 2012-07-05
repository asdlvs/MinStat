using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Bonch.Security.Abstract
{
    public interface IValidate
    {
        bool Validate(string username, string password);
    }
}
