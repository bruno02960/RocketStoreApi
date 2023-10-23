using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RocketStoreApi.DTOs;

namespace RocketStoreApi.Managers
{
    /// <summary>
    /// Defines the interface of the customers manager.
    /// The customers manager allows retrieving, creating, and deleting customers.
    /// </summary>
    public partial interface ICustomersManager
    {
        #region Methods

        /// <summary>
        /// Creates the specified customer.
        /// </summary>
        /// <param name="customer">The customer that should be created.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" /> that represents the asynchronous operation.
        /// The <see cref="Result{T}" /> that describes the result.
        /// The new customer identifier.
        /// </returns>
        Task<Result<Guid>> CreateCustomerAsync(Models.Customer customer, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete an existing customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" /> that represents the asynchronous operation.
        /// The <see cref="Result{T}" /> that describes the result.
        /// The deleted customer identifier.
        /// </returns>
        Task<Result<string>> DeleteCustomerByIdAsync(string id);

        /// <summary>
        /// Retrieve an existing customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" /> that represents the asynchronous operation.
        /// The <see cref="Result{T}" /> that describes the result.
        /// The customer with the given identifier.
        /// </returns>
        Task<Result<CustomerDetails>> GetCustomerByIdAsync(string id);

        /// <summary>
        /// Retrieve a list of existing customers.
        /// </summary>
        /// <param name="nameFilter">Name by which customers should be filtered.</param>
        /// <param name="emailFilter">Email address by which customers should be filtered.</param>
        /// <returns>
        /// The <see cref="Task{TResult}" /> that represents the asynchronous operation.
        /// The <see cref="Result{T}" /> that describes the result.
        /// The list of existing customers.
        /// </returns>
        Task<Result<List<DTOs.CustomerList>>> GetCustomersAsync(string nameFilter, string emailFilter);

        #endregion
    }
}
