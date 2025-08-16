using System;
using System.Collections.Generic;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a company that owns vehicles and employs drivers.
    /// </summary>
    public class Company : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the registration number of the company.
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets the company's physical address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the tax profile for this company.
        /// </summary>
        public TaxProfile TaxProfile { get; set; }

        /// <summary>
        /// Gets or sets the collection of drivers associated with this company.
        /// </summary>
        public virtual ICollection<Driver> Drivers { get; set; }

        /// <summary>
        /// Gets or sets the collection of vehicle ownerships associated with this company.
        /// </summary>
        public virtual ICollection<VehicleOwnership> Vehicles { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Company"/> class.
        /// </summary>
        public Company()
        {
            Drivers = new HashSet<Driver>();
            Vehicles = new HashSet<VehicleOwnership>();
            Address = new Address("", "", "", "", "");
            TaxProfile = TaxProfile.CreateQuebecProfile(); // Default to Quebec profile
        }
    }
}
