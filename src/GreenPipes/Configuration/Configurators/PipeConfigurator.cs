﻿// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace GreenPipes.Configurators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders;
    using Validation;


    public class PipeConfigurator<TContext> :
        IBuildPipeConfigurator<TContext>,
        Configurator
        where TContext : class, PipeContext
    {
        readonly List<IPipeSpecification<TContext>> _specifications;

        public PipeConfigurator()
        {
            _specifications = new List<IPipeSpecification<TContext>>();
        }

        IEnumerable<ValidationResult> Configurator.Validate()
        {
            return _specifications.SelectMany(x => x.Validate());
        }

        void IPipeConfigurator<TContext>.AddPipeSpecification(IPipeSpecification<TContext> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _specifications.Add(specification);
        }

        public IPipe<TContext> Build()
        {
            ValidatePipeConfiguration();

            var builder = new PipeBuilder<TContext>();

            foreach (IPipeSpecification<TContext> configurator in _specifications)
                configurator.Apply(builder);

            return builder.Build();
        }

        void ValidatePipeConfiguration()
        {
            IPipeConfigurationResult result = new PipeConfigurationResult(_specifications.SelectMany(x => x.Validate()));
            if (result.ContainsFailure)
                throw new PipeConfigurationException(result.GetMessage("The pipe configuration was invalid"));
        }
    }
}