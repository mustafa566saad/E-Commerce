
namespace E_commerce.infrastructer.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoriesRepo CategoryRepo { get; }
        IProductRepo ProductRepo { get; }
        IPhotoRepo PhotoRepo { get; }
        Task SaveChangesAsync();
    }
}
