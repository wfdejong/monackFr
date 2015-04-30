using MonackFr.Mvc.JqueryUiHelpers.Classes;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace MonackFr.Mvc.JqueryUiHelpers
{
	public class DataTable
	{
		private HtmlHelper _helper;
		private string _name;
		private List<Column> _columns;


		internal DataTable(HtmlHelper helper)
		{
			_helper = helper;
			_columns = new List<Column>();
		}

		public MvcHtmlString Show()
		{
			StringBuilder dataTable = new StringBuilder();

			dataTable.AppendFormat("var {0} = function (data) {{", _name);
			dataTable.AppendLine("console.log(data);");			
			dataTable.AppendLine("$('#demo').DataTable({");
			dataTable.AppendLine("data: data,");
			dataTable.AppendLine("columns: [");

			for (int i = 0; i < _columns.Count; i++)
			{
				dataTable.AppendLine("{");
				dataTable.AppendFormat("Title: '{0}',\n", _columns[i].Title);
				dataTable.AppendFormat("data: '{0}'\n", _columns[i].DataField);
				dataTable.Append("}");

				if(i < _columns.Count-1)
				{
					dataTable.Append(",\n");
				}
			}

			dataTable.AppendLine("]");
			dataTable.AppendLine("});");
			dataTable.AppendLine("};");

			return new MvcHtmlString(dataTable.ToString());
		}

		public DataTable Name(string name)
		{
			_name = name;
			return this;
		}

		public DataTable AddColumn(string title, string dataField)
		{
			_columns.Add(new Column(title, dataField));
			return this;
		}
	}
}