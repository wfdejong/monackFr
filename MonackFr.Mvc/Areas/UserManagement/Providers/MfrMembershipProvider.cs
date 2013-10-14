using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MonackFr.Mvc.Areas.UserManagement.Models;
using MonackFr.Mvc.Areas.UserManagement.Repositories;
using MonackFr.Security;

namespace MonackFr.Mvc.Areas.UserManagement.Providers
{
	public class MfrMembershipProvider : MembershipProvider, IMfrMembershipProvider
	{
		#region private properties

		/// <summary>
		/// user repository 
		/// </summary>
		private IUserRepository _repository;

		#endregion //private properties

		#region constructors

		/// <summary>
		/// constructor with repository
		/// </summary>
		/// <param name="repository"></param>

		public MfrMembershipProvider() : base()
		{
			_repository = new UserRepository();
		}
		public MfrMembershipProvider(IUserRepository repository) : base()
		{
			_repository = repository;
		}

		#endregion //constructors

		#region overridden functions from MembershipProvider class

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			Hash hash = new Hash();
			User user = new User()
			{
				UserName = username,
				Password = Security.SecurityWrapper.Hash().Create(password),
				Email = email
			};

			_repository.Create(user);
			_repository.Save();
			status = MembershipCreateStatus.Success;
			return GetUser(username, false);
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			User user = _repository.GetSingle(u => u.UserName == username);
			return user.GetMembershipuser();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new NotImplementedException(); }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string userName, string password)
		{
			User user = _repository.GetSingle(u => u.UserName == userName);

			if (user != null)
			{
				return SecurityWrapper.Hash().ValidatePassword(password, user.Password);
			}

			return false;
		}

		#endregion //overridden functions from MembershipProvider class

		#region implementation IMfrMembershipProvider

		MfrUser IMfrMembershipProvider.GetMfrUser(string userName)
		{
			User user = _repository.GetSingle(u => u.UserName == userName);
			return user;
		}

		#endregion //implementation IMfrMembershipProvider
	}
}