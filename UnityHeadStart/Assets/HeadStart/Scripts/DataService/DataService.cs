using System;
using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.utils;

public class DataService
{
    public static readonly string _version = "1.0.1";
    public string DefaultDatabaseName = "Database.db";
    const string _assetsPath = "Assets/HeadStart";
    private SQLiteConnection _connection;

    public DataService(string databaseName = null)
    {
        if (string.IsNullOrEmpty(databaseName) == false)
        {
            DefaultDatabaseName = databaseName;
        }
        #region DataServiceInit
#if UNITY_EDITOR
        var dbPath = string.Format(_assetsPath + @"/StreamingAssets/{0}", DefaultDatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DefaultDatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DefaultDatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DefaultDatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DefaultDatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DefaultDatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DefaultDatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif

        #endregion

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);
    }



    public void CleanUpUsers()
    {
        _connection.DropTable<User>();
        _connection.CreateTable<User>();
    }

    public void CleanDB()
    {
        _connection.DropTable<User>();
    }

    public void CreateDB()
    {
        _connection.CreateTable<User>();
    }

    public void CreateDBIfNotExists()
    {
        try
        {
            _connection.Table<User>().Where(x => x.Id == 1).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            CreateDB();
        }
    }

    /*
     * User
     * * --------------------------------------------------------------------------------------------------------------------------------------
     */

    public void CreateUser(User user)
    {
        _connection.Insert(user);
    }

    public void UpdateUser(User user)
    {
        int rowsAffected = _connection.Update(user);
        Debug.Log("(UPDATE User) rowsAffected : " + rowsAffected);
    }

    public User GetUser()
    {
        try
        {
            return _connection.Table<User>().Where(x => x.Id == 1).FirstOrDefault();
        }
        catch (Exception)
        {
            _connection.CreateTable<User>();
            return _connection.Table<User>().Where(x => x.Id == 1).FirstOrDefault();
        }
    }

    public User GetLastUser()
    {
        if (_connection.Table<User>().Count() > 0)
        {
            return _connection.Table<User>().Last();
        }
        return null;
    }

    //public User GetUserByFacebookId(int facebookId)
    //{
    //    return _connection.Table<User>().Where(x => x.FacebookApp.FacebookId == facebookId).FirstOrDefault();
    //}

    /*
    * User - END
    * * --------------------------------------------------------------------------------------------------------------------------------------
    */

    /*
     * Map
     * * --------------------------------------------------------------------------------------------------------------------------------------
     */

    // X : 0 - 11
    // Y : 0 - 8
    //public int CreateMap(Map map)
    //{
    //    _connection.Insert(map);
    //    return map.Id;
    //}

    //public int UpdateMap(Map map)
    //{
    //    _connection.Update(map);
    //    return map.Id;
    //}

    //public Map GetMap(int mapId)
    //{
    //    return _connection.Table<Map>().Where(x => x.Id == mapId).FirstOrDefault();
    //}

    //public int GetNextMapId(int number)
    //{
    //    return _connection.Table<Map>().Where(x => x.Number == number).FirstOrDefault().Id;
    //}

    //public void CreateTiles(List<MapTile> mapTiles)
    //{
    //    _connection.InsertAll(mapTiles);
    //}

    //public IEnumerable<Map> GetMaps()
    //{
    //    return _connection.Table<Map>();
    //}

    //public IEnumerable<MapTile> GetTiles(int mapId)
    //{
    //    return _connection.Table<MapTile>().Where(x => x.MapId == mapId);
    //}

    //public void DeleteMapTiles(int mapId)
    //{
    //    var sql = string.Format("delete from MapTile where MapId = {0}", mapId);
    //    _connection.ExecuteSql(sql);
    //}
}
