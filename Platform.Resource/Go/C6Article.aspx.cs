﻿using Homory.Model;
using System;

namespace Go
{
    public partial class GoC6Article : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
