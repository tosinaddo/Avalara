using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Avalara.Database
{
    public partial class AvalaraContext : DbContext
    {
        public AvalaraContext()
        {
        }

        public AvalaraContext(DbContextOptions<AvalaraContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TodoList> TodoLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.TaskId).HasColumnName("task_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("subject");

                entity.Property(e => e.TodoListId).HasColumnName("todo_list_Id");

                entity.HasOne(d => d.TodoList)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TodoListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_todo_list_task");
            });

            modelBuilder.Entity<TodoList>(entity =>
            {
                entity.ToTable("todo_list");

                entity.Property(e => e.TodoListId)
                    .HasColumnName("todo_list_id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
