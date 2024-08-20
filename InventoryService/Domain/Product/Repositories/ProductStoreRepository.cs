using System.Data.Entity.Infrastructure;
using DotNetService.Exceptions;
using DotNetService.Infrastructure.Databases;

namespace DotNetService.Domain.Product.Repositories
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

        public Models.Product Create(Models.Product request)
        {
            Models.Product newProduct = new Models.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };

            return this.Save(newProduct);
        }

        public Models.Product Update(Guid id, Models.Product request)
        {
            Models.Product product = _productQueryRepository.Find(id);
            if (product == null)
            {
                return new Models.Product();
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            return this.Save(product, true);
        }

        public void Delete(Guid id)
        {
            Models.Product data = _productQueryRepository.FindOneById(id, true);

            _context.Products.Remove(data);
            int affectedRows = _context.SaveChanges();

            if (affectedRows == 0)
            {
                throw new UnprocessableEntityException("No data was deleted.");
            }
        }

        private Models.Product Save(Models.Product data, bool isUpdate = false)
        {
            if (!isUpdate)
            {
                var dataCreated = _context.Products.Add(data);
                _context.SaveChanges();
                return dataCreated.Entity;
            }

            try
            {
                var dataUpdated = _context.Products.Update(data);
                int affectedRows = _context.SaveChanges();

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
