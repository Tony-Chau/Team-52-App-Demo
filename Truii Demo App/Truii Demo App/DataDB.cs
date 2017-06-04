using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono.Data.Sqlite;
using System.IO;
using Android.Content;

namespace Truii_Demo_App
{
    public class DataDB
    {
        string docsFolder;
        string path;
        SqliteConnection connection;
        Context context;
        /// <summary>
        /// This will initialise the connection to the database
        /// </summary>
        /// <param name="context">This will be used for the toast message that appears</param>
        public DataDB(Context context)
        {
            this.context = context;
            docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            path = System.IO.Path.Combine(docsFolder, "DataDB.db");
            connection = new SqliteConnection("Data Source=" + path);
            
        }

        /// <summary>
        /// Reads the data from the list (treat this as an array)
        /// </summary>
        /// <param name="fieldName">The name of the field the user is getting</param>
        /// <param name="index">The index number, similar to an array</param>
        /// <returns></returns>
        public int readData(string fieldName, int index)
        {
            int[] data = new int[Count()];
            connection.Open();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    
                    command.CommandText = "SELECT * FROM Data";
                    var read = command.ExecuteReader();
                    int i = 0;
                    while (read.Read())
                    {
                        data[i] = (int) read[fieldName];
                        i += 1;
                    }
                    connection.Close();
                    return data[index];
                }
            }
            catch (Exception ex)
            { 
                Toast.MakeText(context, ex.Message, ToastLength.Short).Show();
            }
            connection.Close();
            return -1;
        }

        /// <summary>
        /// Creates the database
        /// </summary>
        public async void CreateDatabase()
        {
            var connectionString = string.Format("Data Source={0};Version=3", path);
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
                using (var connect = new SqliteConnection((connectionString)))
                {
                    await connect.OpenAsync();
                    using (var command = connect.CreateCommand())
                    {
                        string QueryCommand = "CREATE TABLE Data(UserID int NOT NULL, DataOne int NOT NULL, DataTwo int NOT NULL, DataThree int NOT NULL)";
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

        /// <summary>
        /// Checks if the database has been created or not
        /// </summary>
        /// <returns></returns>
        public bool checkDb()
        {
            return File.Exists(path);
        }
        /// <summary>
        /// Counts the number of rows
        /// </summary>
        /// <returns></returns>
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
                Toast.MakeText(this.context, ex.Message, ToastLength.Short).Show();
            }
            connection.Close();
            return count;
        }
        /// <summary>
        /// Insert the data
        /// </summary>
        /// <param name="dataOne">Insert DataOne</param>
        /// <param name="dataTwo">Insert DataOne</param>
        /// <param name="dataThree">Insert DataOne</param>
        public void InserData(int dataOne, int dataTwo, int dataThree)
        {
            int count = Count();
            connection.Open();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("INSERT INTO Data(UserID, DataOne, DataTwo, DataThree) VALUES( {0}, {1}, {2}, {3})", count, dataOne, dataTwo, dataThree);
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