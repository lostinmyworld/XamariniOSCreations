using System;
using System.Collections.Generic;

namespace Mobile.iOS
{
	//	Idea from https://github.com/MilenPavlov/TreeViewSample/blob/master/TV1/TvViewController.cs
	public class TreeNode
	{
		public string NodeName { get; set; }
		public int NodeId { get; set; }
		public int NodeLevel { get; set; }
		public List<TreeNode> Children { get; set; }
		public TreeNode Parent { get; set; }
		public bool IsExpanded { get; set; }
		public bool IsSelected { get; set; }
		public bool IsNodeUser { get; set; }
		public TreeNode ()
		{
			Children = new List<TreeNode> ();
		}
	}
}

