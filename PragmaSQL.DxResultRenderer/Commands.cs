using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.Core;
using PragmaSQL.Core;

namespace PragmaSQL.DxResultRenderer
{
	public class RegisterRenderer : AbstractCommand
	{
		public override void Run()
		{
			RegisterRenderers();
			PrepareConfigDialog();
		}

		private void RegisterRenderers()
		{
			try
			{
				Type t = typeof(DxResultRendererFactory);
				IResultRendererFactory factory = Activator.CreateInstance(t) as IResultRendererFactory;

				ResultRendererSpec spec = new ResultRendererSpec();
				spec.Name = factory.FactoryName;
				spec.Description = factory.FactoryDescription;
				spec.FullName = String.Format("{0}, {1}", t.FullName, t.Assembly.FullName);
				spec.RendererType = t;

				HostServicesSingleton.HostServices.ResultRendererService.Register(spec);

				t = typeof(DxResultRendererFactory_AllInOne);
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
				GenericErrorDialog.ShowError("PragmaSQL.DxResultRenderer Error", "Can not register result renderers.", ex);
			}
		}
		private void PrepareConfigDialog()
		{
			HostServicesSingleton.HostServices.ConfigSvc.DialogOpened += ucDxRendererOptions.ConfigSvc_DialogOpened;
			HostServicesSingleton.HostServices.ConfigSvc.DialogClosed += ucDxRendererOptions.ConfigSvc_DialogClosed;
			HostServicesSingleton.HostServices.ConfigSvc.FinalSelection += ucDxRendererOptions.ConfigSvc_FinalSelection;

		}
	}
}
