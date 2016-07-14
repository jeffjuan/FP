using FP.CORE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FP.CORE.DAL
{
    public class FP_EFContext:DbContext
    {
        public FP_EFContext():base("name=DefaultConnection")
        {
            Database.SetInitializer<FP_EFContext>(new FP_DbInitializer());
        }

        public virtual DbSet<FP_DEPARTMENT> DEPARTMENT { get; set; }
        public virtual DbSet<FP_FEATURE> FEATURE { get; set; }
        public virtual DbSet<FP_ROLE> ROLE { get; set; }
        public virtual DbSet<FP_USER> USER { get; set; }
        public virtual DbSet<FP_USER_FEATURE_ROLE> USER_FEATURE_ROLE { get; set; }
    }
}
