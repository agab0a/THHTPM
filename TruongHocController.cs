using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TruongHoc.Models;

namespace TruongHoc.Controllers
{
    public class TruongHocController : ApiController
    {
        TruongHocContext db = new TruongHocContext();

        [HttpGet]
        [Route("api/getall")]
        public List<sinhvien> GetAll()
        {
            return db.sinhviens.ToList();
        }

        [HttpPost]
        [Route("api/create")]
        public bool Create([FromBody] sinhvien sv)
        {
            try
            {
                var oldStudent = db.sinhviens.FirstOrDefault(s => s.masv == sv.masv);
                if (oldStudent != null)
                {
                    return false;
                }
                db.sinhviens.Add(sv);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut]
        [Route("api/update")]
        public bool Update([FromBody] sinhvien sv)
        {
            try
            {
                var oldStudent = db.sinhviens.FirstOrDefault(s => s.masv == sv.masv);
                if (oldStudent == null)
                {
                    return false;
                }
                oldStudent.hoten = sv.hoten;
                oldStudent.diachi = sv.diachi;
                oldStudent.dienthoai = sv.dienthoai;
                oldStudent.malop = sv.malop;
                oldStudent.anh = sv.anh;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        [HttpDelete]
        [Route("api/delete")]
        public bool Delete(int id)
        {
            try
            {
                var oldStudent = db.sinhviens.FirstOrDefault(s => s.masv == id);
                if (oldStudent == null)
                {
                    return false;
                }
                db.sinhviens.Remove(oldStudent);

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        [HttpGet]
        [Route("api/find")]
        public sinhvien Find(int id)
        {
            try
            {
                var oldStudent = db.sinhviens.FirstOrDefault(s => s.masv == id);
                if (oldStudent == null)
                {
                    return null;
                }
                
                return oldStudent;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
