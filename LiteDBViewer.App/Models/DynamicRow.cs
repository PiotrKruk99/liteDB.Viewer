using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using Dynamitey;

namespace LiteDBViewer.Models;

public class DynamicRow : IList, ITypedList
{
    private ArrayList row;

    DynamicRow()
    {
        row = new();
    }

    public int Add(object? obj)
    {
        if (obj == null)
        {
            return -1;
        }
        else
        {
            row.Add(obj);
            return row.Count - 1;
        }
    }

    public void Clear()
    {
        row.Clear();
    }

    public bool Contains(object? obj)
    {
        return row.Contains(obj);
    }

    public int IndexOf(object? obj)
    {
        return row.IndexOf(obj);
    }

    public void Insert(int index, object? obj)
    {
        row.Insert(index, obj);
    }

    public void Remove(object? obj)
    {
        row.Remove(obj);
    }

    public void RemoveAt(int index)
    {
        row.RemoveAt(index);
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
        get => row[index];
        set => row[index] = value;
    }

    public int Count
    {
        get => row.Count;
    }

    public void CopyTo(Array array, int index)
    {
        row.CopyTo(array, index);
    }

    public bool IsSynchronized
    {
        get => row.IsSynchronized;
    }

    public object SyncRoot
    {
        get => row.SyncRoot;
    }

    public IEnumerator GetEnumerator()
    {
        return row.GetEnumerator();
    }

    public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
    {
        PropertyDescriptorCollection pdc;

        PropertyDescriptor[] pds = new PropertyDescriptor[row.Count];

        for (var i=0; i<row.Count; i++)
            pds[i] = new DynamicPropertyDescriptor((row[i] as KeyValuePair<string, object>?).Value.Key);

        return pdc = new PropertyDescriptorCollection(pds);
    }

    public string GetListName(PropertyDescriptor[] listAccessors)
        {   
            return "";
        }

    private class DynamicPropertyDescriptor : PropertyDescriptor
    {
        public DynamicPropertyDescriptor(string name) : base(name, null)
        {
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return Dynamic.InvokeGet(component, Name);
        }

        public override void ResetValue(object component)
        {

        }

        public override void SetValue(object component, object value)
        {
            Dynamic.InvokeSet(component, Name, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(object); }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get
            {
                return typeof(object);
            }
        }
    }
}

