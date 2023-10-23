using System;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace RocketStoreApi.Managers
{
    /// <summary>
    /// Defines the mapping profile used by the application.
    /// </summary>
    /// <seealso cref="Profile" />
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created via dependency injection.")]
    internal partial class MappingProfile : Profile
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            this.CreateMap<Models.Customer, Entities.Customer>()
                .ForMember(target => target.Email, opt => opt.MapFrom(source => source.EmailAddress))
                .AfterMap(
                    (source, target) =>
                    {
                        target.Id = Guid.NewGuid().ToString();
                    });

            this.CreateMap<Entities.Customer, DTOs.CustomerList>()
                .ForMember(target => target.EmailAddress, opt => opt.MapFrom(source => source.Email));

            this.CreateMap<Entities.Customer, DTOs.CustomerDetails>()
                .ForMember(target => target.EmailAddress, opt => opt.MapFrom(source => source.Email));
        }

        #endregion
    }
}
