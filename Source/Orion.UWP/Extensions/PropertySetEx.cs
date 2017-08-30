using System;

using Windows.Foundation.Collections;

namespace Orion.UWP.Extensions
{
    public static class PropertySetEx
    {
        public static bool CheckContainsKeyAndType(this IPropertySet obj, string key, Type type)
        {
            if (!obj.ContainsKey(key))
                return false;
            var value = obj[key];
            return value.GetType() == type;
        }
    }
}