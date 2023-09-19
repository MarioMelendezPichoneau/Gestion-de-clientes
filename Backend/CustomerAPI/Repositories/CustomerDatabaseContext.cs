using CustomerAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CustomerAPI.Repositories
{
    public class CustomerDatabaseContext: DbContext
    {

        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options):base(options)
        {
            
        }
        public DbSet<CustomerEntity> Customers { get; set; }

        public async Task<CustomerEntity?> Get(int id)
        {
            return await Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            CustomerEntity entity = await Get(id);
            Customers.Remove(entity);
            SaveChanges();

            return true;
        }

        public async Task<CustomerEntity> Add(CreateCustomerDtos customerDtos)
        {
            CustomerEntity entity = new CustomerEntity()
            {
                Id = null,
                Address = customerDtos.Address,
                Email = customerDtos.Email,
                FirstName = customerDtos.FirstName,
                LastName = customerDtos.LastName,
                Phone = customerDtos.Phone,
            };
           
            EntityEntry<CustomerEntity> response = await Customers.AddAsync(entity);

            await SaveChangesAsync();

            return await Get(response.Entity.Id ?? throw new Exception("no se ha podido guardar los datos"));
        }


        public async Task<bool> Actualizar(CustomerEntity customerEntity)
        {
            Customers.Update(customerEntity);
            await SaveChangesAsync();

            return true;
        }

    }

    public class CustomerEntity
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public CustomerDtos ToDtos()
        {
            return new CustomerDtos()
            {
                Address = Address,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Id = Id ?? throw new Exception("Id no puede ser null")
            };
        }
    }
}
