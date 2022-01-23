using ajax_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ajax_CRUD.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        readonly AjaxCallEntities1 db = new AjaxCallEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveData(int Id,string Name,int Class, int Roll, bool IsGood)
        {
            Student student = new Student();
            student.Id = Id;
            student.Name = Name;
            student.Class = Class;
            student.Roll = Roll;
            student.IsGood = IsGood;
            if (student.Id == 0)
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            else
            {
                var std = db.Students.Find(student.Id);
                if(std != null)
                {
                    std.Name = student.Name;
                    std.Class = student.Class;
                    std.Roll = student.Roll;
                    std.IsGood = student.IsGood;
                    
                }
                db.SaveChanges();

            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudents()
        {
            List<Student> students_list = new List<Student>();
            students_list = db.Students.ToList();
            return Json(students_list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentById(int Id)
        {
            var student = db.Students.FirstOrDefault(x => x.Id == Id);
            return Json(student, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            var student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}