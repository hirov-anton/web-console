﻿// WebConsole (https://github.com/hirov-anton/web-console)
// See LICENSE file in the solution root for full license information
// Copyright (c) 2018 Anton Hirov

namespace WebConsole.Core
{
    public static class ApplicationConstants
    {
        // STORAGE KEYS
        public const string JobSetKey = "JobSetKey";

        // CONFIG
        public const string WcConfigFile = "WebConsole.config";
        public const int CleanUpInterval = 1800000;
        public const int DefaultJobsLimit = 12;

        // CODE
        public const string JobsLimitLiteral = "jobsLimit";
    }
}