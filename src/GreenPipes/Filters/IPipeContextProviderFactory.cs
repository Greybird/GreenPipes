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
namespace GreenPipes.Filters
{
    /// <summary>
    /// A factory for obtaining pipe context providers for the dispatchFilter
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IPipeContextProviderFactory<in TContext, in TKey>
        where TContext : class, PipeContext
    {
        IPipeContextProvider<TContext, TResult> GetPipeContextFactory<TResult>(TKey key)
            where TResult : class, PipeContext;
    }


    public interface IPipeContextConverterFactory<in TInput>
        where TInput : class, PipeContext
    {
        IPipeContextConverter<TInput, TOutput> GetConverter<TOutput>()
            where TOutput : class, PipeContext;
    }


    /// <summary>
    /// Converts the input context to the output context
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IPipeContextConverter<in TInput, TOutput>
        where TInput : class, PipeContext
        where TOutput : class, PipeContext
    {
        bool TryConvert(TInput input, out TOutput output);
    }
}