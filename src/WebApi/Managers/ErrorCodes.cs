namespace RocketStoreApi.Managers
{
    /// <summary>
    /// Defines constants that describe error codes.
    /// </summary>
    public static partial class ErrorCodes
    {
        #region Internal Constants

        /// <summary>
        /// The customer already exists.
        /// </summary>
        public const string CustomerAlreadyExists = "CustomerAlreadyExists";

        /// <summary>
        /// The customer does not exist.
        /// </summary>
        public const string InexistentCustomer = "InexistentCustomer";

        /// <summary>
        /// There was an error requesting information from PositionStack-API.
        /// </summary>
        public const string ErrorRequestingFromPositionStackAPI = "ErrorRequestingFromPositionStackAPI";

        #endregion
    }
}
