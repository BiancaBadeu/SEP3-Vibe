using Shared;
using Shared.DTOs;
using ShopApplication.DaoInterfaces;

namespace FileData.DAOs;


    public class ProductFileDao : IProductDao
    {
        private readonly FileContext context;

        public ProductFileDao(FileContext context)
        {
            this.context = context;
        }
        
        
        
        public async Task DeleteAsync(long id)
        {
            Product? existing = await GetByIdAsync(id);
            if (existing == null)
            {
                throw new Exception($"Todo with id {id} not found");
            }

            context.Products.Remove(existing);
            context.SaveChanges();
        }

        public Task AdminUpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }


        // might be used later 
      /*  public Task<Product> CreateAsync(Product product)
        {
            int productId = 1;
            if (context.Products.Any())
            {
                productId = context.Products.Max(p => p.Id);
                productId++;
            }

            product.Id = productId;

            context.Products.Add(product);
            context.SaveChanges();

            return Task.FromResult(products);
        }
        */


      
/*
      public Task<Product?> GetByNameAsync(string name)
        {
            Product? existing = context.products.FirstOrDefault(p =>
                p.name.Equals(name, StringComparison.OrdinalIgnoreCase)
            );
            return Task.FromResult(existing);
        }

       
    // Revise method
    
        public Task<List<Product>> GetProductsAsync()
        {
        
            IEnumerable<Product> products = context.products.AsEnumerable();

            List<Product> productList = new List<Product>();

            foreach (Product product in products)
            {
                productList.Add(product);
            }

            return Task.FromResult(productList);
        }
*/

        public Task<IEnumerable<Product>> GetAsync(SearchProductsParametersDto searchProductsParametersDto)
        {

            IEnumerable<Product> products = context.Products.AsEnumerable();

            if (searchProductsParametersDto.nameContains != null)
            {

                products = context.Products.Where(p => p.name.Contains(searchProductsParametersDto.nameContains, StringComparison.OrdinalIgnoreCase));
                
                
            }

            return Task.FromResult(products);
        }

        public Task<Product?> GetByIdAsync(long? id)
        {
            Product? existing = context.Products.FirstOrDefault(p =>
                p.id==id
            );
            return Task.FromResult(existing);
        }
    }
