using ItemWebAPI.Mapper.Sc.Models;
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
    public interface ISitecoreClient
    {
        Item GetById(string id, string language = null);

        IEnumerable<Item> GetByQuery(string query, string language = null);
    }
}
