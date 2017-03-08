using ItemWebAPI.Mapper.Sc.Attributes;
using ItemWebAPI.Mapper.Sc.Contracts;
using ItemWebAPI.Mapper.Sc.Enums;
using ItemWebAPI.Mapper.Sc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Services
{
    /// <summary>
    /// Implementation of Repository through ItemWebAPI client
    /// </summary>
    public class SitecoreService : ISitecoreService
    {
        ISitecoreClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Sitecore API Client</param>
        public SitecoreService(string database = null)
        {
            _client = new SitecoreApiClient(database);
        }

        public SitecoreService(ISitecoreClient client)
        {
            _client = client;
        }

        #region Transformation Helpers

        /// <summary>
        /// Transform a Single Item
        /// </summary>
        /// <typeparam name="T">Type with Parameters</typeparam>
        /// <param name="item">Sitecore Item</param>
        /// <returns></returns>
        private T TransformItem<T>(Item item, string language = null) where T : new()
        {
            var targetItem = new T();
            var propertyInfos = typeof(T).GetProperties();
            foreach (var info in propertyInfos)
            {
                var attributes = info.GetCustomAttributes(typeof(SitecoreFieldAttribute), true);
                if (attributes == null || !attributes.Any()) continue;

                foreach (var attribute in attributes)
                {
                    var mapperAttribute = attribute as SitecoreFieldAttribute;
                    if (mapperAttribute != null)
                    {
                        var property = targetItem.GetType().GetProperty(info.Name);
                        var field = item.Fields.FirstOrDefault(i => i.Value.Name == mapperAttribute.Name).Value;

                        if (mapperAttribute.Type == FieldType.Id)
                        {
                            property.SetValue(targetItem, item.Id);
                            continue;
                        }

                        if (field == null || field.Value == null || String.IsNullOrEmpty(field.Value)) continue;

                        switch (mapperAttribute.Type)
                        {
                            case FieldType.RichText:
                            case FieldType.SingleLineText:
                                property.SetValue(targetItem, field.Value);
                                break;
                            case FieldType.Checkbox:
                                property.SetValue(targetItem, field.Value == "1");
                                break;
                            case FieldType.Number:
                                if (info.PropertyType == typeof(Decimal))
                                    property.SetValue(targetItem, Convert.ToDecimal(field.Value));
                                if (info.PropertyType == typeof(Double))
                                    property.SetValue(targetItem, Convert.ToDouble(field.Value));
                                if (info.PropertyType == typeof(int))
                                    property.SetValue(targetItem, Convert.ToDouble(field.Value));
                                break;
                            case FieldType.Droplink:
                                var method = this.GetType().GetMethod("GetItem").MakeGenericMethod(info.PropertyType);
                                var linkResult = method.Invoke(this, new object[] { field.Value, language });
                                property.SetValue(targetItem, linkResult);
                                break;
                            case FieldType.Multilist:
                            case FieldType.Treelist:
                                method = this.GetType().GetMethod("GetItemsByIds", BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(info.PropertyType.GetGenericArguments()[0]);
                                var listResult = method.Invoke(this, new object[] { field.Value.Split('|').ToArray(), language });
                                property.SetValue(targetItem, listResult);
                                break;
                            case FieldType.GeneralLink:
                                var generallinkResult = field.Value;
                                XmlSerializer serializer = new XmlSerializer(typeof(Link));
                                using (TextReader reader = new StringReader(generallinkResult))
                                {
                                    Link link = (Link)serializer.Deserialize(reader);
                                    property.SetValue(targetItem, link);
                                }
                                break;
                            case FieldType.Image:
                                var imageResult = field.Value;
                                serializer = new XmlSerializer(typeof(Image));
                                using (TextReader reader = new StringReader(imageResult))
                                {
                                    Image image = (Image)serializer.Deserialize(reader);
                                    property.SetValue(targetItem, image);
                                }
                                break;
                            case FieldType.Presentation:
                                var presetnationResult = field.Value;
                                serializer = new XmlSerializer(typeof(Presentation));
                                using (TextReader reader = new StringReader(presetnationResult))
                                {
                                    Presentation link = (Presentation)serializer.Deserialize(reader);
                                    property.SetValue(targetItem, link);
                                }
                                break;
                        }
                    }
                }
            }
            return targetItem;
        }

        /// <summary>
        /// Transform a collection of items
        /// </summary>
        /// <typeparam name="T">Type with parameters</typeparam>
        /// <param name="items">Sitecore Items</param>
        /// <returns></returns>
        private IEnumerable<T> TransformItems<T>(IEnumerable<Item> items, string language = null) where T : new()
        {
            var responseItems = new List<T>();
            foreach (var item in items)
            {
                var targetItem = TransformItem<T>(item, language);
                responseItems.Add(targetItem);
            }
            return responseItems;
        }

        /// <summary>
        /// Helper for fetching multiple items by string of Ids
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        private IEnumerable<T> GetItemsByIds<T>(string[] ids, string language) where T: new()
        {
            var items = new List<Item>();
            foreach (var id in ids)
            {
                var item = _client.GetById(id,language);
                items.Add(item);
            }
            return TransformItems<T>(items);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get Items based on query
        /// </summary>
        /// <typeparam name="T">Type with Parameters</typeparam>
        /// <param name="query">Sitecore Query</param>
        /// <returns></returns>
        public IEnumerable<T> GetItems<T>(string query, string language = null) where T : new()
        {
            var items = _client.GetByQuery(query, language);
            if (items == null)
                return null;
            return TransformItems<T>(items);
        }

        /// <summary>
        /// Get Item based on Item ID
        /// </summary>
        /// <typeparam name="T">Type with Parameters</typeparam>
        /// <param name="id">Sitecore Item ID</param>
        /// <returns></returns>
        public T GetItem<T>(string id, string language = null) where T : new()
        {
            var item = _client.GetById(id, language);
            if (item == null)
                return default(T);
            return TransformItem<T>(item);
        }

        /// <summary>
        /// Fetch Multiple Items By Ids
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns>Collection of Transformed Items</returns>
        public IEnumerable<T> GetItems<T>(string[] ids, string language = null) where T : new()
        {
            return GetItemsByIds<T>(ids, language);
        }

        #endregion
    }
}
