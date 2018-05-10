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
namespace GreenPipes.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Contracts;
    using GreenPipes;


    public class ConsumeContextRetryContext :
        RetryContext<CommandContext>
    {
        readonly RetryCommandContext _context;
        readonly RetryContext<CommandContext> _retryContext;

        public ConsumeContextRetryContext(RetryContext<CommandContext> retryContext, RetryCommandContext context)
        {
            _retryContext = retryContext;
            _context = context;

            _context.RetryAttempt = retryContext.RetryCount;
        }

        public CommandContext Context => _context;

        public CancellationToken CancellationToken => _retryContext.CancellationToken;

        public Exception Exception => _retryContext.Exception;

        public int RetryCount => _retryContext.RetryCount;

        public int RetryAttempt => _retryContext.RetryAttempt;

        public Type ContextType => _retryContext.ContextType;

        public TimeSpan? Delay => _retryContext.Delay;

        public async Task PreRetry()
        {
            await _retryContext.PreRetry().ConfigureAwait(false);
        }

        public async Task RetryFaulted(Exception exception)
        {
            await _retryContext.RetryFaulted(exception).ConfigureAwait(false);
        }

        public bool CanRetry(Exception exception, out RetryContext<CommandContext> retryContext)
        {
            var canRetry = _retryContext.CanRetry(exception, out RetryContext<CommandContext> policyRetryContext);

            retryContext = new ConsumeContextRetryContext(policyRetryContext, canRetry ? _context.CreateNext() : _context);

            return canRetry;
        }
    }


    public class ConsumeContextRetryContext<TFilter, TContext> :
        RetryContext<TFilter>
        where TFilter : class, CommandContext
        where TContext : RetryCommandContext, TFilter
    {
        readonly TContext _context;
        readonly RetryContext<TFilter> _retryContext;

        public ConsumeContextRetryContext(RetryContext<TFilter> retryContext, TContext context)
        {
            _retryContext = retryContext;
            _context = context;

            _context.RetryAttempt = retryContext.RetryCount;
        }

        public TFilter Context => _context;

        public CancellationToken CancellationToken => _retryContext.CancellationToken;

        public Exception Exception => _retryContext.Exception;

        public int RetryCount => _retryContext.RetryCount;

        public int RetryAttempt => _retryContext.RetryAttempt;

        public Type ContextType => _retryContext.ContextType;

        public TimeSpan? Delay => _retryContext.Delay;

        public async Task PreRetry()
        {
            await _retryContext.PreRetry().ConfigureAwait(false);
        }

        public async Task RetryFaulted(Exception exception)
        {
            await _retryContext.RetryFaulted(exception).ConfigureAwait(false);
        }

        public bool CanRetry(Exception exception, out RetryContext<TFilter> retryContext)
        {
            var canRetry = _retryContext.CanRetry(exception, out RetryContext<TFilter> policyRetryContext);

            retryContext = new ConsumeContextRetryContext<TFilter, TContext>(policyRetryContext, canRetry ? _context.CreateNext<TContext>() : _context);

            return canRetry;
        }
    }
}