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
    public class TeacherController : Controller
    {
        private UnitOfWork.UnitOfWork unitOfWork = new UnitOfWork.UnitOfWork();

        // GET: /Teacher/
        public ActionResult Index(string sortBy)
        {
            ViewBag.SortByName = "Sort By Name";
            ViewBag.SortById = "Sort By Id";

            if (sortBy == ViewBag.SortByName)
            {
                return View(this.unitOfWork.TeacherRepository.GetAll().AsQueryable().OrderBy(x => x.LastName));
            }

            if (sortBy == ViewBag.SortById)
            {
                return View(this.unitOfWork.TeacherRepository.GetAll().AsQueryable().OrderBy(x => x.IdTeacher));
            }

            return View(this.unitOfWork.TeacherRepository.GetAll());
        }

        // GET: /Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = this.unitOfWork.TeacherRepository.GetById(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IdTeacher,LastName,FirstName")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.TeacherRepository.Insert(teacher);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: /Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = this.unitOfWork.TeacherRepository.GetById(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }

        // POST: /Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IdTeacher,LastName,FirstName")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                this.unitOfWork.TeacherRepository.Update(teacher);
                this.unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: /Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = this.unitOfWork.TeacherRepository.GetById(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }

        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = this.unitOfWork.TeacherRepository.GetById(id);
            this.unitOfWork.TeacherRepository.Delete(teacher);
            this.unitOfWork.Save();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Search(string searchBy,string search)
        {
            ViewBag.SearchByName = "SearchByName";
            ViewBag.SearchById = "SearchById";
            
            if (searchBy == ViewBag.SearchById)
            {
                return View(this.unitOfWork.TeacherRepository.GetById(Convert.ToInt32(search)));
            }
            if (searchBy == ViewBag.SearchByName)
            {
                var allTeachers = this.unitOfWork.TeacherRepository.GetAll();
                var searchTeacher = allTeachers.Where(x => x.LastName == search).Single();
                return View(searchTeacher);
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
