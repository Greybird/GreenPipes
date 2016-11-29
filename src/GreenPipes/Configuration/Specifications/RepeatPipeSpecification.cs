// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace GreenPipes.Specifications
{
    using System.Collections.Generic;
    using System.Threading;
    using Filters;


    public class RepeatPipeSpecification<T> :
        IPipeSpecification<T>
        where T : class, PipeContext
    {
        readonly CancellationToken _cancellationToken;

        public RepeatPipeSpecification(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new RepeatFilter<T>(_cancellationToken));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            yield break;
        }
    }
}