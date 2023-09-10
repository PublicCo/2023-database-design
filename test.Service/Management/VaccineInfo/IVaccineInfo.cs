using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Module.Entities;

namespace test.Service.Management.VaccineInfo
{
    public interface IVaccineInfo
    {
        int InsertVaccineAppointRequest(appoint_insert_dto input);
        List<vaccine_appointment> GetVaccineAppointment();
        int UpdateVaccienAppointment(appoint_update_dto input);
        List<hos_vaccine_result_dto> GetHosVaccineInfo(string hos_id);
        int InsertHosVaccine(hos_vaccine_insert_dto input);
        int UpdateVaccineStatus(string vaccine_id, string vaccine_time);
        int InsertVaccinateRecord(string user_Id, string vaccine_Id);
        List<vaccinate_record> GetVaccinateRecord(string user_id);
        int UpddateVaccineSafeStatus(one_vaccine_info data);
        string InsertVaccineType(vaccine_info data);
        List<vaccine_info> GetVaccineType();
        List<apply_result_dto> GetAppointmentHistory(string hos_Id);
    }
}
