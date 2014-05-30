using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityLibrary;
using UniversityMVC.Repository;

namespace UniversityMVC.UnitOfWork
{
    public class UnitOfWork:IDisposable
    {
        private UniversityLibrary.SchoolEntities context = new UniversityLibrary.SchoolEntities();
        private Repository<Student> studentRepository;
        private Repository<Teacher> teacherRepository;
        private Repository<Cours> courseRepository;

        public Repository<Student> StudentRepository
        {
            get 
            {
                if (this.studentRepository==null)
                {
                    this.studentRepository=new Repository<Student>(this.context);
                }

                return this.studentRepository;
            }
        }

        public Repository<Teacher> TeacherRepository
        {
            get
            {
                if (this.teacherRepository==null)
                {
                    this.teacherRepository=new Repository<Teacher>(this.context);
                }

                return this.teacherRepository;
            }
        }

        public Repository<Cours> CourseRepository
        {
            get
            {
                if (this.courseRepository==null)
                {
                    this.courseRepository=new Repository<Cours>(this.context);
                }
                
                return this.courseRepository;

            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

         private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    
}