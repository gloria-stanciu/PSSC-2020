using Microsoft.EntityFrameworkCore;
using StackUnderflow.DatabaseModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.EF.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
        public DbSet<QuestionModel> QuestionModel { get; set; }
    }
}
