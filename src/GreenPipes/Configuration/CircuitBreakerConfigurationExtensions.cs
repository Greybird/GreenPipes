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
    using Configurators;
    using Specifications;


    public static class CircuitBreakerConfigurationExtensions
    {
        /// <summary>
        /// Puts a circuit breaker in the pipe, which can automatically prevent the flow of messages to the consumer
        /// when the circuit breaker is opened.
        /// </summary>
        /// <typeparam name="T">The pipe context type</typeparam>
        /// <param name="configurator"></param>
        /// <param name="configure"></param>
        public static void UseCircuitBreaker<T>(this IPipeConfigurator<T> configurator, Action<ICircuitBreakerConfigurator<T>> configure = null)
            where T : class, PipeContext
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var specification = new CircuitBreakerPipeSpecification<T>();

            configure?.Invoke(specification);

            configurator.AddPipeSpecification(specification);
        }
    }
}