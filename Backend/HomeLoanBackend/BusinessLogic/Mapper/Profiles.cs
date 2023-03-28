using AutoMapper;
using BusinessLogic.DTO;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class Profiles:Profile
    { 
        public Profiles()
        {
            // source ( data coming from api ) ---> destination ( data going into database )
            //CreateMap<SourceLoan, LoanApplication>().ForMember(d => d.property, opt => opt.MapFrom(s => s.loanRequirementDTO));
            CreateMap<ApplyLoanDTO, Property>().ReverseMap();
            CreateMap<ApplyLoanDTO, PersonalIncome>().ReverseMap();
            CreateMap<ApplyLoanDTO, LoanRequirements>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<User, UserUpdatePasswordDTO>().ReverseMap();
            CreateMap<Advisor, LoginAdvisorDTO>().ReverseMap();
            CreateMap<ApplyLoanDTO, User>().ReverseMap();
            CreateMap<User, ApplyCollateralDTO>().ReverseMap();
            CreateMap<Collateral, ApplyCollateralDTO>().ReverseMap();
            CreateMap<User, EditCollateralDTO>().ReverseMap();
            CreateMap<Collateral, EditCollateralDTO>().ReverseMap();
        }
    }
}
