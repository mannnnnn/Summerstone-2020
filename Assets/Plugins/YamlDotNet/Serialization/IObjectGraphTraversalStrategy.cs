//  This file is part of YamlDotNet - A .NET library for YAML.
//  Copyright (c) Antoine Aubry and contributors

//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//  of the Software, and to permit persons to whom the Software is furnished to do
//  so, subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.

using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Defines a strategy that walks through an object graph.
    /// </summary>
    public interface IObjectGraphTraversalStrategy
    {
        /// <summary>
        /// Traverses the specified object graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="visitor">An <see cref="IObjectGraphVisitor{TContext}"/> that is to be notified during the traversal.</param>
        /// <param name="context">A <typeparamref name="TContext" /> that will be passed to the <paramref name="visitor" />.</param>
        void Traverse<TContext>(IObjectDescriptor graph, IObjectGraphVisitor<TContext> visitor, TContext context);
    }
}
