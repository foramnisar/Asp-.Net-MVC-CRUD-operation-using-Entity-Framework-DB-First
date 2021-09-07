using crud_mvc.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud_mvc.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        db_testEntities myDb = new db_testEntities();
        public ActionResult Student(tbl_Student obj)
        {
            if(obj != null)
            {
                return View(obj);
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult AddStudent(tbl_Student studentDetails)
        {
            tbl_Student addStudent = new tbl_Student();
            if (ModelState.IsValid)
            {
                addStudent.ID = studentDetails.ID;
                addStudent.Name = studentDetails.Name;
                addStudent.Fname = studentDetails.Fname;
                addStudent.Email = studentDetails.Email;
                addStudent.Mobile = studentDetails.Mobile;
                addStudent.Description = studentDetails.Description;

                if (studentDetails.ID == 0)
                {
                    myDb.tbl_Student.Add(addStudent);
                    myDb.SaveChanges();
                }
                else
                {
                    myDb.Entry(addStudent).State = System.Data.Entity.EntityState.Modified;
                    myDb.SaveChanges();
                }

                
            }
            ModelState.Clear();

            return View("Student");
        }
        
        public ActionResult StudentList()
        {
            var res = myDb.tbl_Student.ToList();
            return View(res);
        }

        
        public ActionResult Delete(int id) 
        {
            var res = myDb.tbl_Student.Where(x => x.ID == id).First();
            myDb.tbl_Student.Remove(res);
            myDb.SaveChanges();

            var newList = myDb.tbl_Student.ToList();
            return View("StudentList",newList);

        }
    }
}