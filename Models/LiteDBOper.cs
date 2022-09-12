using LiteDB;
using System;
using System.IO;
using System.Collections.Generic;

namespace LiteDBViewer.Models;

public static class LiteDBOper
{
    private static LiteDatabase? liteDatabase = null;

    public static void OpenLDB(string filePath)
    {
        if (!File.Exists(filePath))
        {
            liteDatabase = null;
            return;
        }

        ConnectionString connStr = new()
        {
            Filename = filePath,
            Connection = ConnectionType.Shared
        };

        try
        {
            liteDatabase = new LiteDatabase(connStr);
        }
        catch (LiteException)
        {
            liteDatabase = null;
            return;
        }

        return;
    }

    public static void OpenLDB(string filePath, string password = "")
    {
        if (!File.Exists(filePath))
        {
            liteDatabase = null;
            return;
        }

        ConnectionString connStr = new()
        {
            Filename = filePath,
            Password = password,
            Connection = ConnectionType.Shared
        };

        try
        {
            liteDatabase = new LiteDatabase(connStr);
        }
        catch (LiteException)
        {
            liteDatabase = null;
            return;
        }

        return;
    }

    public static void CloseLDB()
    {
        if (liteDatabase != null)
        {
            liteDatabase.Dispose();
            liteDatabase = null;
        }
    }

    public static IEnumerable<string> GetTablesNames()
    {
        IEnumerable<string> result = new List<string>();

        if (liteDatabase != null)
        {
            try
            {
                result = liteDatabase.GetCollectionNames();
            }
            catch (Exception)
            {
                result = new List<string>();
            }
        }

        return result;
    }

    public static IEnumerable<string>? GetKeysFromTable(string tableName)
    {
        if (liteDatabase != null)
        {
            try
            {
                var document = (new List<BsonDocument>(liteDatabase.GetCollection(tableName).FindAll()))[0];

                if (document != null)
                    return document.Keys;                    
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static IEnumerable<IEnumerable<KeyValuePair<string, object>>>? GetTable(string tableName)
    {
        if (liteDatabase != null)
        {
            try
            {
                var table = liteDatabase.GetCollection(tableName).FindAll();

                if (table != null)
                {
                    List<List<KeyValuePair<string, object>>> result = new();

                    foreach (var row in table)
                    {
                        List<KeyValuePair<string, object>> line = new();

                        foreach (var field in row.GetElements())
                        {
                            line.Add(new KeyValuePair<string, object>(field.Key, field.Value.RawValue));
                        }

                        result.Add(line);
                    }

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static IEnumerable<BsonDocument>? GetTableBson(string tableName)
    {
        if (liteDatabase != null)
        {
            try
            {
                List<BsonDocument> table = new(liteDatabase.GetCollection(tableName).FindAll());
                return table;
            }
            catch (Exception)
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}