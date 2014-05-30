using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityLibrary;

namespace UniversityMVC.ViewModel
{
    public class StudentCourses
    {
        public virtual Student Student { get; set; }
        public virtual Cours Course { get; set; }

    }
}