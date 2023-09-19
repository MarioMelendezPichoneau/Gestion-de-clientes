using CustomerAPI.Dtos;
using CustomerAPI.Repositories;

namespace CustomerAPI.CasosDeUsos
{
    public interface UpdateCustomerUserCas
    {
        Task<CustomerDtos?> Execute(Dtos.CustomerDtos customerDtos);
    }
    public class UpdateCustomerUserCase : UpdateCustomerUserCas
    {
        private readonly CustomerDatabaseContext _databaseContext;
        

        public UpdateCustomerUserCase(CustomerDatabaseContext customerDatabaseContext)
        {
            _databaseContext = customerDatabaseContext;
        }


        public async Task<CustomerDtos?> Execute(Dtos.CustomerDtos customer)
        {
            var entity = await _databaseContext.Get(customer.Id);

            if (entity == null)
                return null;

            entity.FirstName = customer.FirstName;
            entity.LastName = customer.LastName;
            entity.Email = customer.Email;
            entity.Phone = customer.Phone;
            entity.Address = customer.Address;

            await _databaseContext.Actualizar(entity);

            return entity.ToDtos();
        }
    }
}
