using System.Data.Entity.Infrastructure;
using DotNetService.Exceptions;
using DotNetService.Infrastructure.Databases;

namespace DotNetService.Domain.Inventory.Repositories
{
    public class ProductStoreRepository
    {
        private readonly ProductDBContext _context;
        private readonly ProductQueryRepository _productQueryRepository;

        public ProductStoreRepository(
            ProductDBContext context,
            ProductQueryRepository productQueryRepository
        )
        {
            _context = context;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<Models.Product> Create(Models.Product request)
        {
            Models.Product newProduct = new Models.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };

            return await this.Save(newProduct);
        }

        public async Task<Models.Product> Update(Guid id, Models.Product request)
        {
            Models.Product product = await _productQueryRepository.Find(id);
            if (product == null)
            {
                return new Models.Product();
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            return await this.Save(product, true);
        }

        public async Task Delete(Guid id)
        {
            Models.Product data = await _productQueryRepository.FindOneById(id, true);

            _context.Products.Remove(data);
            int affectedRows = await _context.SaveChangesAsync();

            if (affectedRows == 0)
            {
                throw new UnprocessableEntityException("No data was deleted.");
            }
        }

        private async Task<Models.Product> Save(Models.Product data, bool isUpdate = false)
        {
            if (!isUpdate)
            {
                var dataCreated = _context.Products.Add(data);
                await _context.SaveChangesAsync();
                return dataCreated.Entity;
            }

            try
            {
                var dataUpdated = _context.Products.Update(data);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    throw new UnprocessableEntityException("No data was updated.");
                }

                return dataUpdated.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DataNotFoundException("Data with id " + data.Id + " not found.");
            }
        }
    }
}
