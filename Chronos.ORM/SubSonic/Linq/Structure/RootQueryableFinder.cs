// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)
//Original code created by Matt Warren: http://iqtoolkit.codeplex.com/Release/ProjectReleases.aspx?ReleaseId=19725

using System.Linq;
using System.Linq.Expressions;

namespace Chronos.ORM.SubSonic.Linq.Structure
{
    /// <summary>
    /// Finds the first sub-expression that accesses a Query&lt;T&gt; object
    /// </summary>
    public class RootQueryableFinder : ExpressionVisitor
    {
        Expression root;
        public static Expression Find(Expression expression)
        {
            RootQueryableFinder finder = new RootQueryableFinder();
            finder.Visit(expression);
            return finder.root;
        }

        protected override Expression Visit(Expression exp)
        {
            Expression result = base.Visit(exp);

            // remember the first sub-expression that produces an IQueryable
            if (this.root == null && result != null && typeof(IQueryable).IsAssignableFrom(result.Type))
            {
                this.root = result;
            }

            return result;
        }
    }
}