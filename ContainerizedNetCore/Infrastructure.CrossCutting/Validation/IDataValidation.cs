using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Data Validation Facade and data container class.
    /// </summary>
    /// <remarks>
    /// This is better than model being validated by exceptions because it can generate a full one batch message.
    /// 
    /// EG: "Your e-mail was incorrect, the aircraft does not exist in our records, etc"
    /// </remarks>
    /// <example>
    /// Invoke the <see cref="AddValidationError(string)"/> method and in the end invoke the <see cref="Validate"/> message that
    /// will throw a DataValidationException if the current instance has at least one error message.
    /// 
    /// IDataValidation.AddValidationError("Error 1");
    /// IDataValidation.AddValidationError("Error 2");
    /// IDataValidation.AddValidationError("Error 3");
    /// IDataValidation.Validate();
    /// </example>
    public interface IDataValidation
    {
        /// <summary>
        /// Adds an error message to the current error message buffer.
        /// </summary>
        /// <param name="message">The validation error message.</param>
        /// <remarks>
        /// All messages added through this method will be buffered in a "Empty" property identifier on the final output.
        /// </remarks>
        void AddValidationError(string message);

        /// <summary>
        /// Adds an error message to the current error message buffer.
        /// </summary>
        /// <param name="property">The associated property that has thrown an exception, for tracing purposes.</param>
        /// <param name="message">The validation error message.</param>
        void AddValidationError(string property, string message);

        /// <summary>
        /// Validates the private list of messages and if errors were present throws a DataValidationException.
        /// </summary>
        void Validate();
    }
}
