using AutoMapper;
using BudgetService.API.Models;
using BudgetService.API.DTOs;

namespace BudgetService.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<BudgetCreateRequest, Budget>();
            CreateMap<Budget, BudgetResponse>();

            CreateMap<BudgetCategoryDto, BudgetCategory>();
            CreateMap<BudgetCategory, BudgetCategoryDto>();

            CreateMap<IncomeDto, Income>();
            CreateMap<Income, IncomeDto>();
        }
    }
}
