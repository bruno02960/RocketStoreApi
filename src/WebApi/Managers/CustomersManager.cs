using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RocketStoreApi.DTOs;
using RocketStoreApi.Storage;

namespace RocketStoreApi.Managers
{
    /// <summary>
    /// Defines the default implementation of <see cref="ICustomersManager"/>.
    /// </summary>
    /// <seealso cref="ICustomersManager" />
    [SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Created via dependency injection.")]
    internal partial class CustomersManager : ICustomersManager
    {
        #region Private Properties

        private ApplicationDbContext Context
        {
            get;
        }

        private IMapper Mapper
        {
            get;
        }

        private ILogger Logger
        {
            get;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersManager" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public CustomersManager(ApplicationDbContext context, IMapper mapper, ILogger<CustomersManager> logger)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.Logger = logger;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task<Result<Guid>> CreateCustomerAsync(Models.Customer customer, CancellationToken cancellationToken = default)
        {
            customer = customer ?? throw new ArgumentNullException(nameof(customer));

            Entities.Customer entity = this.Mapper.Map<Models.Customer, Entities.Customer>(customer);

            if (this.Context.Customers.Any(i => i.Email == entity.Email))
            {
                this.Logger.LogWarning($"A customer with email '{entity.Email}' already exists.");

                return Result<Guid>.Failure(
                    ErrorCodes.CustomerAlreadyExists,
                    $"A customer with email '{entity.Email}' already exists.");
            }

            this.Context.Customers.Add(entity);

            await this.Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            this.Logger.LogInformation($"Customer '{customer.Name}' created successfully.");

            return Result<Guid>.Success(
                new Guid(entity.Id));
        }

        /// <inheritdoc />
        public async Task<Result<List<DTOs.CustomerList>>> GetCustomersAsync(string nameFilter, string emailFilter)
        {
            List<Entities.Customer> customerEntities = await this.Context.Customers.ToListAsync().ConfigureAwait(false);

            List<DTOs.CustomerList> customers = this.Mapper.Map<List<Entities.Customer>, List<DTOs.CustomerList>>(customerEntities);

            if (nameFilter != null)
            {
                customers = customers.Where(x => x.Name.Contains(nameFilter, StringComparison.InvariantCulture)).ToList();
            }

            if (emailFilter != null)
            {
                customers = customers.Where(x => x.EmailAddress.Contains(emailFilter, StringComparison.InvariantCulture)).ToList();
            }

            this.Logger.LogInformation($"Customer list retrieved successfully.");

            return Result<List<DTOs.CustomerList>>.Success(customers);
        }

        /// <inheritdoc />
        public async Task<Result<CustomerDetails>> GetCustomerByIdAsync(string id)
        {
            Entities.Customer customerEntity = await this.Context.Customers.FindAsync(id).ConfigureAwait(false);

            if (customerEntity == null)
            {
                this.Logger.LogWarning($"Couldn't find any customer with id equal to '{id}'.");

                return Result<CustomerDetails>.Failure(
                    ErrorCodes.InexistentCustomer,
                    $"Couldn't find any customer with id equal to '{id}'.");
            }

            DTOs.CustomerDetails customer = this.Mapper.Map<Entities.Customer, DTOs.CustomerDetails>(customerEntity);

            HttpClient httpClient = new HttpClient();

            Uri uri = new Uri($"http://api.positionstack.com/v1/forward?access_key=391703115f999012be6c2e91a89496be&query={customer.Address}");
            HttpResponseMessage response = await httpClient.GetAsync(uri).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                customer.Location = JsonSerializer.Deserialize<DTOs.LocationResult>(content).Data[0];

                httpClient.Dispose();
            }
            else
            {
                this.Logger.LogWarning($"Error requesting geolocation information from PositionStack-API.");

                httpClient.Dispose();

                return Result<CustomerDetails>.Failure(
                    ErrorCodes.ErrorRequestingFromPositionStackAPI,
                    $"Error requesting geolocation information from PositionStack-API.");
            }

            this.Logger.LogInformation($"Customer list retrieved successfully.");

            return Result<DTOs.CustomerDetails>.Success(customer);
        }

        /// <inheritdoc />
        public async Task<Result<string>> DeleteCustomerByIdAsync(string id)
        {
            Entities.Customer customerEntity = await this.Context.Customers.FindAsync(id).ConfigureAwait(false);

            if (customerEntity == null)
            {
                this.Logger.LogWarning($"Couldn't find any customer with id equal to '{id}'.");

                return Result<string>.Failure(
                    ErrorCodes.InexistentCustomer,
                    $"Couldn't find any customer with id equal to '{id}'.");
            }

            this.Context.Customers.Remove(customerEntity);

            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            this.Logger.LogInformation($"Customer successfully deleted.");

            return Result<string>.Success(id);
        }

        #endregion
    }
}
