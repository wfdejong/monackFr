﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonackFr.Module
{
	public class Tile
	{
		private IModule _module;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="module">the system name of the module</param>
		public Tile(IModule module)
		{
			_module = module;
		}

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

		public IModule Module { get { return _module; } }
	}
}