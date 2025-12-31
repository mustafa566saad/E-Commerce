using AutoMapper;
using E_commerce.Core.DTOs;
using E_commerce.infrastructer.Entities;

namespace E_Commerce.Mapping
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<ProductDTO, Products>().ForMember(e => e.Photos, op => op.Ignore());
        }
    }
}
