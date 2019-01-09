using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Planning.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string Username { get; set; }
		public DateTime DateOfBirth { get; set; }
		[Required]
		public DateTime EnterDate { get; set; }
		[DefaultValue(true)]
		[Required]
		public bool IsVisible { get; set; }
		[NotMapped]
		public IList<UserAccess> UserAccess { get; set; }
		[NotMapped]
		public IList<ProjectRight> ProjectRights { get; set; }
		[NotMapped]
		public IList<ProjectTaskLine> ProjectTaskLines { get; set; }
		[NotMapped]
		public IList<Holiday> Holidays { get; set; }
	}

	public class UserCredential
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[NotMapped]
		public User User { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public DateTime Validity { get; set; }
	}

	public class UserAccess
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }
		[Required]
		public string Path { get; set; }

		public override bool Equals(object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				UserAccess ua = (UserAccess)obj;
				return (this.UserId == ua.UserId) && (this.Path == ua.Path);
			}
		}

		public override int GetHashCode()
		{
			return this.Id;
		}
	}

	public class UserAccessComparer : IEqualityComparer<UserAccess>
	{
		public bool Equals(UserAccess x, UserAccess y)
		{
			return x.Equals(y);
		}

		public int GetHashCode(UserAccess obj)
		{
			return obj.GetHashCode();
		}
	}

	public class Holiday
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[ForeignKey("UserId")]
		public User User { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		public HolidayType HolidayType { get; set; }
		[DefaultValue(false)]
		public bool IsValidate { get; set; }
		public int ValidateUserId { get; set; }
		[ForeignKey("ValidateUserId")]
		public User ValidateUser { get; set; }
	}

	public enum HolidayType
	{
		WorkingTimeReduction = 1,
		PaidVacation = 2,
		UnpaidVacation = 3,
		SickLeave = 4,
	}
}
