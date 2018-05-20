using System.Collections.Generic;

namespace System.Linq.Dynamic.Core
{
    public class DynamicObjectClass : DynamicClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicObjectClass"/> class.
        /// </summary>
        public DynamicObjectClass()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicObjectClass"/> class.
        /// </summary>
        /// <param name="propertylist">The propertylist.</param>
        public DynamicObjectClass(params KeyValuePair<string, object>[] propertylist)
        {
#if !NET35
            foreach (var kvp in propertylist)
            {
                _propertiesDictionary.Add(kvp.Key, kvp.Value);
            }
#endif
        }
    }
}
