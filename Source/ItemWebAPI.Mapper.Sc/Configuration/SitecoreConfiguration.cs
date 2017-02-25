using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ItemWebAPI.Mapper.Sc.Configuration
{
    public class SitecoreConfiguration: ConfigurationSection
    {
        [ConfigurationProperty("host")]
        public HostElement Host
        {
            get
            {
                return (HostElement)this["host"];
            }
            set
            { this["host"] = value; }
        }
    }

    public class HostElement: ConfigurationElement
    {
        [ConfigurationProperty("endpoint", IsRequired = true)]
        public string Endpoint
        {
            get
            {
                return (string)this["endpoint"];
            }
            set
            {
                this["endpoint"] = value;
            }
        }

        [ConfigurationProperty("database", IsRequired = true)]
        public string Database
        {
            get
            {
                return (string)this["database"];
            }
            set
            {
                this["database"] = value;
            }
        }

        [ConfigurationProperty("language", IsRequired = true)]
        public string Language
        {
            get
            {
                return (string)this["language"];
            }
            set
            {
                this["language"] = value;
            }
        }

        [ConfigurationProperty("credentials", IsRequired = true)]
        public CredentialsElement Credentials
        {
            get
            {
                return (CredentialsElement)this["credentials"];
            }
            set
            {
                this["credentials"] = value;
            }
        }
    }

    public class CredentialsElement: ConfigurationElement
    {
        [ConfigurationProperty("username", IsRequired = true)]
        public string UserName
        {
            get
            {
                return (string)this["username"];
            }
            set
            {
                this["username"] = value;
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }

        [ConfigurationProperty("encrypt")]
        public bool Encrypt
        {
            get
            {
                return (bool)this["encrypt"];
            }
            set
            {
                this["encrypt"] = value;
            }
        }
    }
}