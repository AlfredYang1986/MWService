/**
 * Server Singlton
 * Create by Alfred Yang
 * 30-8-2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MWServiceDispatchHelper
{
    class Singleton
    {
        private List<Object> _list = new List<Object>();
        private static Singleton _instance = new Singleton();

        public static T GetInstance<T> (Type classType) 
        {
            foreach (var v in _instance._list)
            {
                if(classType.Equals(v.GetType()))
                    return (T)v; 
            }

            ConstructorInfo constructor = classType.GetConstructor(Type.EmptyTypes);
            Object o = constructor.Invoke(null);
            _instance._list.Add(o);
            return (T)o;
        }

        public static T GetInstance<T>()
        {
            Type t = typeof(T);
            return GetInstance<T>(t);
        }

        public static T GetInstance<T>(String strTypeName)
        {
            Type t = Type.GetType(strTypeName);
            return GetInstance<T>(t);
        }
    }
}
