// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MacrosList.cs" company="Oswald Maskens">
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

    using Nios2Compiler.Macros;

    public static class MacrosList
    {
        public static List<IMacro> All
        {
            get
            {
                var list = new List<IMacro>();

                list.AddRange(RegMacros.All);
                list.AddRange(ImmMacros.All);
                list.Add(new PopMacro());
                list.Add(new PushMacro());
                list.AddRange(SetMacros.All);
                list.AddRange(MemMacros.All);
                list.AddRange(BranchMacros.All);

                return list;
            }
        }
    }
}
