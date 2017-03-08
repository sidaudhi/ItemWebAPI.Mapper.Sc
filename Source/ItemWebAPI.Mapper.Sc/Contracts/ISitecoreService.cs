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
        IEnumerable<T> GetItems<T>(string query, string language = null) where T : new();

        T GetItem<T>(string id, string language = null) where T : new();

        IEnumerable<T> GetItems<T>(string[] ids, string language = null) where T : new();
    }
}
