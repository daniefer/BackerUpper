using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;

namespace BackerUpper.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Filters.Clear();
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new );
        }
    }
}
