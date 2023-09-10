using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Common.Db;
using test.Module.Entities;

namespace test.Service.Management.MedicalStaff
{
    public class MedicalStaffService: IMedicalStaffService
    {
        private readonly IMapper _mapper;
        public MedicalStaffService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 获取该站点所有工作人员信息
        /// </summary>
        /// <param name="hos_id"></param>
        /// <returns></returns>
        public List<workerinfo_dto> GetMedicalAllStaff(string hos_id)
        {
            List<workerinfo_dto> workerinfo_Dtos = new List<workerinfo_dto>();

            workerinfo_Dtos = DbContext.db.Queryable<worker_info>().Where(it => it.hos_id == hos_id).Select(x => new workerinfo_dto()
            {
                id = x.worker_id,
                phone_number = x.phone_number
            }).ToList();

            return workerinfo_Dtos;
        }
        /// <summary>
        /// 获取工作人员所在的医疗点id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<workerinfo_dto2> GetStaffMedicalPoint(string id)
        {
            List<workerinfo_dto2> workerinfo_Dto2s = new List<workerinfo_dto2>();
            workerinfo_Dto2s = DbContext.db.Queryable<worker_info>().Where(it => it.worker_id == id).Select(x => new workerinfo_dto2()
            {
                hos_id = x.hos_id,
                phone_number = x.phone_number
            }).ToList();
            return workerinfo_Dto2s;
        }
        /// <summary>
        /// 工作人员健康登记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int StaffPostHealth(string worker_id,string status, DateTime time)
        {
            worker_health_state worker_Health_State=new worker_health_state()
            {
                worker_id=worker_id,
                record_time = time,
                worker_state = status
            };
            return DbContext.db.Insertable<worker_health_state>(worker_Health_State).ExecuteCommand();
        }
    }
}
