﻿namespace RocketStoreApi.DTOs
{
    /// <summary>
    /// Defines a customer.
    /// </summary>
    public class CustomerDetails
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

        /// <summary>
        /// Gets or sets the customer vat number.
        /// </summary>
        public string VatNumber
        {
            get;
            set;
        }

        #endregion
    }
}
