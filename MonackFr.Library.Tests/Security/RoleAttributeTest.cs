﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonackFr.Security;
using Moq;

namespace MonackFr.Library.Tests.SecurityTest
{
	enum TestAttibutes
	{
		Test1,
		Test2
	}

	[TestClass]
	public class RoleAttributeTest
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void test_if_role_attribute_throws_argument_exeption()
		{
			RoleAttribute roleAttribute = new RoleAttribute("Role1", "Role2");
		}

		[TestMethod]
		public void test_if_role_attribute_constructor_adds_roles()
		{
			RoleAttribute roleAttribute = new RoleAttribute(TestAttibutes.Test1, TestAttibutes.Test2);
			Assert.AreEqual(roleAttribute.Roles, TestAttibutes.Test1.ToString() + "," + TestAttibutes.Test2.ToString());
		}
	}

	[TestClass]
	public class DescriptionTest
	{

		[TestMethod]
		public void description_is_set_in_constructor()
		{
			string testedValue = "DescriptionContent";
			Description description = new Description(testedValue);
			Assert.AreEqual(description.Content, testedValue);
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyStringException))]
		public void throws_exception_on_empty_string()
		{
			string testedValue = string.Empty;
			Description description = new Description(testedValue);			
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void throws_exception_on_null()
		{
			string testedValue = null;
			Description description = new Description(testedValue);
		}
	}

	[TestClass]
	public class EnumExtensionsTest
	{
		enum testenum
		{
			[@Description("testdescription")]
			first,
			second
		}

		[TestMethod]
		public void returns_description()
		{
			testenum test = testenum.first;
			Assert.AreEqual(test.ToDescription(), "testdescription");
		}

		[TestMethod]
		public void returns_enum_value()
		{
			testenum test = testenum.second;
			Assert.AreEqual(test.ToDescription(), "second");
			Assert.AreEqual(test.ToDescription(), test.ToString());

		}
	}
}
