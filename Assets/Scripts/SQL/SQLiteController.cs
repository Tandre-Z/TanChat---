using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class SQLiteController : MonoBehaviour
{
    SQLiteConnection liteConnection;
    // Start is called before the first frame update
    void Start()
    {
        //OpenSqlite();
        //CreatTable();
        //Insert("admin02", "222");
        //Select();
    }

    public void OpenSqlite()
    {
        string path = Application.persistentDataPath + "/UserDB.db";

        liteConnection = new SQLiteConnection(path, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
    }

    public void CreatTable()
    {
        if (liteConnection!=null)
        {
            liteConnection.CreateTable<User>();
            //SQLiteCommand liteCommand = liteConnection.CreateCommand("CREATE TABLE IF NOT EXISTS User(UserName varchar(225),Password varchar(225))");
            //liteCommand.ExecuteNonQuery();
        }
    }

    public void Insert(User user)
    {
        if (liteConnection!=null)
        {
            liteConnection.Insert(user);
            //SQLiteCommand insertCommand = liteConnection.CreateCommand("INSERT INTO User VALUES(\"" + name + "\",\"" + pwd + "\")");
            ////string sql=string.Format("")
            //insertCommand.ExecuteNonQuery();
        }
    }

    public TableQuery<User> Select()
    {
        if (liteConnection!=null)
        {
            TableQuery<User> ts= liteConnection.Table<User>();
            //.Where(user=>user.UserName==userName)---->lamder语句
            //string sql = "SELECT * FROM " + TableName;
            //SQLiteCommand liteCommand = liteConnection.CreateCommand(sql);

            //List<T> users = liteCommand.ExecuteQuery<T>();

            return ts;
        }
        return null;
    }
    
    public void Delete(string userName)
    {
        if(liteConnection!=null)
        {
            liteConnection.Delete(userName);
        }
    }

    public void UpdateUser(User user)
    {
        if(liteConnection!=null)
        {
            liteConnection.Update(user);
        }
    }

    public void CloseSqlite()
    {
        if (liteConnection != null)
        {
            liteConnection.Close();
            liteConnection.Dispose();

            System.GC.Collect();
        }
    }

    private void OnDestory()
    {
        CloseSqlite();
    }


   
}
