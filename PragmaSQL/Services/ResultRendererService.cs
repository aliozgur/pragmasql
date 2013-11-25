using System;
using System.Collections.Generic;
using System.Text;
using PragmaSQL.Core;

namespace PragmaSQL
{
	public class ResultRendererService:IResultRendererService
	{
		internal static IDictionary<string, ResultRendererSpec> _renderers = new Dictionary<string, ResultRendererSpec>();
		
		#region IResultRendererService Members

		public void Register(ResultRendererSpec spec)
		{
			if(spec == null)
				throw new NullParameterException("ResultRendererSpec parameter is null.");
			if(String.IsNullOrEmpty(spec.Name))
				throw new Exception("Name property of ResultRendererSpec instance is null or empty.");

			if(String.IsNullOrEmpty(spec.FullName))
				throw new Exception("FullName property of ResultRendererSpec instance is null or empty.");

			string fullName = spec.FullName.ToLowerInvariant().Trim();
			if (_renderers.ContainsKey(fullName))
				throw new Exception(String.Format("Renderer of type \"{0}\" already registered.",spec.FullName));

			_renderers.Add(fullName, spec);
		}

		#endregion
	}
}
