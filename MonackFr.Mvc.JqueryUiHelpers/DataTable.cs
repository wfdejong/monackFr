using MonackFr.Mvc.JqueryUiHelpers.Classes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace MonackFr.Mvc.JqueryUiHelpers
{
	/// <summary>
	/// a Jquery ui datatable
	/// As described at http://datatables.net
	/// </summary>
	public class DataTable : JqUi
	{
		private List<Column> _columns;
		private string _ajaxUrl;
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="helper"></param>
		internal DataTable(HtmlHelper helper, string name)
			: base(helper, name)
		{
			_columns = new List<Column>();
		}

		/// <summary>
		/// Renders the output
		/// </summary>
		/// <returns>The client side code that renders in the browser</returns>
		public override MvcHtmlString Show()
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

				if(!string.IsNullOrEmpty(_columns[i].Render))
				{
					dataTable.AppendFormat(", \nrender: {0}", _columns[i].Render);
				}

				dataTable.Append("}");

				if(i < _columns.Count-1)
				{
					dataTable.Append(",\n");
				}
			}

			dataTable.AppendLine("]");
			dataTable.AppendLine("});\n");
						
			dataTable.AppendLine("};");
			dataTable.AppendLine("</script>");

			return new MvcHtmlString(dataTable.ToString());
		}
		
		/// <summary>
		/// Adds a column definition to the table
		/// </summary>
		/// <param name="title">Column title that is shown in the browser</param>
		/// <param name="dataField">Field that binds to the data</param>
		/// <returns>The current datatable</returns>
		public DataTable AddColumn(string title, string dataField)
		{
			return AddColumn(title, dataField, true);
		}

		/// <summary>
		/// Adds a column definition to the table
		/// </summary>
		/// <param name="title">Column title that is shown in the browser</param>
		/// <param name="dataField">Field that binds to the data</param>
		/// <param name="visible">indicates if the column is shown or not</param>
		/// <param name="render">the method that is called on rendering</param>
		/// <returns>The current datatable</returns>
		public DataTable AddColumn(string title, string dataField, string render)
		{
			return AddColumn(title, dataField, true, render);
		}
		
		/// <summary>
		/// Adds a column definition to the table
		/// </summary>
		/// <param name="title">Column title that is shown in the browser</param>
		/// <param name="dataField">Field that binds to the data</param>
		/// <param name="visible">indicates if the column is shown or not</param>
		/// <returns>The current datatable</returns>
		public DataTable AddColumn(string title, string dataField, bool visible)
		{
			return AddColumn(title, dataField, visible, null);
		}
		
		/// <summary>
		/// Adds a column definition to the table
		/// </summary>
		/// <param name="title">Column title that is shown in the browser</param>
		/// <param name="dataField">Field that binds to the data</param>
		/// <param name="visible">indicates if the column is shown or not</param>
		/// <param name="render">the method that is called on rendering</param>
		/// <returns>The current datatable</returns>
		public DataTable AddColumn(string title, string dataField, bool visible, string render)
		{
			_columns.Add(new Column(title, dataField, visible, render));
			return this;
		}

		/// <summary>
		/// Sets the url for getting the json data
		/// </summary>
		/// <param name="url">path from root</param>
		/// <returns>The current datatable</returns>
		public DataTable AjaxUrl(string url)
		{
			_ajaxUrl = url;
			return this;
		}

		/// <summary>
		/// Converts data to json that is fit for datatables
		/// </summary>
		/// <typeparam name="T">Type of the data</typeparam>
		/// <param name="data">the data that need to be converted</param>
		/// <param name="key">the key field</param>
		/// <returns>Json</returns>
		public static JsonResult DataToJson<T>(IEnumerable<T> data, Expression<Func<T, object>> key)
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

		public static JsonResult DataToJson(IEnumerable<object> data)
		{
			return DataToJson(data, null);
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