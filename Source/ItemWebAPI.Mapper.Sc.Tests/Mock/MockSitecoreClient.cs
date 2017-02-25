using ItemWebAPI.Mapper.Sc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemWebAPI.Mapper.Sc.Models;

namespace ItemWebAPI.Mapper.Sc.Tests.Mock
{
    internal class MockSitecoreClient : ISitecoreClient
    {
        Dictionary<Guid, Item> _idRepository;
        Dictionary<string, IEnumerable<Item>> _queryRepository;

        public MockSitecoreClient()
        {
            _idRepository = new Dictionary<Guid, Item>()
            {
                {
                    new Guid("{DDD7284A-AF73-49CB-8AAD-2C6B928DEA99}"),
                    new Item()
                    {
                        Id = "{DDD7284A-AF73-49CB-8AAD-2C6B928DEA99}",
                        Fields = new Dictionary<string, Field>()
                        {
                            { Guid.NewGuid().ToString() , new Field() { Name = "Name", Value = "Apparel" } },
                            { Guid.NewGuid().ToString() , new Field() { Name = "Description", Value = "Lorem ipsum dolor sit amet, consectetur adipiscing elit" } }
                        }
                    }
                },
                {
                    new Guid("{82A3AA39-2462-4189-B496-833118A04719}"),
                    new Item()
                    {
                        Id = "{82A3AA39-2462-4189-B496-833118A04719}",
                        Fields = new Dictionary<string, Field>()
                        {
                            { Guid.NewGuid().ToString() , new Field() { Name = "Name", Value = "Scarves" } },
                            { Guid.NewGuid().ToString() , new Field() { Name = "Description", Value = "Lorem ipsum dolor sit amet, consectetur adipiscing elit" } }
                        }
                    }
                }
            };

            _queryRepository = new Dictionary<string, IEnumerable<Item>>()
            {
                {
                    "QueryForItems",
                    new List<Item>()
                    {
                        new Item() {
                            Id = Guid.NewGuid().ToString(),
                            Fields = new Dictionary<string, Field>()
                            {
                                { Guid.NewGuid().ToString(), new Field() { Name = "Name", Value="Black Scarf" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Description", Value="Lorem ipsum dolor sit amet, consectetur adipiscing elit" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "IsFeatured", Value="1" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Tags", Value="{DDD7284A-AF73-49CB-8AAD-2C6B928DEA99}|{82A3AA39-2462-4189-B496-833118A04719}" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "ProductLink", Value="<link text=\"Black Scarf\" linkType=\"external\" class=\"test\" title=\"Test Title\" target=\"_self\" url=\"http://google.com\" anchor=\"Test\" />" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Image", Value="<image mediaid=\""+Guid.NewGuid().ToString()+"\" src=\"~/media/2EB30763B2C24ACD8283E1A84EC9B225.ashx\"  />" } }
                            }
                        },
                        new Item() {
                            Id = Guid.NewGuid().ToString(),
                            Fields = new Dictionary<string, Field>()
                            {
                                { Guid.NewGuid().ToString(), new Field() { Name = "Name", Value="Men's Belt" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Description", Value="Lorem ipsum dolor sit amet, consectetur adipiscing elit" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "IsFeatured", Value="1" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Tags", Value="{DDD7284A-AF73-49CB-8AAD-2C6B928DEA99}" } }
                            }
                        },
                        new Item() {
                            Id = Guid.NewGuid().ToString(),
                            Fields = new Dictionary<string, Field>()
                            {
                                { Guid.NewGuid().ToString(), new Field() { Name = "Name", Value="Rolex Wrist Watch" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Description", Value="Lorem ipsum dolor sit amet, consectetur adipiscing elit" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "IsFeatured", Value="0" } },
                                { Guid.NewGuid().ToString(), new Field() { Name = "Tags", Value="" } }
                            }
                        }
                    }
                }
            };
        }
        public Item GetById(string id, string language = null)
        {
            Guid guid;

            if (!Guid.TryParse(id, out guid))
                return null;

            return _idRepository.ContainsKey(guid) ? _idRepository[guid] : null;
        }

        public IEnumerable<Item> GetByQuery(string query, string language = null)
        {
            return _queryRepository.ContainsKey(query) ? _queryRepository[query] : null;
        }
    }
}
