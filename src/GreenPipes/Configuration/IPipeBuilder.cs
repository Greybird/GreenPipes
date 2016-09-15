﻿// Copyright 2013-2016 Chris Patterson
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace GreenPipes
{
    /// <summary>
    /// A pipe builder constructs a pipe by adding filter to the end of the chain, after
    /// while the builder completes the pipe/filter combination.
    /// </summary>
    /// <typeparam name="TContext">The pipe context type</typeparam>
    public interface IPipeBuilder<TContext>
        where TContext : class, PipeContext
    {
        /// <summary>
        /// Add a filter to the pipe after any existing filters
        /// </summary>
        /// <param name="filter">The filter to add</param>
        void AddFilter(IFilter<TContext> filter);
    }


    /// <summary>
    /// A pipe builder constructs a pipe by adding filter to the end of the chain, after
    /// while the builder completes the pipe/filter combination.
    /// </summary>
    /// <typeparam name="TContext">The pipe context type</typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IPipeBuilder<TContext, TResult>
        where TContext : class, PipeContext
        where TResult : class
    {
        /// <summary>
        /// Add a filter to the pipe after any existing filters
        /// </summary>
        /// <param name="filter">The filter to add</param>
        void AddFilter(IFilter<TContext, TResult> filter);
    }
}