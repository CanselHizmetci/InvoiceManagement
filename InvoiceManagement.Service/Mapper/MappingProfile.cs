using AutoMapper;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.DTOs;

namespace InvoiceManagement.Service.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Apartment, ApartmentDTO>().ReverseMap();
            CreateMap<ApartmentType, ApartmentTypeDTO>().ReverseMap();
            CreateMap<Block, BlockDTO>().ReverseMap();
            CreateMap<Debt, DebtDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<InvoiceType, InvoiceTypeDTO>().ReverseMap();
            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
