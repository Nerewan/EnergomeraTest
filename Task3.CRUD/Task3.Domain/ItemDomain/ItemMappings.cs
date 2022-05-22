using AutoMapper;
using Task3.Model.DTOs.Requests.Item;
using Task3.Model.DTOs.Responses.Item;
using Task3.Model.Entities;

namespace Task3.Domain.ItemDomain
{
    public class ItemMappings : Profile
    {
        public ItemMappings()
        {
            CreateMap<Item, ItemResponseBase>()
                    .IncludeAllDerived();

            CreateMap<CreateItemRequest, Item>()
                    .IncludeAllDerived();

            CreateMap<UpdateItemRequest, Item>()
                    .IncludeAllDerived();
        }
    }
}
