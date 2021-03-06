﻿namespace MonackFr.Library.Module
{
	/// <summary>
	/// Definition of a package
	/// </summary>
    public interface IPackage
    {
        /// <summary>
        /// Package name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Package description
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// location of scripts
        /// </summary>
        string ScriptSource { get; }
    }
}