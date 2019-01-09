using Microsoft.EntityFrameworkCore;
using Planning.Models;

public class PlanningContext : DbContext
{
	public PlanningContext(DbContextOptions<PlanningContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// One to one relation
		builder.Entity<UserCredential>()
			.HasOne(u => u.User);

		builder.Entity<Holiday>()
			.HasOne(h => h.ValidateUser);

		// One to many relation
		builder.Entity<User>()
			.HasMany(u => u.UserAccess)
			.WithOne(h => h.User);

		builder.Entity<User>()
			.HasMany(u => u.Holidays)
			.WithOne(h => h.User);

		builder.Entity<User>()
			.HasMany(u => u.ProjectRights)
			.WithOne(pr => pr.User);

		builder.Entity<User>()
			.HasMany(u => u.ProjectTaskLines)
			.WithOne(ptl => ptl.User);

		builder.Entity<Project>()
			.HasMany(p => p.ProjectTasks)
			.WithOne(pt => pt.Project);

		// Many to many relation
		builder.Entity<ProjectRight>()
			.HasKey(t => new { t.ProjectTaskId, t.UserId });

		builder.Entity<ProjectRight>()
			.HasOne(pr => pr.ProjectTask)
			.WithMany(pt => pt.ProjectRights)
			.HasForeignKey(pr => pr.ProjectTaskId);

		builder.Entity<ProjectRight>()
			.HasOne(pr => pr.User)
			.WithMany(u => u.ProjectRights)
			.HasForeignKey(pr => pr.UserId);
		
		builder.Entity<ProjectTaskLine>()
			.HasKey(t => new { t.ProjectTaskId, t.UserId });

		builder.Entity<ProjectTaskLine>()
			.HasOne(ptl => ptl.ProjectTask)
			.WithMany(pt => pt.ProjectTaskLines)
			.HasForeignKey(ptl => ptl.ProjectTaskId);

		builder.Entity<ProjectTaskLine>()
			.HasOne(ptl => ptl.User)
			.WithMany(u => u.ProjectTaskLines)
			.HasForeignKey(ptl => ptl.UserId);

		base.OnModelCreating(builder);
	}

	public override int SaveChanges()
	{
		ChangeTracker.DetectChanges();
		return base.SaveChanges();
	}

	public DbSet<Planning.Models.User> User { get; set; }

	public DbSet<Planning.Models.UserCredential> UserCredential { get; set; }

	public DbSet<Planning.Models.UserAccess> UserAccess { get; set; }

	public DbSet<Planning.Models.Holiday> Holiday { get; set; }

	public DbSet<Planning.Models.Project> Project { get; set; }

	public DbSet<Planning.Models.ProjectRight> ProjectRight { get; set; }

	public DbSet<Planning.Models.ProjectTask> ProjectTask { get; set; }

	public DbSet<Planning.Models.ProjectTaskLine> ProjectTaskLine { get; set; }
}