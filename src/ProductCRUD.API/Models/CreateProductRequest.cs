namespace ProductCRUD.API.Models
{
    public sealed record CreateProductRequest(string name, string description, decimal price);
}
