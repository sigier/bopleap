using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BopLeap.Core.Attributes
{
    /// <summary>
    /// Specifies the InfluxDB measurement name for a model class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class InfluxMeasurementAttribute : Attribute
    {
        /// <summary>
        /// The name of the measurement in InfluxDB.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfluxMeasurementAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the InfluxDB measurement (e.g., "trades").</param>
        public InfluxMeasurementAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
