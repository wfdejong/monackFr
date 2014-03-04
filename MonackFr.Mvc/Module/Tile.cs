﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Mvc.Module
{
	public class Tile
	{
		/// <summary>
		/// Title
		/// </summary>
		public string Title { get; set; }
		
		/// <summary>
		/// Items shown in tile
		/// </summary>
		public string[] PreviewItems { get; set; }

		/// <summary>
		/// Copyright text
		/// </summary>
		public string Copyright { get; set; }
		
		/// <summary>
		/// Action url
		/// </summary>
		public string Url { get; set; }
	}
}