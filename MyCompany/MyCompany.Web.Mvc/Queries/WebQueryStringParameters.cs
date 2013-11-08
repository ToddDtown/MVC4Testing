using System.Collections;
using System.Collections.Generic;

namespace MyCompany.Web.Mvc.Queries
{
    public class WebQueryStringParameters : ICollection<IUrlParameter>
    {
        private Dictionary<string, IUrlParameter> _parameters;

        public WebQueryStringParameters()
        {
            _parameters = new Dictionary<string, IUrlParameter>();
        }

        public void Add<T>(WebQueryStringParameter<T> item)
        {
            _parameters.Add("[" + item.PropertyName + "]", item);
        }

        public void Add(IUrlParameter item)
        {
            _parameters.Add(item.PropertyName, item);
        }

        public IUrlParameter this[string name]
        {
            get
            {
                IUrlParameter item = null;

                var key = GetKey(name);
                if (_parameters.ContainsKey(key))
                    item = _parameters[key];

                return item;
            }
        }

        public int Count
        {
            get { return _parameters.Count; }
        }
        
        private static string GetKey(string parameterName)
        {
            return "[" + parameterName + "]";
        }

        public void Clear()
        {
            _parameters.Clear();
        }

        public bool Contains(IUrlParameter item)
        {
            return _parameters.ContainsKey(item.PropertyName);
        }

        public void CopyTo(IUrlParameter[] array, int arrayIndex)
        {
            
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IUrlParameter item)
        {
            return _parameters.Remove(item.PropertyName);
        }

        IEnumerator<IUrlParameter> IEnumerable<IUrlParameter>.GetEnumerator()
        {
            return _parameters.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var parameter in this)
            {
                yield return parameter;
            }
        }
    }
}
