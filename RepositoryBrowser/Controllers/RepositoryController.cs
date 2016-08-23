using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using RepositoryBrowser.Models;

namespace RepositoryBrowser.Controllers
{
    public class RepositoryController : ApiController
    {
        static public Repository ob = new Repository(new DirectoryInfo(@"."));
        public IEnumerable<DirectoryInfo> Get()
        {
            return ob.GetDirectories();
        }
        public IEnumerable<DirectoryInfo> Get(int value)
        {
            if (ob.Dir.Parent == null)
                return ob.GetDirectories();
            ob.Dir = ob.Dir.Parent;
            return ob.GetDirectories(); ;
        }
        //Обновлюємо поле dir згідно каталогу у який перейшли і повертає список підкаталогів
        public IEnumerable<DirectoryInfo> Post(Temp val)
        {
            if (val.dir == null) return null;
            ob.Dir = new DirectoryInfo(val.@dir);
            return ob.GetDirectories();
        }

        public void Put(string value)
        {
            

        }
    }
}
