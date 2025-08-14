using Cargo.Application.DTOs.Common;
using Cargo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.DTOs.DriverContracts
{
    public class DriverSettingsDto
    {
        /// <summary>
        /// Number of pay bands used for rate calculations.
        /// </summary>
        public int NumPayBands { get; set; }

        /// <summary>
        /// Hourly rate for time-based pay calculations.
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Fuel surcharge rate.
        /// </summary>
        public decimal FscRate { get; set; }

        /// <summary>
        /// Fuel surcharge calculation mode (per mile or percentage).
        /// </summary>
        public FscMode FscMode { get; set; }

        /// <summary>
        /// Waiting time rate per minute.
        /// </summary>
        public decimal WaitingPerMinute { get; set; }

        /// <summary>
        /// Administrative fee percentage or flat amount.
        /// </summary>
        public decimal AdminFee { get; set; }

        /// <summary>
        /// Province for tax calculation purposes.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Tax profile details for this driver.
        /// </summary>
        public TaxProfileDto TaxProfile { get; set; }
    }
}
