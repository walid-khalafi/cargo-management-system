namespace Cargo.Domain.Enums
{
    /// <summary>
    /// Enum representing types of wait in driver batch statements.
    /// </summary>
    public enum WaitType
    {
        /// <summary>
        /// Represents waiting time due to customer account.
        /// </summary>
        CustomerAccWait,

        /// <summary>
        /// Represents waiting time due to terminal account.
        /// </summary>
        TerminalAccWait,

        /// <summary>
        /// Represents other types of waiting time not categorized.
        /// </summary>
        Other
    }
}
