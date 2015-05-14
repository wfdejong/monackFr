using MonackFr.Mvc.JqueryUiHelpers.Classes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace MonackFr.Mvc.JqueryUiHelpers
{
	public class DataTable
	{
		private HtmlHelper _helper;
		private string _name;
		private List<Column> _columns;
		private string _ajaxUrl;
		private string _onRowClick;


		internal DataTable(HtmlHelper helper)
		{
			_helper = helper;
			_columns = new List<Column>();
		}

		public MvcHtmlString Show()
		{
			StringBuilder dataTable = new StringBuilder();

			dataTable.AppendFormat("<table id=\"{0}\"></table>", _name);
			dataTable.AppendLine("<script type=\"text/javascript\">");			
			dataTable.AppendFormat("var {0} = function (data) {{", _name);
			dataTable.AppendLine("console.log(data);");			
			dataTable.Append("$(\"#");
			dataTable.AppendFormat("{0}\")", _name);
			dataTable.Append(".dataTable({");
			dataTable.AppendLine("\"ajax\": {");
			dataTable.AppendFormat("url: \"{0}\",\n", _ajaxUrl);
			dataTable.AppendLine("type: \"Post\"");
			dataTable.AppendLine("},");
			dataTable.AppendLine("columns: [");

			for (int i = 0; i < _columns.Count; i++)
			{
				dataTable.AppendLine("{");
				dataTable.AppendFormat("title: '{0}',\n", _columns[i].Title);
				dataTable.AppendFormat("data: '{0}'\n", _columns[i].DataField);

				if(!_columns[i].Visible)
				{
					dataTable.AppendFormat(",\nvisible: false\n", _columns[i].DataField);
				}

				dataTable.Append("}");

				if(i < _columns.Count-1)
				{
					dataTable.Append(",\n");
				}
			}

			dataTable.AppendLine("]");
			dataTable.AppendLine("});\n");

			dataTable.Append("$(\"#");
			dataTable.AppendFormat("{0} tbody\").on(\"click\", \"tr\", function(){{", _name);
			dataTable.AppendFormat("{0}(this);", _onRowClick);
			dataTable.AppendLine("});\n");
			

			dataTable.AppendLine("};");
			dataTable.AppendLine("</script>");

			return new MvcHtmlString(dataTable.ToString());
		}

		public DataTable Name(string name)
		{
			_name = name;
			return this;
		}

		public DataTable AddColumn(string title, string dataField)
		{
			return AddColumn(title, dataField, true);
		}

		public DataTable AddColumn(string title, string dataField, bool visible)
		{
			_columns.Add(new Column(title, dataField, visible));
			return this;
		}

		public DataTable AjaxUrl(string url)
		{
			_ajaxUrl = url;
			return this;
		}

		public DataTable OnRowClick(string callback)
		{
			_onRowClick = callback;
			return this;
		}

		public static JsonResult DataToJson<T>(object data, Expression<Func<T, object>> key)
		{
			string id = null;
			if(key!=null)
			{
				id = GetPropertyInfo<T, object>(key).Name;
			}

			return new JsonResult
			{
				Data = new
				{
					data = data,
					id = id					
				}
			};
		}

		public static JsonResult DataToJson(object data)
		{
			return DataToJson<object>(data, null);
		}

		public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
		{
			Type type = typeof(TSource);

			MemberExpression member = propertyLambda.Body as MemberExpression;
			if (member == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda.ToString()));

			PropertyInfo propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda.ToString()));

			if (type != propInfo.ReflectedType &&
				!type.IsSubclassOf(propInfo.ReflectedType))
				throw new ArgumentException(string.Format(
					"Expresion '{0}' refers to a property that is not from type {1}.",
					propertyLambda.ToString(),
					type));

			return propInfo;
		}
	}
}