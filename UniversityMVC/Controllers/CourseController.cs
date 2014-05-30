using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityLibrary;

namespace UniversityMVC.Controllers
{
    public class CourseController : Controller
    {
        private UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork();

        // GET: /Course/
        public ActionResult Index(string sortBy)
        { 
            ViewBag.SortByName = "Sort by name";
            ViewBag.SortById = "Sort by id";

            var values = new List<object>() { ViewBag.SortByName, ViewBag.SortById };
            var sortValues = new SelectList(values);
            ViewBag.SortValues = sortValues;

            if (sortBy == @ViewBag.SortByName)
            {
                return View(this.unitOfWork.CourseRepository.GetAll().AsQueryable().OrderBy(x=>x.CourseName));
            }

            if (sortBy == @ViewBag.SortById)
            {
                return View(this.unitOfWork.CourseRepository.GetAll().AsQueryable().OrderBy(x=>x.IdCourse));
            }

            var courses = this.unitOfWork.CourseRepository.GetAll();

            return View(courses);
        }

        // GET: /Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = this.unitOfWork.CourseRepository.GetById(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: /Course/Create
        public ActionResult Create()
        {
            ViewBag.IdTeacher = new SelectList(this.unitOfWork.TeacherRepository.GetAll(), "IdTeacher", "LastName");
            return View();
        }

        // POST: /Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdCourse,CourseName,IdTeacher")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.CourseRepository.Insert(cours);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }

            ViewBag.IdTeacher = new SelectList(this.unitOfWork.TeacherRepository.GetAll(), "IdTeacher", "LastName", cours.IdTeacher);
            return View(cours);
        }

        // GET: /Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = this.unitOfWork.CourseRepository.GetById(id);

            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTeacher = new SelectList(this.unitOfWork.TeacherRepository.GetAll(), "IdTeacher", "LastName", cours.IdTeacher);
            return View(cours);
        }

        // POST: /Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdCourse,CourseName,IdTeacher")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.CourseRepository.Update(cours);
                this.unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IdTeacher = new SelectList(this.unitOfWork.TeacherRepository.GetAll(), "IdTeacher", "LastName", cours.IdTeacher);
            return View(cours);
        }

        // GET: /Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = this.unitOfWork.CourseRepository.GetById(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = this.unitOfWork.CourseRepository.GetById(id);
            this.unitOfWork.CourseRepository.Delete(cours);
            this.unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string searchName)
        {
            if (searchName != null)
            {
                return View(this.unitOfWork.CourseRepository.GetAll().Where(x => x.CourseName == searchName).Single());
            }
            else
            {
                return View();
            }
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
