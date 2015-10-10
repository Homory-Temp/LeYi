using System;

namespace Homory.Model
{
	public abstract class HomoryControl : System.Web.UI.UserControl
	{
		protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());
	}
}
