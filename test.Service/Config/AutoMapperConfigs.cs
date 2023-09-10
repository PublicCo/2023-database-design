using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using test.Module.Entities;
using test.Service.Management.NuclearTest;
using test.Service.Management.Reservevation;
using test.Service.Management.VaccineInfo;
using test.Service.User.Dto;

namespace test.Service.Config
{
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            //登录
            CreateMap<InputUserDto, user_info>();

            //核酸检测映射
            CreateMap<nuclear_test_info_dto, nuclear_test_info>();
            CreateMap<nuclear_detection_dto, nuclear_test_info>();

            //医疗点申请疫苗
            CreateMap<appoint_insert_dto, vaccine_appointment>();
            CreateMap<appoint_update_dto, vaccine_appointment>();
            CreateMap<hos_vaccine_dto, hos_vaccine_result_dto>();
            CreateMap<hos_vaccine_insert_dto, one_vaccine_info>();

            //预约信息
            CreateMap<appointment_info_dto, appointment_result_dto>();
        }
    }
}
