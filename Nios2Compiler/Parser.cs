// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parser.cs" company="Oswald Maskens">
//   Copyright 2013-2014 Oswald Maskens
//   
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Nios2Compiler
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal static class Parser
    {
        public static List<SourceLine> Parse(List<string> lines)
        {
            return lines.Select(ParseLine).ToList();
        }

        public static SourceLine ParseLine(string line, int lineNumber)
        {
            // Change tabs for 4 spaces
            line = line.Replace("\t", "    ");

            var alignmentBuilder = new StringBuilder();
            while (line.StartsWith(" "))
            {
                alignmentBuilder.Append(" ");
                line = line.Substring(1);
            }

            var alignment = alignmentBuilder.ToString();

            var parts = line.Split(';');
            var comment = string.Empty;

            if (parts.Length != 1)
            {
                var commentBuilder = new StringBuilder();
                for (var i = 1; i < parts.Length; i++)
                {
                    commentBuilder.Append(parts[i]);
                }

                comment = commentBuilder.ToString();
            }

            return new SourceLine
                       {
                           Comment = comment, 
                           Instruction = parts[0].Trim(), 
                           Number = lineNumber + 1, 
                           Alignment = alignment, 
                           Text = line
                       };
        }
    }
}
