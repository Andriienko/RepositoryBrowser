using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RepositoryBrowser.Controllers
{
    public class CountController : ApiController
    {
        public IEnumerable<int> Get()
        {
            //Закоментувать блок if, якщо потрібно підраховувати файли на диску С:\
            //Оскільки у більшості користувачів Windows він розташований на локальному диску С:\, який містить 
            //величезну кількість файлів, то з метою економії часу підрахунок файлів в ньому не провоиться.
            //Лише якщо перейти у підкаталог
            if (RepositoryController.ob.Dir.FullName == @"C:\")
                return new int []{0,0,0};
            return RepositoryController.ob.GetCount();
        }
    }
}
