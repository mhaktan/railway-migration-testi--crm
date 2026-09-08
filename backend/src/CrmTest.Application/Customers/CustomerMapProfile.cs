using AutoMapper;
using CrmTest.Entities;
using CrmTest.Customers.Dto;

namespace CrmTest.Customers
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}
