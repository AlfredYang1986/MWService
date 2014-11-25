using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
/*
 * Catherine Added file
 * Lambda expression helper
 */
namespace MWDataEntity
{
    class MWExpressionManyToManyHelper
    {
        public static Expression ColorLambdaGen<T, U, C>(ParameterExpression inner_parmer,string propertyName,string propertyName2, C propertyValue, string method)
        {
            var property_name = Expression.Property(inner_parmer, typeof(T).GetProperty(propertyName));
            var property_name_1 = Expression.Property(property_name, typeof(U).GetProperty(propertyName2));
            return Expression.Equal(property_name_1, Expression.Constant(propertyValue, typeof(C)));
        }

        public static Expression SizeLambdaGen<T, U, C>(ParameterExpression inner_parmer, string propertyName, string propertyName2, C propertyValue, string method)
        {
            var property_name = Expression.Property(inner_parmer, typeof(T).GetProperty(propertyName));
            var property_name_1 = Expression.Property(property_name, typeof(U).GetProperty(propertyName2));
            return Expression.Equal(property_name_1, Expression.Constant(propertyValue, typeof(C)));
        }
       
        public static Expression CategoryLambdaGen<T, U>(ParameterExpression inner_parmer, string propertyName, U propertyValue, string method)
        {
            var property_name = Expression.Property(inner_parmer, typeof(T).GetProperty(propertyName));
            return Expression.Equal(property_name, Expression.Constant(propertyValue, typeof(U)));
        }

        public static Expression SenceLambdaGen<T, C>(ParameterExpression inner_parmer, string propertyName, C propertyValue, string method)
        {
            var property_name = Expression.Property(inner_parmer, typeof(T).GetProperty(propertyName));

            return Expression.Equal(property_name, Expression.Constant(propertyValue, typeof(C)));
        }

        public static Expression innerLambdaGen<T, U, G>(ParameterExpression inner_parmer, IList<G> conditions, string propertyName,string propertyName2, string propertyName3, string Tag)
        {
            dynamic Exp = null;
            dynamic LambdaExp = null;
            foreach (G condition in conditions)
            {
                if (Tag == "color")
                     LambdaExp = ColorLambdaGen<T, U, G>(inner_parmer, propertyName,propertyName2, condition, null);
                else if (Tag == "category")
                    LambdaExp = CategoryLambdaGen<T, G>(inner_parmer, propertyName, condition, null);
                else if (Tag == "size")
                    LambdaExp = SizeLambdaGen<T, U, G>(inner_parmer, propertyName, propertyName2, condition, null);
                else if (Tag == "sence")
                    LambdaExp = SenceLambdaGen<T, G>(inner_parmer, propertyName,  condition, null);
                else if (Exp == null)
                    Exp = LambdaExp;
                else
                    LambdaExp = Expression.OrElse(LambdaExp, Exp);
            }

            return Expression.Lambda<Func<T, bool>>(
                        LambdaExp, new ParameterExpression[] { inner_parmer });

        }
        
        public static MethodBase GetGenericMethod(Type type, string name, Type[] typeArgs, Type[] argTypes, BindingFlags flags)
        {
            int typeArity = typeArgs.Length;
            var methods = type.GetMethods()
                .Where(m => m.Name == name)
                .Where(m => m.GetGenericArguments().Length == typeArity)
                .Select(m => m.MakeGenericMethod(typeArgs));

            return Type.DefaultBinder.SelectMethod(flags, methods.ToArray(), argTypes, null);
        }

        public static bool IsIEnumerable(Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        public static Type GetIEnumerableImpl(Type type)
        {
            // Get IEnumerable implementation. Either type is IEnumerable<T> for some T, 
            // or it implements IEnumerable<T> for some T. We need to find the interface.
            if (IsIEnumerable(type))
                return type;
            Type[] t = type.FindInterfaces((m, o) => IsIEnumerable(m), null);
            return t[0];
        }
    }
}
