using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace VideoService
{
    public class RWWebconfig : ConfigurationSection
    {
        [ConfigurationProperty("sdnname", IsRequired = true)]
        public string SdnName
        {
            get { return this["sdnname"].ToString(); }
            set { this["sdnname"] = value; }
        }

        [ConfigurationProperty("sdnvalue", IsRequired = true)]
        public string SdnValue
        {
            get { return this["sdnvalue"].ToString(); }
            set { this["sdnvalue"] = value; }
        }
    }
}