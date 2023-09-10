using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NetTaste;
using SqlSugar;
using test.Common.Db;
using test.Module.Entities;
using test.Service.Management.UserHealthState;
using test.Service.Token;

namespace test.Service.Management.VaccineInfo
{
    public class VaccineInfo : IVaccineInfo
    {
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly IUserHealthState _userHealthState;
        public VaccineInfo(IMapper mapper, IJWTService jwtService, IUserHealthState userHealthState)
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _userHealthState = userHealthState;
        }
        public int InsertVaccineAppointRequest(appoint_insert_dto input)
        {
            vaccine_appointment data = TransInputDto(input);
            if(!DbContext.db.Queryable<hospital_info>().Any(it=>it.hos_id.Equals(data.hos_id)))
            {
                throw new Exception("未查询到此医疗点");
            }

            return DbContext.db.Insertable(data).ExecuteCommand();
        }
        public vaccine_appointment TransInputDto(appoint_insert_dto input)
        {
            vaccine_appointment data = _mapper.Map<vaccine_appointment>(input);
            data.appoint_time = DateTime.Now;
            data.appoint_state = "未处理";
            return data;
        }
        public vaccine_appointment TransInputDto(appoint_update_dto input)
        {
            vaccine_appointment data = _mapper.Map<vaccine_appointment>(input);
            return data;
        }
        public List<vaccine_appointment> GetVaccineAppointment()
        {
            return DbContext.db.Queryable<vaccine_appointment>().Where(it => it.appoint_state == "未处理").ToList();
        }
        public int UpdateVaccienAppointment(appoint_update_dto input)
        {
            vaccine_appointment data = TransInputDto(input);
            return DbContext.db.Updateable(data).UpdateColumns(it => it.appoint_state).Where(it => it.hos_id == data.hos_id).ExecuteCommand();
        }
        public List<hos_vaccine_result_dto>GetHosVaccineInfo(string hos_id)
        {
            var hos_info=DbContext.db.Queryable<one_vaccine_info,vaccine_info>((sv,vt)=>new JoinQueryInfos(
                JoinType.Left,sv.vaccine_type_id==vt.type_id
                ))
                .Where((sv,vt)=>sv.medical_point_id==hos_id)
                .Select<hos_vaccine_dto>().ToList();

            List<hos_vaccine_result_dto> result = _mapper.Map<List<hos_vaccine_result_dto>>(hos_info);
            for(int i=0;i<result.Count;i++)
            {
                result[i].production_date = hos_info[i].production_date.Value.ToShortDateString();
            }
            return result;
        }
        public int InsertHosVaccine(hos_vaccine_insert_dto input)
        {
            if (!DbContext.db.Queryable<hospital_info>().Any(it => it.hos_id.Equals(input.medical_point_id)))
            {
                throw new Exception("未查询到此医疗点");
            }
            if (!DbContext.db.Queryable<vaccine_info>().Any(it => it.type_id.Equals(input.vaccine_type_id)))
            {
                throw new Exception("无此疫苗种类");
            }

            one_vaccine_info data = TransInputDto(input);
            return DbContext.db.Insertable(data).ExecuteCommand();
        }
        public one_vaccine_info TransInputDto(hos_vaccine_insert_dto input)
        {
            one_vaccine_info data = _mapper.Map<one_vaccine_info>(input);
            data.production_date = Convert.ToDateTime(input.production_date);
            data.vaccinated = 0;
            data.vaccinated_time = null;
            return data;
        }
        public int UpdateVaccineStatus(string vaccine_id,string vaccine_time)
        {
            one_vaccine_info data=new one_vaccine_info();
            data.vaccine_id = vaccine_id;
            data.vaccinated = 1;
            data.vaccinated_time = Convert.ToDateTime(vaccine_time);
            return DbContext.db.Updateable(data).UpdateColumns(it => new { it.vaccinated, it.vaccinated_time }).Where(it => it.vaccine_id == vaccine_id).ExecuteCommand();
        }
        public int InsertVaccinateRecord(string user_Id,string vaccine_Id)
        {
            if (!DbContext.db.Queryable<user_info>().Any(m => m.user_id.Equals(user_Id)))
            {
                throw new Exception("无此用户");
            }
            if (!DbContext.db.Queryable<one_vaccine_info>().Any(m => m.vaccine_id.Equals(vaccine_Id)))
            {
                throw new Exception("无此疫苗");
            }
            get_vaccine_info data= new get_vaccine_info();
            data.user_id = user_Id;
            data.vaccin_id = vaccine_Id;
            data.vaccinate_time = DateTime.Now;
            data.vaccinate_times = _userHealthState.UpdateVaccinationStatus(user_Id);
            return DbContext.db.Insertable(data).ExecuteCommand();
        }
        public List<vaccinate_record> GetVaccinateRecord(string user_id)
        {
            List<vaccinate_record>result = new List<vaccinate_record>();
            var records = DbContext.db.Queryable<get_vaccine_info, one_vaccine_info, vaccine_info>((gv, vi, vt) => new JoinQueryInfos(
                JoinType.Left, gv.vaccin_id == vi.vaccine_id,
                JoinType.Left, vi.vaccine_type_id == vt.type_id
                ))
                .Where((gv,vi,vt)=>gv.user_id == user_id)
                .Select((gv, vi, vt) => new { vaccin_id = gv.vaccin_id, vaccinate_time = gv.vaccinate_time, vaccinate_times = gv.vaccinate_times, manufacture = vt.manufacture, type = vt.type_description }).ToList();
            foreach(var record in records)
            {
                vaccinate_record data = new vaccinate_record();
                data.manufacture = record.manufacture;
                data.type = record.type;
                data.vaccin_id = record.vaccin_id;
                data.vaccinate_time = record.vaccinate_time.Value.ToString();
                data.vaccinate_times = record.vaccinate_times;
                result.Add(data);
            }
            return result;
        }
        public int UpddateVaccineSafeStatus(one_vaccine_info data)
        {
            return DbContext.db.Updateable(data).UpdateColumns(it => new { it.is_safe }).Where(it => it.vaccine_id == data.vaccine_id).ExecuteCommand();
        }
        public string InsertVaccineType(vaccine_info data)
        {
            if(DbContext.db.Queryable<vaccine_info>().Any(it=>it.Equals(data.type_id)))
            {
                throw new Exception("此疫苗种类id已经存在");
            }
            int result = DbContext.db.Insertable(data).ExecuteCommand();
            if (result == 1)
                return data.type_id;
            throw new Exception("插入出错");
        }
        public List<vaccine_info> GetVaccineType()
        {
            return DbContext.db.Queryable<vaccine_info>().ToList();
        }
        public List<apply_result_dto> GetAppointmentHistory(string hos_Id)
        {
            if (!DbContext.db.Queryable<hospital_info>().Any(it => it.hos_id.Equals(hos_Id)))
            {
                throw new Exception("未查询到此医疗点");
            }
            List<apply_result_dto> list_result = DbContext.db.Queryable<vaccine_appointment>().Where(it => it.hos_id == hos_Id).Select<apply_result_dto>().ToList();
            return list_result;
        }
    }
}
