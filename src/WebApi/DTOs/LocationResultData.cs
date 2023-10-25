﻿using System.Text.Json.Serialization;

namespace RocketStoreApi.DTOs
{
    /// <summary>
    /// Defines a location, as returned from the PositionStack-API.
    /// </summary>
    public class LocationResultData
    {
        /// <summary>
        /// Gets or sets the latitude coordinate associated with the location result.
        /// </summary>
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate associated with the location result.
        /// </summary>
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the type of location result
        /// (venue, address, street, neighbourhood, borough, localadmin, locality,
        /// county, macrocounty, region, macroregion, country, coarse, postalcode).
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the location result. This could be a place name,
        /// address, postal code, and more.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the street or house number associated with the location result.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the postal or ZIP code associated with the location result.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the street name associated with the location result.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the confidence score between 0 (0% confidence) and 1 (100%
        /// confidence) associated with the location result.
        /// </summary>
        [JsonPropertyName("confidence")]
        public int Confidence { get; set; }

        /// <summary>
        /// Gets or sets the name of the region associated with the location result.
        /// </summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the region code associated with the location result.
        /// </summary>
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        /// <summary>
        /// Gets or sets the county associated with the location result.
        /// </summary>
        [JsonPropertyName("county")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the locality associated with the location result.
        /// </summary>
        [JsonPropertyName("locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the name of the administrative area associated with the
        /// location result.
        /// </summary>
        [JsonPropertyName("administrative_area")]
        public object AdministrativeArea { get; set; }

        /// <summary>
        /// Gets or sets the name of the neighbourhood associated with the location
        /// result.
        /// </summary>
        [JsonPropertyName("neighbourhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the common name of the country associated with the location
        /// result.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166 Alpha 2 (two letters) code of the country
        /// associated with the location result.
        /// </summary>
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the continent associated with the location result.
        /// </summary>
        [JsonPropertyName("continent")]
        public string Continent { get; set; }

        /// <summary>
        /// Gets or sets the formatted place name or address.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
