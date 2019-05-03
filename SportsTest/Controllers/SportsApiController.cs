using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsTest.Models;

namespace SportsTest.Controllers
{
    public class SportsApiController : Controller
    {
        SportsTestContext DB = new SportsTestContext();
        //Get all registerd tests
        public ActionResult GetAllTest()
        {
            List<TestList> tsList = new List<TestList>();
            TestList obj_ts_list;
            var result = (from t in DB.Tests
                          join td in DB.TestDetails on t.TestId equals td.TestId
                          orderby td.Date descending
                          select new
                          {

                              td.Date,
                              td.NoOfParticipants,
                              t.SportsName,
                              td.TestDetailsId,
                          }).ToList();
            foreach (var res in result)
            {
                string date = string.Format("{0:dd-MM-yyyy}", res.Date);
                obj_ts_list = new TestList();
                obj_ts_list.Date = date;
                obj_ts_list.NoOfParticipants = res.NoOfParticipants;
                obj_ts_list.TestName = res.SportsName;
                obj_ts_list.TestDeatilsId = res.TestDetailsId;
                tsList.Add(obj_ts_list);

            }

            if (result.Count > 0)
            {
                return Json(new { msgdetail = "success", resultCode = 1, data = tsList });
            }
            else
            {
                return Json(new { msgdetail = "success", resultCode = 0, data = "" });
            }

        }
        //Insert New Test
        public ActionResult CreateTest(TestDetails tst_deatils)
        {
            tst_deatils.NoOfParticipants = 0;
            DB.TestDetails.Add(tst_deatils);
            int result = DB.SaveChanges();
            return Json(new { msgdetail = "success", resultCode = result });
        }
        //Insert New member in test
        public ActionResult InsertAthlete(MemberTestMapping mb_details)
        {
            var checkIfAlreadyAddedOrNot = (from mt in DB.MemberTestMapping
                                            where mt.MemberId == mb_details.MemberId
                                            && mt.TestDetailsId == mb_details.TestDetailsId select mt).ToList();
            TestDetails getTestDetails = (from td in DB.TestDetails
                                  where td.TestDetailsId == mb_details.TestDetailsId
                                  select td).SingleOrDefault();
            int? noOfCount = getTestDetails.NoOfParticipants;
            
            if (checkIfAlreadyAddedOrNot.Count > 0)
            {
                return Json(new { msgdetail = "faill", resultCode = 2 });
            }
            DB.MemberTestMapping.Add(mb_details);
            int result = DB.SaveChanges();
            if (result > 0)
            {
                noOfCount = noOfCount + 1;
            }
            getTestDetails.NoOfParticipants = noOfCount;
            DB.SaveChanges();
            return Json(new { msgdetail = "success", resultCode = result });

        }
        //Get all member by test
        public ActionResult GetAllMemberByTest(int id)
        {
            var memberResult = (from m in DB.Memders
                                join mt in DB.MemberTestMapping on m.MemberId equals mt.MemberId
                                where mt.TestDetailsId == id
                                select new
                                {
                                    m.MemberName,
                                    mt.Distance,
                                    mt.MemberTestId,
                                    m.MemberId
                                }).ToList();
            if (memberResult.Count > 0)
            {
                return Json(new { msgdetail = "success", resultCode = 1, data = memberResult });
            }
            else
            {
                return Json(new { msgdetail = "success", resultCode = 0, data = memberResult });
            }
        }

        //Delete member infrom db
        public ActionResult DeleteMemberFromTest(int id)
        {
            MemberTestMapping result = DB.MemberTestMapping.FirstOrDefault(m => m.MemberTestId == id);
            DB.MemberTestMapping.Remove(result);
            int res = DB.SaveChanges();
            TestDetails getTestDetails = (from td in DB.TestDetails
                                          where td.TestDetailsId == result.TestDetailsId
                                          select td).SingleOrDefault();
            int? noOfCount = getTestDetails.NoOfParticipants;
            if (res > 0)
            {
                noOfCount = noOfCount - 1;
            }
            getTestDetails.NoOfParticipants = noOfCount;
            DB.SaveChanges();
            return Json(new { msgdetail = "success", resultCode = res });
        }
        //Delete Test
        public ActionResult DeleteTest(int id)
        {
            TestDetails result = DB.TestDetails.FirstOrDefault(m => m.TestDetailsId == id);
            var result2 = DB.MemberTestMapping.Where(m => m.TestDetailsId == id).ToList();
            foreach (var m in result2)
            {
                DB.MemberTestMapping.Remove(m);
                DB.SaveChanges();
            }
               
            DB.TestDetails.Remove(result);

            int res = DB.SaveChanges();
            return Json(new { msgdetail = "success", resultCode = res });
        }

        public ActionResult UpdateMember(MemberTestMapping mb_deatils)
        {
            MemberTestMapping member = (from p in DB.MemberTestMapping
                                        where p.MemberTestId == mb_deatils.MemberTestId
                                        select p).SingleOrDefault();
            member.Distance = mb_deatils.Distance;
            member.MemberId = mb_deatils.MemberId;
            int res = DB.SaveChanges();
            return Json(new { msgdetail = "success", resultCode = res });
        }
    }
}