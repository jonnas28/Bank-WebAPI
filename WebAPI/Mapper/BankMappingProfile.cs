using AutoMapper;
using Bank.Common;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public class BankMappingProfile : Profile
    {
        public BankMappingProfile()
        {
            
            CreateMap<Account,AccountDTO>()
                .ReverseMap()
                .ForAllMembers(o => o.Condition((src, dest, value) => value != null));

            CreateMap<Transaction,TransactionDTO>()
                .ReverseMap()
                .ForAllMembers(o => o.Condition((src, dest, value) => value != null));
        }
    }
}
