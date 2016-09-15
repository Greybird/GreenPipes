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
    using System.ComponentModel;


    /// <summary>
    /// Configures a pipe with specifications
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IPipeConfigurator<TContext>
        where TContext : class, PipeContext
    {
        /// <summary>
        /// Adds a pipe specification to the pipe configurator at the end of the chain
        /// </summary>
        /// <param name="specification">The pipe specification to add</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddPipeSpecification(IPipeSpecification<TContext> specification);
    }


    /// <summary>
    /// Configures a pipe with specifications
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IPipeConfigurator<TContext, TResult> :
        IPipeConfigurator<TContext>
        where TContext : class, PipeContext
        where TResult : class
    {
        /// <summary>
        /// Adds a pipe specification to the pipe configurator at the end of the chain
        /// </summary>
        /// <param name="specification">The pipe specification to add</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddPipeSpecification(IPipeSpecification<TContext, TResult> specification);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void SetHandlerPipe(IPipe<TContext, TResult> handlerPipe);
    }
}