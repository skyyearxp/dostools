﻿namespace Vurdalakov
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    // Command format: [/option] [/option=value] [filename1 [filename2 ...]]

    public class CommandLineParser
    {
        private readonly Dictionary<String, String> _options = new Dictionary<String, String>(StringComparer.InvariantCultureIgnoreCase);

        public CommandLineParser(Char[] prefixes = null, Char[] valueSeparators = null, Boolean caseInsensitiveNames = true)
        {
            if (null == prefixes)
            {
                prefixes = new[] { '/', '-' };
            }

            if (null == valueSeparators)
            {
                valueSeparators = new[] { '=', ':' };
            }

            _options = new Dictionary<String, String>(caseInsensitiveNames ? StringComparer.InvariantCultureIgnoreCase : null);

            ParseArguments(prefixes, valueSeparators);
        }

        public String ExecutableFileName { get; private set; }

        public String ExecutablePath { get; private set; }

        public String[] FileNames { get; private set; }

        public Boolean IsOptionSet(String optionName)
        {
            return _options.ContainsKey(optionName);
        }

        public Boolean IsOptionSet(params String[] optionNames)
        {
            foreach (var optionName in optionNames)
            {
                if (IsOptionSet(optionName))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean OptionHasValue(String optionName)
        {
            return IsOptionSet(optionName) && String.IsNullOrEmpty(_options[optionName]);
        }

        public Boolean OptionHasValue(params String[] optionNames)
        {
            foreach (var optionName in optionNames)
            {
                if (OptionHasValue(optionName))
                {
                    return true;
                }
            }

            return false;
        }

        public String GetOptionString(String optionName, String defaultValue)
        {
            return OptionHasValue(optionName) ? _options[optionName] : defaultValue;
        }

        public String GetOptionString(params String[] optionNames)
        {
            foreach (var optionName in optionNames)
            {
                if (OptionHasValue(optionName))
                {
                    return GetOptionString(optionName, null);
                }
            }

            return null;
        }

        public Int32 GetOptionInt(String optionName, Int32 defaultValue)
        {
            try
            {
                return OptionHasValue(optionName) ? Convert.ToInt32(GetOptionString(optionName, null)) : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public Int32 GetOptionInt(params String[] optionNames)
        {
            foreach (var optionName in optionNames)
            {
                if (OptionHasValue(optionName))
                {
                    return GetOptionInt(optionName, 0);
                }
            }

            return 0;
        }

        private void ParseArguments(Char[] prefixes, Char[] valueSeparators)
        {
            var args = Environment.GetCommandLineArgs();

            ExecutableFileName = args[0];
            ExecutablePath = Path.GetDirectoryName(ExecutableFileName);

            var fileNames = new List<String>();

            for (int i = 1; i < args.Length; i++)
            {
                var s = args[i];

                if (String.IsNullOrEmpty(s))
                {
                    // just skip
                }
                else if (0 == s.IndexOfAny(prefixes)) // options
                {
                    if (s.IndexOfAny(valueSeparators) > 0) // option with value
                    {
                        var optionParts = s.Split(valueSeparators);
                        _options.Add(optionParts[0].TrimStart(prefixes).ToLower(), optionParts[1]);
                    }
                    else // option without value
                    {
                        _options.Add(s.TrimStart(prefixes).ToLower(), null);
                    }
                }
                else // file names
                {
                    fileNames.Add(s);
                }
            }

            FileNames = fileNames.ToArray();
        }
    }
}
