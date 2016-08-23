using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace RepositoryBrowser.Controllers
{
    public class HDDController : ApiController
    {
        //Повертає список фіксованих локальних дисків на HDD
        public IEnumerable<DriveInfo> Get()
        {
            List<DriveInfo> drives = new List<DriveInfo>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed)
                    drives.Add(drive);
            }
            return drives;
        }

    }
}
