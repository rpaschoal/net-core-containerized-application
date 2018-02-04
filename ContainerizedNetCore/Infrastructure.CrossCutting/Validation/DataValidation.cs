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
    public sealed class DataValidation : IDataValidation
    {
        /// <summary>
        /// Error messages buffer.
        /// </summary>
        private Dictionary<string, IList<string>> Errors = new Dictionary<string, IList<string>>();

        /// <summary>
        /// Adds an error message to the current error message buffer.
        /// </summary>
        /// <param name="message">The validation error message.</param>
        /// <remarks>
        /// All messages added through this method will be buffered in a "Empty" property identifier on the final output.
        /// </remarks>
        public void AddValidationError(string message)
        {
            this.AddValidationError(string.Empty, message);
        }

        /// <summary>
        /// Adds an error message to the current error message buffer.
        /// </summary>
        /// <param name="property">The associated property that has thrown an exception, for tracing purposes.</param>
        /// <param name="message">The validation error message.</param>
        public void AddValidationError(string property, string message)
        {
            if (this.Errors.ContainsKey(property))
            {

                this.Errors[property].Add(message.TrimEnd('.'));
            }
            else
            {
                this.Errors.Add(property, new List<string>() { message.TrimEnd('.') });
            }
        }

        /// <summary>
        /// Validates the private list of messages and if errors were present throws a DataValidationException.
        /// </summary>
        public void Validate()
        {
            if (this.Errors.Any())
            {
                var summaryMessage = string.Empty;
                bool firstExecution = true;

                foreach (var errorKey in Errors)
                {
                    foreach (var message in errorKey.Value)
                    {
                        summaryMessage += !firstExecution ? ", " + message : message;
                        firstExecution = false;
                    }
                }

                if (!String.IsNullOrEmpty(summaryMessage))
                {
                    // Adds final dot to message since all final dots were previously removed.
                    throw new DataValidationException(summaryMessage + ".", Errors);
                }
            }
        }
    }
}
