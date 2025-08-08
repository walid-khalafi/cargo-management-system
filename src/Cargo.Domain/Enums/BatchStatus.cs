namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Represents the status of a driver batch.
    /// </summary>
    public enum BatchStatus
    {
        /// <summary>
        /// The batch is in draft state and can be modified.
        /// </summary>
        Draft,

        /// <summary>
        /// The batch has been finalized and is ready for approval.
        /// </summary>
        Finalized,

        /// <summary>
        /// The batch has been approved and is ready for payment.
        /// </summary>
        Approved,

        /// <summary>
        /// The batch has been paid and is closed.
        /// </summary>
        Paid
    }
}
