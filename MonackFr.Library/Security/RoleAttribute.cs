using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;

namespace MonackFr.Security
{
	/// <summary>
	/// Attribute for defining role for mvc action
	/// </summary>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public class RoleAttribute : AuthorizeAttribute
	{
		public RoleAttribute(params Object[] roles)
		{
			if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
			{
				throw new ArgumentException("Role attribute argument is not of type Enum");
			}

			this.Roles = String.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
		}
	}

	/// <summary>
	/// Description attribute for enums
	/// </summary>
	public class Description : Attribute
	{
		private String _content;

		public Description(String content)
		{
			if (content == string.Empty)
			{
				throw new EmptyStringException();
			}
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			_content = content;
		}

		public String Content
		{
			get
			{
				return _content;
			}
		}
	}

	/// <summary>
	/// Extension class for enums
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Convert enum description attribute to string
		/// </summary>
		/// <param name="en"></param>
		/// <returns></returns>
		public static String ToDescription(this Enum en)
		{
			Type enumType = en.GetType();
			MemberInfo[] enumMemberInfos = enumType.GetMember(en.ToString());

			if (enumMemberInfos != null && enumMemberInfos.Length > 0)
			{
				Object[] EnumMemberAttributes = enumMemberInfos[0].GetCustomAttributes(typeof(Description), false);
				if (EnumMemberAttributes != null && EnumMemberAttributes.Length > 0)
				{
					return ((Description)EnumMemberAttributes[0]).Content;
				}
			}
			return en.ToString();
		}
	}
}
