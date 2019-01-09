using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Planning.Models
{
	public class Project
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int OwnerId { get; set; }
		[ForeignKey("OwnerId")]
		public User Owner { get; set; }
		[NotMapped]
		public IList<ProjectTask> ProjectTasks { get; set; }
		[DefaultValue(true)]
		public bool IsActive { get; set; }
	}

	public class ProjectRight
	{
		public int ProjectTaskId { get; set; }
		[ForeignKey("ProjectTaskId")]
		public ProjectTask ProjectTask { get; set; }
		
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }
	}

	public class ProjectTask
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int ProjectId { get; set; }
		[ForeignKey("ProjectId")]
		[NotMapped]
		public Project Project { get; set; }
		[NotMapped]
		public IList<ProjectTaskLine> ProjectTaskLines { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		[DefaultValue(true)]
		public bool IsActive { get; set; }
		[NotMapped]
		public IList<ProjectRight> ProjectRights { get; set; }
	}

	public class ProjectTaskLine
	{
		public int ProjectTaskId { get; set; }
		[ForeignKey("ProjectTaskId")]
		public ProjectTask ProjectTask { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }

		[Required]
		public DateTime Date { get; set; }
		[Required]
		public TimeSpan Time { get; set; }
		public string Comment { get; set; }
	}
}
