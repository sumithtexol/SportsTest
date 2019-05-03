using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsTest.Models;
namespace SportsTest.Controllers
{
    public class TestsController : Controller
    {
       
        SportsTestContext DB = new SportsTestContext();
        //List all Test details
        public IActionResult Index()
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
            foreach(var res in result)
            {
                string date = string.Format("{0:dd-MM-yyyy}", res.Date);
                obj_ts_list = new TestList();
                obj_ts_list.Date = date;
                obj_ts_list.NoOfParticipants = res.NoOfParticipants;
                obj_ts_list.TestName = res.SportsName;
                obj_ts_list.TestDeatilsId = res.TestDetailsId;
                tsList.Add(obj_ts_list);

            }
            ViewBag.Tests = DB.Tests.ToList();
            ViewBag.TestDeatils = tsList;
            return View();
        }
        //Test Deatails
        public ActionResult TestDeatils(int id)
        {
            List<MemberDetailsByTest> mbList = new List<MemberDetailsByTest>();
            MemberDetailsByTest obj_mb_list;
            var result = (from t in DB.Tests
                          join td in DB.TestDetails on t.TestId equals td.TestId
                          where td.TestDetailsId ==id
                          select new
                          {
                              td.Date,
                              t.SportsName,
                              td.TestDetailsId,
                          }).FirstOrDefault();
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
            foreach (var res in memberResult)
            {
                obj_mb_list = new MemberDetailsByTest();
                obj_mb_list.MemberName = res.MemberName;
                obj_mb_list.Distance = res.Distance;
                obj_mb_list.MemberTestId = res.MemberTestId;
                obj_mb_list.MemberId = res.MemberId;
                mbList.Add(obj_mb_list);
            }
            ViewBag.MemberListByTest = mbList;
            ViewBag.TestName = result.SportsName;
            ViewBag.TestDate = result.Date;
            ViewBag.TestId = id;

            ViewBag.MemberDetails = DB.Memders.ToList(); ;
            return View();
        }



        
       
        
        


    }
}