using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RocketStoreApi.DTOs;
using RocketStoreApi.Managers;
using RocketStoreApi.Models;

namespace RocketStoreApi.Controllers
{
    /// <summary>
    /// Defines the customers controller.
    /// This controller provides actions on customers.
    /// </summary>
    [ControllerName("Customers")]
    [ApiController]
    public partial class CustomersController : ControllerBase
    {
        // Ignore Spelling: api

        #region Public Methods

        /// <summary>
        /// Creates the specified customer.
        /// </summary>
        /// <param name="customer">The customer that should be created.</param>
        /// <returns>
        /// The new customer identifier.
        /// </returns>
        [HttpPost("api/customers")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCustomerAsync(Customer customer)
        {
            Result<Guid> result = await this.HttpContext.RequestServices.GetRequiredService<ICustomersManager>()
                .CreateCustomerAsync(customer).ConfigureAwait(false);

            if (result.FailedWith(ErrorCodes.CustomerAlreadyExists))
            {
                return this.Conflict(
                    new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.Conflict,
                        Title = result.ErrorCode,
                        Detail = result.ErrorDescription
                    });
            }
            else if (result.Failed)
            {
                return this.BadRequest(
                    new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = result.ErrorCode,
                        Detail = result.ErrorDescription
                    });
            }

            return this.Created(
                this.GetUri("customers", result.Value),
                result.Value);
        }

        /// <summary>
        /// Retrieve a list of existing customers.
        /// </summary>
        /// <param name="nameFilter">Name by which customers should be filtered.</param>
        /// <param name="emailFilter">Email address by which customers should be filtered.</param>
        /// <returns>
        /// The list of existing customers.
        /// </returns>
        [HttpGet("api/customers")]
        [ProducesResponseType(typeof(List<DTOs.CustomerList>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomersAsync([FromQuery] string nameFilter = null, [FromQuery] string emailFilter = null)
        {
            Result<List<DTOs.CustomerList>> result = await this.HttpContext.RequestServices.GetRequiredService<ICustomersManager>()
                .GetCustomersAsync(nameFilter, emailFilter).ConfigureAwait(false);

            if (result.Failed)
            {
                return this.BadRequest(
                    new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = result.ErrorCode,
                        Detail = result.ErrorDescription
                    });
            }

            return this.Ok(result.Value);
        }

        /// <summary>
        /// Retrieve an existing customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>
        /// The customer with the given identifier.
        /// </returns>
        [HttpGet("/api/customers/{id}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomerDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] string id)
        {
            Result<DTOs.CustomerDetails> result = await this.HttpContext.RequestServices.GetRequiredService<ICustomersManager>()
                .GetCustomerByIdAsync(id).ConfigureAwait(false);

            if (result.FailedWith(ErrorCodes.InexistentCustomer))
            {
                return this.NotFound(
                    new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Title = result.ErrorCode,
                        Detail = result.ErrorDescription
                    });
            }

            return this.Ok(result.Value);
        }

        /// <summary>
        /// Delete an existing customer by its identifier.
        /// </summary>
        /// <param name="id">Customer identifier.</param>
        /// <returns>
        /// The deleted customer identifier.
        /// </returns>
        [HttpDelete("/api/customers/{id}")]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomerDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomerByIdAsync([FromRoute] string id)
        {
            Result<string> result = await this.HttpContext.RequestServices.GetRequiredService<ICustomersManager>()
                .DeleteCustomerByIdAsync(id).ConfigureAwait(false);

            if (result.FailedWith(ErrorCodes.InexistentCustomer))
            {
                return this.NotFound(
                    new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Title = result.ErrorCode,
                        Detail = result.ErrorDescription
                    });
            }

            return this.Ok(result.Value);
        }

        #endregion

        #region Private Methods

        private Uri GetUri(params object[] parameters)
        {
            string result = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            foreach (object pathParam in parameters)
            {
                result = $"{result}/{pathParam}";
            }

            return new Uri(result);
        }

        #endregion
    }
}
