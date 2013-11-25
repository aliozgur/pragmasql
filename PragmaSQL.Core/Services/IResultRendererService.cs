using System;
using System.Collections.Generic;
using System.Text;

namespace PragmaSQL.Core
{
	public class ResultRendererSpec
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		private string _description;
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private string _fullName = String.Empty;
		public string FullName
		{
			get { return _fullName; }
			set { _fullName = value; }
		}

		private Type _rendererType;
		public Type RendererType
		{
			get { return _rendererType; }
			set { _rendererType = value; }
		}

		public ResultRendererSpec Copy()
		{
			ResultRendererSpec result = new ResultRendererSpec();
			result.Name = Name;
			result.Description = Description;
			result.FullName = FullName;
			result.RendererType = RendererType;
			return result;
		}

		public override string ToString()
		{
			return Name;
		}
	}

	public interface IResultRendererService
	{
		void Register(ResultRendererSpec spec);
	}
}
