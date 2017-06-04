using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;
using System.IO;

namespace Truii_Demo_App
{
    public class DataDB
    {
        string docsFolder;
        string path;
        SqliteConnection connection;
        Context context;
        public DataDB(Context context)
        {
            this.context = context;
            docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            path = System.IO.Path.Combine(docsFolder, "DataDB.db");
            connection = new SqliteConnection("Data Source=" + path);
            
        }
        public async void CreateDatabase()
        {
            try
            {
                SqliteConnection.CreateFile(path);
            }catch (IOException ex)
            {
                Toast.MakeText(this.context, ex.Message, ToastLength.Short).Show();
            }
            connection.Open();
            try
            {
                using (var connect = new SqliteConnection(connection))
                {
                    await connect.OpenAsync();
                    using (var command = connect.CreateCommand())
                    {
                        string QueryCommand = "CREATE TABLE Data(UserID INTEGER PRIMARY KEY AUTOINCREMENT, DataOne int NOT NULL, DataTwo int NOT NULL, DataThree int NOT NULL)";
                        command.CommandText = QueryCommand;
                        command.CommandType = System.Data.CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.context, ex.Message, ToastLength.Short).Show();
            }
            connection.Close();
        }

        public bool checkDb()
        {
            return File.Exists(path);
        }

        public int Count()
        {
            int count = 0;
            connection.Open();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Select * FROM Data";
                    
                    var read = command.ExecuteReader();
                    while (read.Read())
                    {
                        count += 1;
                    }
                }
            }catch(Exception ex)
            {
            }
            connection.Close();
            return count;
        }
        public void InserData(int dataOne, int dataTwo, int dataThree)
        {
            connection.Open();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    string QueryCommand = "INSERT INTO Data(DataOne, DataTwo, DataThree) VALUES(" + dataOne + ", " + dataTwo + ", " + dataThree + ")";
                    command.CommandText = QueryCommand;
                    var rowcount = command.ExecuteNonQuery();
                }
            }catch (Exception ex)
            {
                Toast.MakeText(this.context, ex.Message, ToastLength.Short).Show();
            }
            connection.Close();
        }
    }
}