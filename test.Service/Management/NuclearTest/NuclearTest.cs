using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SqlSugar;
using test.Common.Db;
using test.Module.Entities;

namespace test.Service.Management.NuclearTest
{
    public class NuclearTest : INuclearTest
    {
        private readonly IMapper _mapper;
        public NuclearTest(IMapper mapper)
        {
            _mapper = mapper;
        }
        public nuclear_test_info Insert(nuclear_test_info_dto input)
        {
            string exp = CheckUID(input.user_id);
            if (exp == String.Empty)
            {
                nuclear_test_info test = TransInputDto(input);
                DbContext.db.Insertable(test).IgnoreColumns("test_result").ExecuteCommand();
                return test;
            }
            else
                throw new Exception(exp);
        }
        private nuclear_test_info TransInputDto(nuclear_test_info_dto input)
        {
            var test = _mapper.Map<nuclear_test_info>(input);
            test.test_time = DateTime.Now;
            return test;
        }
        private nuclear_test_info TransInputDto(nuclear_detection_dto input)
        {
            var test=_mapper.Map<nuclear_test_info>(input);
            return test;
        }
        private string Check(nuclear_test_info_dto input)
        {
            if(CheckUID(input.user_id)!=String.Empty)
            {
                return "用户不存在";
            }
            if(CheckPID(input.test_tube_ID)!=String.Empty)
            {
                return "此试管不存在";
            }
            return String.Empty;
        }
        private string CheckUID(string input)
        {
            if (!DbContext.db.Queryable<user_info>().Any(m => m.user_id.Equals(input)))
            {
                return "用户不存在";
            }
            return String.Empty;
        }
        private string CheckPID(string input)
        {
            if (!DbContext.db.Queryable<nuclear_test_info>().Any(m => m.test_tube_ID.Equals(input)))
            {
                return "此试管不存在";
            }
            return String.Empty;
        }
        public string UpdateDetectionResult(nuclear_detection_dto input)
        {
            string exp=CheckPID(input.test_tube_ID);
            nuclear_test_info test = TransInputDto(input);
            if (exp != String.Empty)
                throw new Exception(exp);
            else
            {
                DbContext.db.Updateable(test).UpdateColumns(it => new { it.test_result }).Where(it => it.test_tube_ID == input.test_tube_ID).ExecuteCommand();
                return String.Empty;
            }
        }
        public string UpdateDetectionResult(List<nuclear_detection_dto> input)
        {
            string exp;
            List<nuclear_test_info> info=new List<nuclear_test_info>();
            nuclear_test_info temp;
            foreach (nuclear_detection_dto test in  input)
            {
                exp=CheckPID(test.test_tube_ID);
                if (exp != String.Empty)
                    throw new Exception(exp);
                else
                    temp = TransInputDto(test);
                info.Add(temp);
            }
            return DbContext.db.Updateable(info).UpdateColumns(it => new { it.test_result }).ExecuteCommand().ToString();
        }
        public List<nuclear_detection_result_dto> GetDetectionResultById(string id)
        {
            return DbContext.db.Queryable<nuclear_test_info>().Where(it => it.user_id == id).Select<nuclear_detection_result_dto>().ToList();
        }
        public List<nuclear_infected_dto> GetInfectdPeople(DateTime start,DateTime end)
        {
            return DbContext.db.Queryable<user_info, nuclear_test_info>((ui, nt) => new JoinQueryInfos(
                JoinType.Right, ui.user_id == nt.user_id
                ))
                .Where((ui, nt) => nt.test_time >= start && nt.test_time <= end && nt.test_result == "阳性")
                .Select<nuclear_infected_dto>()
                .ToList();
        }
        public nuclear_test_info GetInfoByTube(string test_tube_ID)
        {
            return DbContext.db.Queryable<nuclear_test_info>().Where(it => it.test_tube_ID == test_tube_ID).First();
        }
    }
}