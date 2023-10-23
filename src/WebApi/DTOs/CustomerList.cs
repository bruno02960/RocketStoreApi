namespace RocketStoreApi.DTOs
{
    /// <summary>
    /// Defines a customer.
    /// </summary>
    public class CustomerList
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the customer email address.
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        #endregion
    }
}
