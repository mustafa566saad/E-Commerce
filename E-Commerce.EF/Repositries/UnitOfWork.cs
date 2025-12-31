using AutoMapper;
using E_commerce.Core.Services;
using E_commerce.infrastructer.Interfaces;
using E_Commerce.EF.Data;

namespace E_Commerce.EF.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMapper mapper;
        public IImageServices imageServices;
        public ICategoriesRepo CategoryRepo { get; }

        public IProductRepo ProductRepo { get; }

        public IPhotoRepo PhotoRepo { get; }

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IMapper mapper, IImageServices imageServices)
        {
            _context = context;
            this.mapper = mapper;
            this.imageServices = imageServices;

            CategoryRepo = new CategoriesRepo(_context);
            ProductRepo = new ProductRepo(_context, mapper, imageServices);
            PhotoRepo = new PhotoRepo(_context);
        }

        public async Task SaveChangesAsync()
        =>await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
