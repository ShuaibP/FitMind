﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FitMind.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class fitMindDbEntities1 : DbContext
    {
        public fitMindDbEntities1()
            : base("name=fitMindDbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Goodie> Goodies { get; set; }
        public virtual DbSet<Quiz> Quizs { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Trophy> Trophies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<UserExam> UserExams { get; set; }
        public virtual DbSet<UserQuiz> UserQuizs { get; set; }
        public virtual DbSet<UserSurvey> UserSurveys { get; set; }
        public virtual DbSet<UserTrophy> UserTrophies { get; set; }
    }
}
