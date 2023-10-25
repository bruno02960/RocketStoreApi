using System.Threading.Tasks;
using RocketStoreApi.DTOs;

namespace RocketStoreApi.Managers
{
    /// <summary>
    /// Interface that defines the interaction with the PositionStack-API.
    /// </summary>
    public partial interface IPositionStackGateway
    {
        /// <summary>
        /// Gets the location of the customer given it's address.
        /// </summary>
        /// <param name="address"> Customer's address. </param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.
        /// The <see cref="Result{T}" /> that describes the result.
        /// The customer location.
        /// </returns>
        public Task<Result<LocationResultData>> SendForwardGeocodingRequestAsync(string address);
    }
}