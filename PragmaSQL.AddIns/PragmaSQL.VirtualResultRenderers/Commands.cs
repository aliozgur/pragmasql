using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.VirtualResultRenderers
{
	public class RegisterRenderer : AbstractCommand
	{
		public override void Run()
		{
			RegisterRenderers();
		}

		private void RegisterRenderers()
		{
			try
			{
				Type t = typeof(VirtualResultRendererFactory);
				IResultRendererFactory factory = Activator.CreateInstance(t) as IResultRendererFactory;

				ResultRendererSpec spec = new ResultRendererSpec();
				spec.Name = factory.FactoryName;
				spec.Description = factory.FactoryDescription;
				spec.FullName = String.Format("{0}, {1}", t.FullName, t.Assembly.FullName);
				spec.RendererType = t;

				HostServicesSingleton.HostServices.ResultRendererService.Register(spec);

				t = typeof(VirtualResultRendererFactory_AllInOne);
				factory = Activator.CreateInstance(t) as IResultRendererFactory;

				spec = new ResultRendererSpec();
				spec.Name = factory.FactoryName;
				spec.Description = factory.FactoryDescription;
				spec.FullName = String.Format("{0}, {1}", t.FullName, t.Assembly.FullName);
				spec.RendererType = t;
        HostServicesSingleton.HostServices.ResultRendererService.Register(spec);



        t = typeof(PaletteCompatibleRendererFactory);
        factory = Activator.CreateInstance(t) as IResultRendererFactory;

        spec = new ResultRendererSpec();
        spec.Name = factory.FactoryName;
        spec.Description = factory.FactoryDescription;
        spec.FullName = String.Format("{0}, {1}", t.FullName, t.Assembly.FullName);
        spec.RendererType = t;

				HostServicesSingleton.HostServices.ResultRendererService.Register(spec);
			}
			catch (Exception ex)
			{
				GenericErrorDialog.ShowError("PragmaSQL.VirtualResultRenderers Error", "Can not register result renderers.", ex);
			}
		}
	}
}
