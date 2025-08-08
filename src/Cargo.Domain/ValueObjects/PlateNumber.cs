// ==================================================================================
// VALUE OBJECT: PlateNumber
// ==================================================================================
// Purpose: Represents a vehicle plate number as a value object
// This value object encapsulates plate number validation and formatting
// ==================================================================================

namespace Cargo.Domain.ValueObjects
{
    /// <summary>
    /// Represents a vehicle plate number as a value object
    /// This value object encapsulates plate number validation and formatting
    /// </summary>
    public class PlateNumber
    {
        /// <summary>
        /// Gets or sets the plate number value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the issuing state or country
        /// </summary>
        public string IssuingAuthority { get; set; }

        /// <summary>
        /// Gets or sets the plate type (standard, commercial, etc.)
        /// </summary>
        public string PlateType { get; set; }

        /// <summary>
        /// Initializes a new instance of the PlateNumber class
        /// </summary>
        public PlateNumber()
        {
            Value = string.Empty;
            IssuingAuthority = string.Empty;
            PlateType = "Standard";
        }

        /// <summary>
        /// Initializes a new instance of the PlateNumber class with specified values
        /// </summary>
        /// <param name="value">The plate number value</param>
        /// <param name="issuingAuthority">The issuing authority</param>
        /// <param name="plateType">The plate type</param>
        public PlateNumber(string value, string issuingAuthority, string plateType = "Standard")
        {
            Value = value;
            IssuingAuthority = issuingAuthority;
            PlateType = plateType;
        }

        /// <summary>
        /// Returns the formatted plate number
        /// </summary>
        /// <returns>The formatted plate number string</returns>
        public override string ToString()
        {
            return $"{Value} ({IssuingAuthority})";
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>True if the specified object is equal to the current object</returns>
        public override bool Equals(object obj)
        {
            if (obj is PlateNumber other)
            {
                return Value == other.Value && 
                       IssuingAuthority == other.IssuingAuthority && 
                       PlateType == other.PlateType;
            }
            return false;
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, IssuingAuthority, PlateType);
        }
    }
}
