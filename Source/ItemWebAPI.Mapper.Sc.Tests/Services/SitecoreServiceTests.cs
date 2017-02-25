using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ItemWebAPI.Mapper.Sc.Contracts;
using ItemWebAPI.Mapper.Sc.Tests.Mock;
using ItemWebAPI.Mapper.Sc.Tests.TestModels;

namespace ItemWebAPI.Mapper.Sc.Services.Tests
{
    [TestClass]
    public class SitecoreServiceTests
    {
        ISitecoreService _sitecoreService;

        public SitecoreServiceTests()
        {
            _sitecoreService = new SitecoreService(new MockSitecoreClient());
        }

        [TestMethod]
        public void CheckIfFieldsAreParsed()
        {
            var results = _sitecoreService.GetItems<ProductTestModel>("QueryForItems");
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
            foreach(var product in results)
            {
                Assert.IsNotNull(product.Id, "ID of product is null");
                Assert.IsNotNull(product.Name, "Name of product is null");
                Assert.IsNotNull(product.Description, "Description of product is null");
            }
        }

        [TestMethod]
        public void CheckIfMultilistFieldsAreFunctional()
        {
            var results = _sitecoreService.GetItems<ProductTestModel>("QueryForItems");
            var result1 = results.FirstOrDefault();

            Assert.IsNotNull(result1.Tags, "Tags for first product is null");
            Assert.IsTrue(result1.Tags.Count() > 0, "Tags for first product is empty");
            Assert.IsTrue(result1.Tags.FirstOrDefault().Name == "Apparel", "The first tag for first product is not Apparel");
            Assert.IsNotNull(result1.Tags.FirstOrDefault().Description, "The description of first tag for first product is null");
            Assert.IsTrue(result1.Tags.LastOrDefault().Name == "Scarves", "The last tag for first product is not Scarves");
            Assert.IsNotNull(result1.Tags.LastOrDefault().Description, "The description of last tag for first product is null");
        }

        [TestMethod]
        public void CheckIfCheckboxFieldsAreFunctional()
        {
            var results = _sitecoreService.GetItems<ProductTestModel>("QueryForItems");
            Assert.IsTrue(results.FirstOrDefault().IsFeatured, "IsFeatured of first product is not true");
            Assert.IsFalse(results.LastOrDefault().IsFeatured, "IsFeature of last product is not false");
        }

        [TestMethod]
        public void CheckIfImageFieldsAreFunctional()
        {
            var results = _sitecoreService.GetItems<ProductTestModel>("QueryForItems");
            Assert.IsNotNull(results.FirstOrDefault().Image, "Image of first product is null");
            Assert.IsNotNull(results.FirstOrDefault().Image.Source, "Image of first product is null");
        }

        [TestMethod]
        public void CheckIfLinkFieldsAreFunctional()
        {
            var results = _sitecoreService.GetItems<ProductTestModel>("QueryForItems");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink,"Product Link is null");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink.Text, "Product Link Text is null");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink.Url, "Product Link Url is null");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink.LinkType, "Product Link LinkType is null");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink.Anchor, "Product Link Anchor is null");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink.Target, "Product Link Target is null");
            Assert.IsNotNull(results.FirstOrDefault().ProductLink.Title, "Product Link Title is null");
        }

        [TestMethod]
        public void CheckIfSingleItemFetchIsFunctional()
        {
            var id = "{DDD7284A-AF73-49CB-8AAD-2C6B928DEA99}";
            var tag = _sitecoreService.GetItem<TagTestModel>(id);
            Assert.IsNotNull(tag);
            Assert.IsNotNull(tag.Id);
            Assert.IsNotNull(tag.Name);
            Assert.IsNotNull(tag.Description);

            Assert.AreEqual(tag.Id, id);
            Assert.AreEqual(tag.Name, "Apparel");
            Assert.AreEqual(tag.Description, "Lorem ipsum dolor sit amet, consectetur adipiscing elit");
        }
    }
}