﻿// Copyright 2012-2018 Chris Patterson
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
    using System;
    using System.Threading.Tasks;
    using Configurators;
    using Pipes;


    public static class Pipe
    {
        /// <summary>
        /// Create a new pipe using the pipe configurator to add filters, etc.
        /// </summary>
        /// <typeparam name="T">The pipe context type</typeparam>
        /// <param name="callback">The configuration callback</param>
        /// <returns>An initialized pipe ready for use</returns>
        public static IPipe<T> New<T>(Action<IPipeConfigurator<T>> callback)
            where T : class, PipeContext
        {
            var configurator = new PipeConfigurator<T>();

            callback(configurator);

            return configurator.Build();
        }

        /// <summary>
        /// Constructs a simple pipe that executes the specified action
        /// </summary>
        /// <typeparam name="T">The pipe context type</typeparam>
        /// <param name="action">The method to execute</param>
        /// <returns>The constructed pipe</returns>
        public static IPipe<T> Execute<T>(Action<T> action)
            where T : class, PipeContext
        {
            var configurator = new PipeConfigurator<T>();

            configurator.UseExecute(action);

            return configurator.Build();
        }

        /// <summary>
        /// Constructs a simple pipe that executes the specified action
        /// </summary>
        /// <typeparam name="T">The pipe context type</typeparam>
        /// <param name="action">The method to execute</param>
        /// <returns>The constructed pipe</returns>
        public static IPipe<T> ExecuteAsync<T>(Func<T, Task> action)
            where T : class, PipeContext
        {
            var configurator = new PipeConfigurator<T>();

            configurator.UseExecuteAsync(action);

            return configurator.Build();
        }

        /// <summary>
        /// Returns an empty pipe of the specified context type
        /// </summary>
        /// <typeparam name="T">The context type</typeparam>
        /// <returns></returns>
        public static IPipe<T> Empty<T>()
            where T : class, PipeContext
        {
            return Cache<T>.EmptyPipe;
        }


        static class Cache<TContext>
            where TContext : class, PipeContext
        {
            internal static readonly IPipe<TContext> EmptyPipe = new EmptyPipe<TContext>();
        }
    }
}