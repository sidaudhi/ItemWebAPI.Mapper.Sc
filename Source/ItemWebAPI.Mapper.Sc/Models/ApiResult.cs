using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemWebAPI.Mapper.Sc.Models
{
    internal class ApiResult
    {
        public int StatusCode { get; set; }

        public Result Result { get; set; }
    }

    internal class Result
    {
        public int TotalCount { get; set; }

        public int ResultCount { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
