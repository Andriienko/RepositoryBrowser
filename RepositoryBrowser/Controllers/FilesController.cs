using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace RepositoryBrowser.Controllers
{
    public class FilesController : ApiController
    {

        public IEnumerable<FileInfo> Get()
        {
            return RepositoryController.ob.GetFiles();
        }
        //Метод, що повертає шлях до поточного каталогу
        public string Get(int val)
        {
            return RepositoryController.ob.Dir.FullName;
        }
    }
}
