using System;
using System.Collections.Generic;
using System.Text;

namespace FilExile
{
    /// <summary>
    /// An attribute to indicate reusability/maturity of a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum)]
    public class ReusableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReusableAttribute"/> class.
        /// </summary>
        /// <param name="category">The category of reusability.</param>
        /// <param name="description">The description of the reusable type.</param>
        public ReusableAttribute(ReusableCategory category, string description)
        {
            Category = category;
            Description = description;
        }

        /// <summary>
        /// Category of the class.
        /// </summary>
        public ReusableCategory Category { get; private set; }

        /// <summary>
        ///   Short description of the reusable class.
        /// </summary>
        public string Description { get; private set; }
    }
}
