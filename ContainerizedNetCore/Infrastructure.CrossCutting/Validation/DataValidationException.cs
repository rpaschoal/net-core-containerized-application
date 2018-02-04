using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Custom validation class that identifies a data error.
    /// </summary>
    public class DataValidationException : Exception
    {
        public DataValidationException(string message, Dictionary<string, IList<string>> validationDetails) : base(message)
        {
            this.ValidationDetails = validationDetails;
        }

        /// <summary>
        /// Dictionary that holds each property and its individual validation exceptions.
        /// </summary>
        /// <remarks>
        /// * The key of the dictionary is the name of the supplied property. If none was provided while validating, the default key will be <see cref="String.Empty"/>
        /// * The value of the dictionary is a list of errors that occurred in the key property.
        /// </remarks>
        public Dictionary<string, IList<string>> ValidationDetails { get; set; }
    }
}
