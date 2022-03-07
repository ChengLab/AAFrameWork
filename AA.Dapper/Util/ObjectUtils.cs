﻿using AA.FrameWork;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace AA.Dapper.Util
{
    /// <summary>
    /// 将对象从一种类型转换为另一种类型的实用方法
    /// </summary>
    public static class ObjectUtils
    {
        /// <summary>
        /// Convert the value to the required <see cref="System.Type"/> (if necessary from a string).
        /// </summary>
        /// <param name="newValue">The proposed change value.</param>
        /// <param name="requiredType">
        /// The <see cref="System.Type"/> we must convert to.
        /// </param>
        /// <returns>The new value, possibly the result of type conversion.</returns>
        public static object ConvertValueIfNecessary(Type requiredType, object newValue)
        {
            if (newValue != null)
            {
                // if it is assignable, return the value right away
                if (requiredType.IsInstanceOfType(newValue))
                {
                    return newValue;
                }
                // try to convert using type converter
                TypeConverter typeConverter = TypeDescriptor.GetConverter(requiredType);
                if (typeConverter.CanConvertFrom(newValue.GetType()))
                {
                    return typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, newValue);
                }
                typeConverter = TypeDescriptor.GetConverter(newValue.GetType());
                if (typeConverter.CanConvertTo(requiredType))
                {
                    return typeConverter.ConvertTo(null, CultureInfo.InvariantCulture, newValue, requiredType);
                }
                if (requiredType == typeof(Type))
                {
                    return Type.GetType(newValue.ToString(), true);
                }
                if (newValue.GetType().GetTypeInfo().IsEnum)
                {
                    // If we couldn't convert the type, but it's an enum type, try convert it as an int
                    return ConvertValueIfNecessary(requiredType,
                        Convert.ChangeType(newValue, Convert.GetTypeCode(newValue), null));
                }

                throw new NotSupportedException(newValue + " is no a supported value for a target of type " +
                                                requiredType);
            }
            if (requiredType.GetTypeInfo().IsValueType)
            {
                return Activator.CreateInstance(requiredType);
            }

            // return default
            return null;
        }


        /// <summary>
        /// Instantiates an instance of the type specified.
        /// </summary>
        /// <returns></returns>
        public static T InstantiateType<T>(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), "Cannot instantiate null");
            }
            ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
            if (ci == null)
            {
                throw new ArgumentException("Cannot instantiate type which has no empty constructor", type.Name);
            }
            return (T)ci.Invoke(new object[0]);
        }

        /// <summary>
        /// Sets the object properties using reflection.
        /// </summary>
        public static void SetObjectProperties(object obj, string[] propertyNames, object[] propertyValues)
        {
            for (int i = 0; i < propertyNames.Length; i++)
            {
                string name = propertyNames[i];
                string propertyName = CultureInfo.InvariantCulture.TextInfo.ToUpper(name.Substring(0, 1)) +
                                      name.Substring(1);

                try
                {
                    SetPropertyValue(obj, propertyName, propertyValues[i]);
                }
                catch (Exception nfe)
                {
                    //$"Could not parse property '{name}' into correct data type: {nfe.Message}", nfe);

                }
            }
        }

        /// <summary>
        /// Sets the object properties using reflection.
        /// </summary>
        /// <param name="obj">The object to set values to.</param>
        /// <param name="props">The properties to set to object.</param>
        public static void SetObjectProperties(object obj, NameValueCollection props)
        {
            // remove the type
            props.Remove("type");

            foreach (string name in props.Keys)
            {
                string propertyName = CultureInfo.InvariantCulture.TextInfo.ToUpper(name.Substring(0, 1)) +
                                      name.Substring(1);

                try
                {
                    object value = props[name];
                    SetPropertyValue(obj, propertyName, value);
                }
                catch (Exception nfe)
                {//cant load dbdrive 
                    throw new AAException(
                        $"Could not parse property '{name}' into correct data type: {nfe.Message}", nfe);
                }
            }
        }

        public static void SetPropertyValue(object target, string propertyName, object value)
        {
            Type t = target.GetType();

            PropertyInfo pi = t.GetProperty(propertyName);

            if (pi == null || !pi.CanWrite)
            {
                // try to find from interfaces
                foreach (var interfaceType in target.GetType().GetInterfaces())
                {
                    pi = interfaceType.GetProperty(propertyName);
                    if (pi != null && pi.CanWrite)
                    {
                        // found suitable
                        break;
                    }
                }
            }

            if (pi == null)
            {
                // not match from anywhere
                throw new MemberAccessException($"No writable property '{propertyName}' found");
            }

            MethodInfo mi = pi.GetSetMethod();

            if (mi == null)
            {
                throw new MemberAccessException($"Property '{propertyName}' has no setter");
            }

            if (mi.GetParameters()[0].ParameterType == typeof(TimeSpan))
            {
                // special handling
                value = GetTimeSpanValueForProperty(pi, value);
            }
            else
            {
                value = ConvertValueIfNecessary(mi.GetParameters()[0].ParameterType, value);
            }

            mi.Invoke(target, new[] { value });
        }

        public static TimeSpan GetTimeSpanValueForProperty(PropertyInfo pi, object value)
        {
            object[] attributes = pi.GetCustomAttributes(typeof(TimeSpanParseRuleAttribute), false).ToArray();

            if (attributes.Length == 0)
            {
                return (TimeSpan)ConvertValueIfNecessary(typeof(TimeSpan), value);
            }

            TimeSpanParseRuleAttribute attribute = (TimeSpanParseRuleAttribute)attributes[0];
            long longValue = Convert.ToInt64(value);
            switch (attribute.Rule)
            {
                case TimeSpanParseRule.Milliseconds:
                    return TimeSpan.FromMilliseconds(longValue);
                case TimeSpanParseRule.Seconds:
                    return TimeSpan.FromSeconds(longValue);
                case TimeSpanParseRule.Minutes:
                    return TimeSpan.FromMinutes(longValue);
                case TimeSpanParseRule.Hours:
                    return TimeSpan.FromHours(longValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool IsAttributePresent(Type typeToExamine, Type attributeType)
        {
            return typeToExamine.GetTypeInfo().GetCustomAttributes(attributeType, true).Any();
        }
    }
}
