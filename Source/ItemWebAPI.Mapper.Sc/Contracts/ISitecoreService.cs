using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemWebAPI.Mapper.Sc.Contracts
{
    /// <summary>
    /// Contract for Sitecore Repository
    /// </summary>
    public interface ISitecoreService
    {
        IEnumerable<T> GetItems<T>(string query) where T : new();

        T GetItem<T>(string id) where T : new();

        IEnumerable<T> GetItems<T>(string[] ids) where T : new();
    }
}
