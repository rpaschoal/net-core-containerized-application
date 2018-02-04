using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Extensions
{
    /// <summary>
    /// Reflection helper class.
    /// Credits: http://mcgivery.com/c-reflection-get-property-value-of-nested-classes/
    /// </summary>
    public static class ReflectionExtensions
    {
        public static Object GetPropValue(this Object obj, String propName)
        {
            string[] nameParts = propName.Split('.');
            if (nameParts.Length == 1)
            {
                return obj.GetType().GetProperty(propName).GetValue(obj, null);
            }

            foreach (String part in nameParts)
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static void SetPropValue(this Object obj, String propName, object value, bool setUnsafe = false)
        {
            string[] nameParts = propName.Split('.');
            if (nameParts.Length == 1 && !setUnsafe)
            {
                return;
            }

            foreach (String part in nameParts)
            {
                if (obj == null) { break; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { break; }

                // Check if we are in last level to set the value
                if (info.Name == nameParts[nameParts.Length - 1])
                {
                    Type t = Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType;

                    if (!setUnsafe)
                    {
                        object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                        info.SetValue(obj, safeValue, null);
                    }
                    else
                    {
                        info.SetValue(obj, value, null);
                    }

                }
                else
                {
                    // Keep going to sub levels...
                    obj = info.GetValue(obj, null);
                }
            }
        }

        public static PropertyInfo GetNestedPropInfo(this Object obj, String propName)
        {
            try
            {
                string[] nameParts = propName.Split('.');

                PropertyInfo info = obj.GetType().GetProperty(propName);

                if (nameParts.Length != 1)
                {
                    foreach (String part in nameParts)
                    {
                        if (obj == null) { break; }

                        Type type = obj.GetType();
                        info = type.GetProperty(part);

                        // May be a collection type. Lets try to proceed with this thought.
                        if (info == null)
                        {
                            var genericTypeForCollection = type.GetGenericArguments().SingleOrDefault();
                            if (genericTypeForCollection != null)
                            {
                                info = genericTypeForCollection.GetProperty(part);

                                if (info != null)
                                {
                                    if (info.PropertyType.GetConstructor(Type.EmptyTypes) != null)
                                    {
                                        obj = Activator.CreateInstance(info.PropertyType);
                                        continue;
                                    }
                                    else
                                    {
                                        // Stopping here, can't go further.
                                        break;
                                    }
                                }
                            }
                        }

                        if (info == null) { break; }

                        obj = info.GetValue(obj, null);
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static PropertyInfo GetNestedPropInfo(this Object obj, String propName, out int collectionPathTotal)
        {
            collectionPathTotal = 0;

            try
            {
                string[] nameParts = propName.Split('.');

                PropertyInfo info = obj.GetType().GetProperty(propName);

                if (nameParts.Length != 1)
                {
                    foreach (String part in nameParts)
                    {
                        if (obj == null) { break; }

                        Type type = obj.GetType();
                        info = type.GetProperty(part);

                        // May be a collection type. Lets try to proceed with this thought.
                        if (info == null)
                        {
                            var genericTypeForCollection = type.GetGenericArguments().SingleOrDefault();
                            if (genericTypeForCollection != null)
                            {
                                info = genericTypeForCollection.GetProperty(part);
                                collectionPathTotal++;

                                if (info != null)
                                {
                                    if (info.PropertyType.GetConstructor(Type.EmptyTypes) != null)
                                    {
                                        obj = Activator.CreateInstance(info.PropertyType);
                                        continue;
                                    }
                                    else
                                    {
                                        // Stopping here, can't go further.
                                        break;
                                    }
                                }
                            }
                        }

                        if (info == null) { break; }

                        obj = info.GetValue(obj, null);
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a member of type property name.
        /// http://stackoverflow.com/questions/273941/get-property-name-and-type-using-lambda-expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyName<T, U>(Expression<Func<T, U>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member.Name;

            throw new ArgumentException("Expression is not a member access", "expression");
        }
    }
}
