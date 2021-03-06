﻿using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;

namespace XamlCSS.Dom
{
	public abstract class ElementCollectionBase<TDependencyObject> : List<IElement>, IHtmlCollection<IElement>
		where TDependencyObject : class
	{
        protected ITreeNodeProvider<TDependencyObject> treeNodeProvider;

        public ElementCollectionBase(IEnumerable<IElement> elements, ITreeNodeProvider<TDependencyObject> treeNodeProvider)
		{
            this.treeNodeProvider = treeNodeProvider;

            var children = elements;

			this.AddRange(children);
		}

		public ElementCollectionBase(IDomElement<TDependencyObject> node, ITreeNodeProvider<TDependencyObject> treeNodeProvider)
		{
            this.treeNodeProvider = treeNodeProvider;

            var children = GetChildren(node.Element)
				.Select(x => CreateElement(x, node))
				.ToList();

			this.AddRange(children);
		}

		abstract protected IEnumerable<TDependencyObject> GetChildren(TDependencyObject dependencyObject);

		abstract protected IElement CreateElement(TDependencyObject dependencyObject, IDomElement<TDependencyObject> parentNode);

		abstract protected string GetId(TDependencyObject dependencyObject);

		public IElement this[string id]
		{
			get
			{
				return this.FirstOrDefault(x => GetId((x as IDomElement<TDependencyObject>).Element) == id);
			}
		}

		public int Length { get { return Count; } }
	}
}
