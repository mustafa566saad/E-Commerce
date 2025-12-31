using E_commerce.infrastructer.Entities;
using E_commerce.infrastructer.Interfaces;
using E_Commerce.EF.Data;

namespace E_Commerce.EF.Repositries
{
    public class PhotoRepo:BaseRepo<Photos>, IPhotoRepo
    {
        public PhotoRepo(AppDbContext context) : base(context)
        {
        }
    }
}
