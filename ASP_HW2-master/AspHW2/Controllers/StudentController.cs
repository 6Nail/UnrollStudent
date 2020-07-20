using AspHW2.Data;
using AspHW2.Models;
using AspHW2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AspHW2.Controllers
{
    public class StudentController : Controller
    {
        private StudentDataContext db = new StudentDataContext();

        // GET: Student
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Mark")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,Mark")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Enroll()
        {
            var model = new StudentCoursesViewModel();
            ViewBag.Students = db.Students.ToList();
            ViewBag.Courses = db.Courses.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Enroll(StudentCoursesViewModel model)
        {
            var student = db.Students.Find(model.StudentId);
            var course = db.Courses.Find(model.CourseId);

            if (student != null && course != null)
            {
                student.Courses = new List<Course>();
                student.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = student.Id });
            }
            return View();
        }

        public ActionResult UnEnroll()
        {
            var model = new StudentCoursesViewModel();
            ViewBag.Students = db.Students.ToList();
            ViewBag.Courses = db.Courses.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult UnEnroll(StudentCoursesViewModel model)
        {
            var student = db.Students.Include(x => x.Courses).FirstOrDefault(s => s.Id == model.StudentId);
            var course = db.Courses.Find(model.CourseId);

            if (student != null)
            {
                if (!student.Courses.Contains(course))
                {
                    return RedirectToAction("Index");
                }

                student.Courses.Remove(course);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = student.Id });

            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}