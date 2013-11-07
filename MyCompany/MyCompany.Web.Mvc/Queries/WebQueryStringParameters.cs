using System;
using System.Collections;
using System.Collections.Generic;

namespace MyCompany.Web.Mvc.Queries
{
    public class WebQueryStringParameters : ICollection
    {
        private Dictionary<string, object> _parameters;

        public WebQueryStringParameters()
        {
            _parameters = new Dictionary<string, object>();
        }

        public void Add<T>(WebQueryStringParameter<T> item)
        {
            _parameters.Add("[" + item.PropertyName + "]", item);
        }

        public void Remove(string key)
        {
            _parameters.Remove(key);
        }

        public Dictionary<string, object> ParameterList()
        {
            return _parameters;
        }

        public object this[string name]
        {
            get
            {
                return name.StartsWith("[") ? this._parameters[name] : this._parameters[GetKey(name)];
            }
        }

        public void CopyTo(Array array, int index)
        {
            
        }

        public int Count
        {
            get { return _parameters.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return null; }
        }

        public IEnumerator GetEnumerator()
        {
            return null;
        }

        private static string GetKey(string parameterName)
        {
            return "[" + parameterName + "]";
        }

    }
}
