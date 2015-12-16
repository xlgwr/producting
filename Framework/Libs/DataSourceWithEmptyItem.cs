using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Framework.Libs
{
    class DataSourceWithEmptyItem : ITypedList, IList
    {
        protected readonly object NullObject = new object();
        public readonly IList NestedList;
        public ITypedList NestedTypedList { get { return (ITypedList)NestedList; } }
        public DataSourceWithEmptyItem(ITypedList list)
        {
            this.NestedList = (IList)list;
        }

        class EmptyObjectPropertyDescriptor : PropertyDescriptor
        {
            public readonly PropertyDescriptor NestedDescriptor;
            public readonly object NullObject;
            public readonly object NullObjectValue;
            public EmptyObjectPropertyDescriptor(PropertyDescriptor nestedDescriptor, object nullObject, object nullObjectValue)
                : base(nestedDescriptor.Name, (Attribute[])new ArrayList(nestedDescriptor.Attributes).ToArray(typeof(Attribute)))
            {
                this.NestedDescriptor = nestedDescriptor;
                this.NullObject = nullObject;
                this.NullObjectValue = nullObjectValue;
            }
            public override bool CanResetValue(object component)
            {
                return false;
            }
            public override Type ComponentType
            {
                get { return typeof(object); }
            }
            public override object GetValue(object component)
            {
                if (component == NullObject)
                    return NullObjectValue;
                else
                    return NestedDescriptor.GetValue(component);
            }
            public override bool IsReadOnly
            {
                get { return true; }
            }
            public override Type PropertyType
            {
                get { return NestedDescriptor.PropertyType; }
            }
            public override void ResetValue(object component)
            {
                throw new NotSupportedException("The method or operation is not implemented.");
            }
            public override void SetValue(object component, object value)
            {
                throw new NotSupportedException("The method or operation is not implemented.");
            }
            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }
        }
        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            List<PropertyDescriptor> result = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor pd in NestedTypedList.GetItemProperties(ExtractOriginalDescriptors(listAccessors)))
            {
                object nullVal = null;
                if (pd.PropertyType == typeof(string))
                    nullVal = "[empty]";
                result.Add(new EmptyObjectPropertyDescriptor(pd, NullObject, null));
            }
            return new PropertyDescriptorCollection(result.ToArray());
        }
        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return NestedTypedList.GetListName(ExtractOriginalDescriptors(listAccessors));
        }

        protected static PropertyDescriptor[] ExtractOriginalDescriptors(PropertyDescriptor[] listAccessors)
        {
            if (listAccessors == null)
                return null;
            PropertyDescriptor[] convertedDescriptors = new PropertyDescriptor[listAccessors.Length];
            for (int i = 0; i < convertedDescriptors.Length; ++i)
            {
                PropertyDescriptor d = listAccessors[i];
                EmptyObjectPropertyDescriptor c = d as EmptyObjectPropertyDescriptor;
                if (c != null)
                    convertedDescriptors[i] = c.NestedDescriptor;
                else
                    convertedDescriptors[i] = d;
            }
            return convertedDescriptors;
        }
        public int Add(object value)
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        public void Clear()
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        public bool Contains(object value)
        {
            if (value == NullObject)
                return true;
            return NestedList.Contains(value);
        }
        public int IndexOf(object value)
        {
            if (value == NullObject)
                return 0;
            int nres = NestedList.IndexOf(value);
            if (nres < 0)
                return nres;
            return nres + 1;
        }
        public void Insert(int index, object value)
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        public bool IsFixedSize
        {
            get { return true; }
        }
        public bool IsReadOnly
        {
            get { return true; }
        }
        public void Remove(object value)
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        public void RemoveAt(int index)
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        public object this[int index]
        {
            get
            {
                if (index == 0)
                    return NullObject;
                else
                    return NestedList[index - 1];
            }
            set
            {
                throw new NotSupportedException("The method or operation is not implemented.");
            }
        }
        public void CopyTo(Array array, int index)
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        public int Count
        {
            get { return NestedList.Count + 1; }
        }
        public bool IsSynchronized
        {
            get { return false; }
        }
        public object SyncRoot
        {
            get { return NestedList.SyncRoot; }
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotSupportedException("The method or operation is not implemented.");
        }
    }
}
