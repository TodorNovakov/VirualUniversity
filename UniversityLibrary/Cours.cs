//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniversityLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Cours
    {
        public Cours()
        {
            this.Students = new HashSet<Student>();
        }
    
        public int IdCourse { get; set; }


        public string CourseName { get; set; }


        public int IdTeacher { get; set; }
    
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}