using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityLibrary;
using UniversityMVC.UnitOfWork;
using UniversityMVC.ViewModel;


namespace UniversityMVC.Controllers
{
    public class StudentController : Controller
    {
        private UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork();

        // GET: /Student/
        public ActionResult Index(string sortBy)
        {
            ViewBag.SortByName = "SortByName";
            ViewBag.SortById = "SortById";
            var values = new List<string>{"SortByName", "SortById"};
            var sortValues = new SelectList( values);
            ViewBag.sortBy = sortValues;


            if (sortBy == ViewBag.SortByName)
            {
                return View(this.unitOfWork.StudentRepository.GetAll().AsQueryable().OrderBy(x => x.LastName));
            }
            else
            {
                return View(this.unitOfWork.StudentRepository.GetAll().AsQueryable().OrderBy(x => x.IdStudent));

            }
        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: /Student/Create
        public ActionResult Create()
        {

            ViewBag.AllCourses = new SelectList(this.unitOfWork.CourseRepository.GetAll(), "IdCourse", "CourseName"); 

           // ViewBag.Courses = new MultiSelectList(this.unitOfWork.CourseRepository.GetAll(), "IdCourse", "CourseName");
           
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( StudentCourses studentCourses)
        {
            

            if (ModelState.IsValid)
            {
                var student = studentCourses.Student;
                var course = this.unitOfWork.CourseRepository.GetById(studentCourses.Course.IdCourse);
                student.Courses.Add(course);
                this.unitOfWork.StudentRepository.Insert(student);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(studentCourses);
        }

        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdStudent,LastName,FirstName")] Student student)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.StudentRepository.Update(student);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = this.unitOfWork.StudentRepository.GetById(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = this.unitOfWork.StudentRepository.GetById(id);
            this.unitOfWork.StudentRepository.Delete(student);
            this.unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddCourse (int? id)
        {
            

             if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentCourses stCourse = new StudentCourses();
            stCourse.Student = student;
            var allCourses = this.unitOfWork.CourseRepository.GetAll();
            var studentCourses = student.Courses;
            var coursesAvailable = allCourses.Except(studentCourses);
            ViewBag.Courses = new MultiSelectList(coursesAvailable, "IdCourse", "CourseName", student.Courses);



            return View(stCourse);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(StudentCourses studentCourses)
        {

            if (ModelState.IsValid)
            {
                var student = this.unitOfWork.StudentRepository.GetById(studentCourses.Student.IdStudent);
                var course = this.unitOfWork.CourseRepository.GetById(studentCourses.Course.IdCourse);
                student.Courses.Add(course);
                this.unitOfWork.StudentRepository.Update(student);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(studentCourses);
        }

        public ActionResult DeleteCourse(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentCourses stCourse = new StudentCourses();
            stCourse.Student = student;
            var studentAllCourse = student.Courses;

            ViewBag.Courses = new MultiSelectList(studentAllCourse, "IdCourse", "CourseName", student.Courses);



            return View(stCourse);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(StudentCourses studentCourses)
        {

            if (ModelState.IsValid)
            {
                var student = this.unitOfWork.StudentRepository.GetById(studentCourses.Student.IdStudent);
                var course = this.unitOfWork.CourseRepository.GetById(studentCourses.Course.IdCourse);
                student.Courses.Remove(course);
                this.unitOfWork.StudentRepository.Update(student);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(studentCourses);
        }

        [HttpGet]
        public ActionResult Search(string searchBy,string search)
        {


            ViewBag.SearchByName = "Search By Name";
            ViewBag.SearchById = "Search By Id";

            if (searchBy == ViewBag.SearchByName)
            {
                return View(this.unitOfWork.StudentRepository.GetAll().Where(x => x.LastName == search).Single());
            }

            if (searchBy == ViewBag.SearchById)
            {
                return View(this.unitOfWork.StudentRepository.GetById(Convert.ToInt32(search)));
            }

            return View();
            



           
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
