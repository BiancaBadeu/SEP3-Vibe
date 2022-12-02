using Shared;
using Shared.DTOs;
using ShopApplication.DaoInterfaces;
using ShopApplication.LogicInterfaces;

namespace ShopApplication.Logic;

public class ProductLogic : IProductLogic
{
    private readonly IProductDao productDao;

    public ProductLogic(IProductDao productDao)
    {
        this.productDao = productDao;
    }
    
    public async Task DeleteAsync(long id)
    {
        Product? product = await productDao.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception($"Product with ID {id} was not found!");
        }


        await productDao.DeleteAsync(id);
    }

    


    /* Not relevant for this requirement but might prove useful
    private static void ValidateData(ProductCreationDto productoToCreate)
    {
        string name = productToCreate.name;
        string description = productToCreate.description;

        if (name.Length < 3)
            throw new Exception("name must be at least 3 characters!");

        if (name.Length > 50)
            throw new Exception("name must be less than 50 characters!");

        if (description.Length < 3)
            throw new Exception("description must be at least 3 characters!");

        if (description.Length > 150)
            throw new Exception("description must be less than 150 characters!");
    }
    */
/*
    public Task<Product?> GetByNameAsync(string name)
    {
        return productDao.GetByNameAsync(name);
    }

    public Task<List<Product>> GetProductsAsync()
    {

        return productDao.GetProductsAsync();
    }
*/
    public Task<IEnumerable<Product>> GetAsync(SearchProductsParametersDto searchProductsParametersDto)
    {

        return productDao.GetAsync(searchProductsParametersDto);
    }

    public async Task<ProductCreationDto> GetByIdAsync(long id)
    {
        Product? product = await productDao.GetByIdAsync(id);
        if (product == null)
        {
            throw new Exception(
                $"Product with id {id} not found!");
        }

        return new ProductCreationDto( product.name, product.description, product.price, product.stock, product.image, product.ingredients);
    }
    
    public async Task AdminUpdateAsync(ProductAdminUpdateDto dto)
    {
        Product? existing = await productDao.GetByIdAsync(dto.id);

        if (existing == null)
        {
            throw new Exception($"Product with ID {dto.id} not found!");
        }
        

        string nameToUse = dto.name ?? existing.name;
        string descriptionToUse = dto.description ?? existing.description;
        double priceToUse = dto.price ?? existing.price;
        string imageToUse = dto.image ?? existing.image;
        string ingredientsToUse = dto.ingredients ?? existing.ingredients;
    
        Product updated = new (nameToUse, descriptionToUse, priceToUse, imageToUse, ingredientsToUse )
        {
            id = existing.id,
            stock = existing.stock,
        };

        ValidateProduct(updated);

        await productDao.AdminUpdateAsync(updated);    }
    
    private void ValidateProduct(Product dto)
    {
        if (string.IsNullOrEmpty(dto.name)) throw new Exception("Name cannot be empty.");
        if (string.IsNullOrEmpty(dto.description)) throw new Exception("Description cannot be empty.");
        if (string.IsNullOrEmpty(dto.ingredients)) throw new Exception("Ingredients cannot be empty.");
        if (string.IsNullOrEmpty(dto.image)) throw new Exception("Image cannot be empty.");
        if (dto.price<=0) throw new Exception("Price cannot be lower or equal to 0.");
    }
}