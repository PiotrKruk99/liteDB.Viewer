using System;
using System.Dynamic;
using System.Collections;
using System.ComponentModel;

namespace LiteDBViewer.Models;

public class DynamicTable : IList, ITypedList
{
    private ArrayList rows;

    DynamicTable()
    {
        rows = new();
    }

    public int Add(object? obj)
    {
        if (obj == null)
        {
            return -1;
        }
        else
        {
            rows.Add(obj);
            return rows.Count - 1;
        }
    }

    public void Clear()
    {
        rows.Clear();
    }

    public bool Contains(object? obj)
    {
        return rows.Contains(obj);
    }

    public int IndexOf(object? obj)
    {
        return rows.IndexOf(obj);
    }

    public void Insert(int index, object? obj)
    {
        rows.Insert(index, obj);
    }

    public void Remove(object? obj)
    {
        rows.Remove(obj);
    }

    public void RemoveAt(int index)
    {
        rows.RemoveAt(index);
    }

    public bool IsFixedSize
    {
        get => true;
    }

    public bool IsReadOnly
    {
        get => false;
    }

    public object? this[int index]
    {
        get => rows[index];
        set => rows[index] = value;
    }

    public int Count
    {
        get => rows.Count;
    }

    public void CopyTo(Array array, int index)
    {
        rows.CopyTo(array, index);
    }

    public bool IsSynchronized
    {
        get => rows.IsSynchronized;
    }

    public object SyncRoot
    {
        get => rows.SyncRoot;
    }

    public IEnumerator GetEnumerator()
    {
        return rows.GetEnumerator();
    }
}

public class DynamicRow : DynamicObject
{

}