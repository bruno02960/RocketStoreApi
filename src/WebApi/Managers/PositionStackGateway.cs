using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RocketStoreApi.DTOs;

namespace RocketStoreApi.Managers
{
    /// <summary>
    /// Gateway for interaction with the PositionStack-API.
    /// </summary>
    public class PositionStackGateway : IPositionStackGateway
    {
        private ILogger Logger
        {
            get;
        }

        /// <inheritdoc />
        public async Task<Result<LocationResultData>> SendForwardGeocodingRequestAsync(string address)
        {
            HttpClient httpClient = new HttpClient();

            Uri uri = new Uri($"http://api.positionstack.com/v1/forward?access_key=391703115f999012be6c2e91a89496be&query={address}");
            HttpResponseMessage response = await httpClient.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                httpClient.Dispose();

                return Result<LocationResultData>.Success(JsonSerializer.Deserialize<DTOs.LocationResult>(content).Data[0]);
            }
            else
            {
                this.Logger.LogWarning($"Error requesting geolocation information from PositionStack-API.");

                httpClient.Dispose();

                return Result<LocationResultData>.Failure(
                    ErrorCodes.ErrorRequestingFromPositionStackAPI,
                    $"Error requesting geolocation information from PositionStack-API.");
            }
        }
    }
}
