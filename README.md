# Mapper for Sitecore Item Web API [![Build Status](https://travis-ci.org/sidaudhi/ItemWebAPI.Mapper.Sc.svg?branch=master)](https://travis-ci.org/sidaudhi/ItemWebAPI.Mapper.Sc)

This library helps developers to query Sitecore and map custom models to Sitecore Items. 

The library integrates with Sitecore through the Item Web API with all integration parameters configurable through a new configuration section.

## NuGet Packages

### Published Packages

You can try the libraries immmediately by installing the following [ItemWebAPI.Mapper NuGet packages](https://www.nuget.org/packages/ItemWebAPI.Mapper.Sc/).

```
Install-Package ItemWebAPI.Mapper.Sc
```

## Operations

Currently the following operations are supported.

### Authentication

To access Sitecore's Item Web API you must have a valid service account created.
You can configure the username and password as part of App_Config/SitecoreAPI.config.

### Models

You can setup your models for mapping using the SitecoreField attribute available with the library:

```
public class Product
{
    [SitecoreField("Id", FieldType.Id)]
    public string Id { get; set; }

    [SitecoreField("Name", FieldType.SingleLineText)]
    public string Name { get; set; }

    [SitecoreField("Description", FieldType.SingleLineText)]
    public string Description { get; set; }

    [SitecoreField("IsFeatured", FieldType.Checkbox)]
    public bool IsFeatured { get; set; }

    [SitecoreField("Tags", FieldType.Multilist)]
    public IEnumerable<Tag> Tags { get; set; }
}

public class Tag
{
    [SitecoreField("Name", FieldType.SingleLineText)]
    public string Name {get}
   
    [SitecoreField("Description", FieldType.SingleLineText)]
    public string Description { get; set; }

}
```

### Instantiation

If your project uses a DI framework like Autofac or Ninject, you can register an instance of SitecoreService using the ISitecoreService interface:
```
builder.RegisterType<SitecoreService>().As<ISitecoreService>().SingleInstance();
```

In case there is DI setup, you can instantiate the type as:
```
ISitecoreService sitecoreService = new SitecoreService(); 
//You can also pass database as a parameter to the instantiation
```

### Querying

Below are some simple examples that show how to use the library to query for items:

#### By Sitecore Query

```
string query = "fast:/sitecore/Content/Shared Data/Products/*[@Tag == '{82A3AA39-2462-4189-B496-833118A04719}']";
IEnumerable<Product> products = sitecoreService.GetItems<Product>(query);
```
 
#### By ID

```
string id = "{82A3AA39-2462-4189-B496-833118A04719}";
Tag tag = sitecoreService.GetItem<Tag>(id);
```

#### By IDs
```
string[] ids = new string[] { 
		"{82A3AA39-2462-4189-B496-833118A04719}",
		"{DDD7284A-AF73-49CB-8AAD-2C6B928DEA99}"  
		};
IEnumerable<Tag> tags = sitecoreService.GetItems<Tag>("{82A3AA39-2462-4189-B496-833118A04719}");
```

## Contributing to the Repository ###

If you find any issues or opportunities for improving this respository, fix them!  Feel free to contribute to this project by [forking](http://help.github.com/fork-a-repo/) this repository and make changes to the content.  Once you've made your changes, share them back with the community by sending a pull request. Please see [How to send pull requests](http://help.github.com/send-pull-requests/) for more information about contributing to Github projects.

## Reporting Issues ###

If you find any issues with this demo that you can't fix, feel free to report them in the [issues](https://github.com/developerforce/Force.com-Toolkit-for-NET/issues) section of this repository.