using System.Collections.Generic;
using Web.Users.Models;

namespace Web.Users.Services
{
    public interface IConfigurationService
    {
        IList<Purpose> GetPurposes();
    }
}