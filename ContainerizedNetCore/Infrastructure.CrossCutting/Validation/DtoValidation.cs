using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Implement this interface on a DTO model to expose its own data validation for required properties.
    /// </summary>
    public abstract class DtoValidation
    {
        protected IDataValidation _DataValidation;

        public DtoValidation(IDataValidation DataValidation)
        {
            _DataValidation = DataValidation;
        }

        /// <summary>
        /// Implemented on DTO level to check property by property.
        /// </summary>
        protected abstract void ApplyValidation();

        /// <summary>
        /// Validates current dto model that represents a request payload.
        /// </summary>
        /// <remarks>
        /// Validation errors should be flushed by <see cref="IDataValidation"/>. 
        /// </remarks>
        public void ValidateModel()
        {
            this.ApplyValidation();
            this._DataValidation.Validate();
        }
    }
}
