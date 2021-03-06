﻿// WebConsole (https://github.com/hirov-anton/web-console)
// See LICENSE file in the solution root for full license information
// Copyright (c) 2018 Anton Hirov

using System;
using System.Collections.Generic;
using WebConsole.Core.Entities;
using static WebConsole.Core.ApplicationConstants;

namespace WebConsole.Core.Job
{
    public interface IJobBufferHandler
    {
        bool Run(int id, Action<JobContent, Dictionary<int, JobContent>> action);
        void Run(Action<Dictionary<int, JobContent>> action);
        void RunAll(Action<JobContent> action);
        void Clear();
    }

    public class JobBufferHandler : IJobBufferHandler
    {
        private readonly IApplicationStorage<Dictionary<int, JobContent>> buffer;

        public JobBufferHandler(IApplicationStorage<Dictionary<int, JobContent>> buffer)
        {
            this.buffer = buffer;
        }

        public bool Run(int id, Action<JobContent, Dictionary<int, JobContent>> action)
        {
            var isSuccessful = false;
            buffer.Invoke(JobSetKey, all =>
            {
                if (all.ContainsKey(id))
                {
                    action.Invoke(all[id], all);
                    isSuccessful = true;
                }
            });
            return isSuccessful;
        }

        public void Run(Action<Dictionary<int, JobContent>> action)
        {
            buffer.Invoke(JobSetKey, action.Invoke);
        }

        public void RunAll(Action<JobContent> action)
        {
            buffer.Invoke(JobSetKey, all =>
            {
                foreach (var item in all)
                {
                    action.Invoke(item.Value);
                }
            });
        }

        public void Clear()
        {
            buffer.Invoke(JobSetKey, all => all.Clear());
        }
    }
}