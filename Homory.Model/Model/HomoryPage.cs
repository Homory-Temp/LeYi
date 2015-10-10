using System;

namespace Homory.Model
{
	public abstract class HomoryPage : System.Web.UI.Page
	{
		protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());
	}
}
