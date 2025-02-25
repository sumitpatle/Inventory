using AutoMapper;
using Domain.Entities;
using System.Globalization;

namespace Application.Upload
{
    public class InventoryItemProfile : Profile
    {
        public InventoryItemProfile()
        {
            CreateMap<InventoryItemDto, InventoryItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id if not in the CSV
        }
    }


}
