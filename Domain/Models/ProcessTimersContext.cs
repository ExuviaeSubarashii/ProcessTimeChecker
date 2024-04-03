using Microsoft.EntityFrameworkCore;

namespace PTC.Domain.Models;

public partial class ProcessTimersContext : DbContext
{
		public ProcessTimersContext()
		{
		}

		public ProcessTimersContext(DbContextOptions<ProcessTimersContext> options)
						: base(options)
		{
		}

		public virtual DbSet<TasksSaving> TasksSaving { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
						=> optionsBuilder.UseSqlServer("Data Source=DESKTOP-12NGJ7T;Initial Catalog=ProcessTimers;Integrated Security=True;TrustServerCertificate=True");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
				OnModelCreatingPartial(modelBuilder);

				modelBuilder.Entity<TasksSaving>(entity =>
				{
						entity.HasKey(e => e.TaskId);
						entity.Property(e => e.TaskClosing).HasColumnType("datetime");

						entity.Property(e => e.TaskOpening).HasColumnType("datetime");

						entity.Property(e => e.TaskDate).HasColumnType("datetime");

						entity.Property(e => e.TaskName).HasMaxLength(100).IsUnicode(false).IsFixedLength();

						entity.Property(e => e.TaskHour).HasMaxLength(100).IsUnicode(false).IsFixedLength();
				});
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
