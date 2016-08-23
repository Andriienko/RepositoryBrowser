using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace RepositoryBrowser.Models
{
    public class Temp
    {
        public string dir { get; set; }
    }
    //Клас який рахує кількість обєктів, а також виконує переходи по вказаним директоріям
    public class Repository
    {
        DirectoryInfo dir;
        int CountLess10MB;
        int Count10_50MB;
        int CountMore100MB;
        public DirectoryInfo Dir
        {
            get { return dir; }
            set { dir = value; }
        }
        public Repository(DirectoryInfo dir)
        {
            this.dir = dir;
            CountLess10MB = Count10_50MB = CountMore100MB = 0;
        }
        //Підрахунок кількості файлів у каталозі, який передається в метод
        void Count(DirectoryInfo dir)
        {
            foreach (var file in dir.GetFiles())
            {
                if (((file.Attributes & FileAttributes.System) != FileAttributes.System) && ((file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden))
                {
                    if (file.Length <= 1048576 * 10)
                        CountLess10MB++;
                    else if (file.Length > 1048576 * 10 & file.Length <= 1048576 * 50)
                        Count10_50MB++;
                    else if (file.Length > 1048576 * 100)
                        CountMore100MB++;
                }
            }
        }
        //Рекурсивний метод для підрахунку кількості файлів та їх розмірів у всіх вкладених каталогах
        void Evaluate(DirectoryInfo dir)
        {
            //Базовий випадок. Коли у каталозі немає підкаталогів визивається метод для підрахунку файлів
            if (dir.GetDirectories().Length == 0)
            {
                Count(dir);
                return;
            }
            //Відсів каталогів, які мають системний, або прихований атрибут
            foreach (var subdir in dir.GetDirectories())
            {
                if (((subdir.Attributes & FileAttributes.System) != FileAttributes.System) && ((subdir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden))
                {
                    try
                    {
                        Evaluate(subdir);
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.Message); }
                }
            }
            //Якщо проглянуті всі підкаталоги даного каталогу починається підрахунок файлів в даному каталозі
            //після чого перехід на рівень вище
            Count(dir);
        }
        //Метод, що повертає підкаталоги даного каталогу(поле dir)
        //Підкаталоги, що мають системний, або прихований атрибут не повертаються
        public IEnumerable<DirectoryInfo> GetDirectories()
        {
            if (dir == null)
                return null;
            List<DirectoryInfo> showDir = new List<DirectoryInfo>();
            foreach (var subdir in dir.EnumerateDirectories())
            {
                if (((subdir.Attributes & FileAttributes.System) != FileAttributes.System) &&
                        ((subdir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden))
                    showDir.Add(subdir);
            }
            return showDir.AsEnumerable();
        }
        //Метод, що повертає інформацію про файли даного каталогу(поле dir)
        //Файли, що мають системний, або прихований атрибут не враховуються
        public IEnumerable<FileInfo> GetFiles()
        {
            List<FileInfo> showFile = new List<FileInfo>();
            foreach (var file in dir.EnumerateFiles())
            {
                if (((file.Attributes & FileAttributes.System) != FileAttributes.System) &&
                        ((file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden))
                    showFile.Add(file);
            }
            return showFile.AsEnumerable();
        }
        //Метод, що повертає інф. про кількість файлів в підкаталогах
        public IEnumerable<int> GetCount()
        {
            List<int> counts = new List<int>();
            CountLess10MB = Count10_50MB = CountMore100MB = 0;
            Evaluate(dir);
            counts.Add(CountLess10MB);
            counts.Add(Count10_50MB);
            counts.Add(CountMore100MB);
            return counts.AsEnumerable();
        }
    }
}