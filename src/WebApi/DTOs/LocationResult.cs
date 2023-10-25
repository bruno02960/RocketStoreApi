using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RocketStoreApi.DTOs
{
    /// <summary>
    /// Defines the forward geocoding response from PositionStack-API.
    /// </summary>
    public class LocationResult
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the latitude coordinate associated with the location result.
        /// </summary>
        [JsonPropertyName("data")]
        public List<LocationResultData> Data { get; set; }

        #endregion
    }
}