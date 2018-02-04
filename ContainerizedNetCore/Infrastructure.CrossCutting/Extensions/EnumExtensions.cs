using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Extensions
{
    public static class EnumUtility
    {
        /// <summary>
        /// Returns an Enum Description attribute value.
        /// </summary>
        /// <param name="value">The current enum value.</param>
        /// <returns>
        /// Extracted text from the Description attribute of the supplied enum value.
        /// </returns>
        /// <remarks>
        /// As default underlying data type for an enum is an int, it's possible to get the description by enum index just by cast it the value in caller method
        /// </remarks> 
        /// <example>
        /// ex: int value = 1;
        /// string description = Enumerations.GetEnumDescription((MyEnum)value);
        /// </example>
        public static string GetEnumDescription(this System.Enum value)
        {
            try
            {
                System.Reflection.FieldInfo info = value.GetType().GetField(value.ToString());

                System.ComponentModel.DescriptionAttribute[] attributes = (System.ComponentModel.DescriptionAttribute[])info.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}
