﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestGit.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyOpacDBContext : DbContext
    {
        public MyOpacDBContext()
            : base("name=MyOpacDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<config> configs { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<feedback_comments> feedback_comments { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<library> libraries { get; set; }
        public virtual DbSet<lm> lms { get; set; }
        public virtual DbSet<loan> loans { get; set; }
        public virtual DbSet<reservation> reservations { get; set; }
        public virtual DbSet<reservation_items> reservation_items { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
