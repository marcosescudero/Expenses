[assembly: Xamarin.Forms.Dependency(typeof(Expenses.Droid.Implementations.PathService))]
namespace Expenses.Droid.Implementations
{
    using Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Expenses.db3");
        }
    }
}
