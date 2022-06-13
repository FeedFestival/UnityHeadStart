using UnityEngine;

namespace Assets.HeadStart.Features.Database
{
    [ExecuteInEditMode]
    public class SqlDatabase : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.1.0";
#pragma warning restore 0414 //
        public void DeleteDataBase()
        {
            var dataService = new DataService();
            dataService.CleanDB();
        }
        public void RecreateDataBase()
        {
            var dataService = new DataService();
            dataService.CleanDB();
            dataService.CreateDB();
        }

        public void CleanUpUsers()
        {
            var dataService = new DataService();
            dataService.CleanUpUsers();
        }
    }
}
