// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PopMacro.cs" company="Oswald Maskens">
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

namespace Nios2Compiler.Test.Macros
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Nios2Compiler.Test.Helpers;

    [TestClass]
    public class PopMacro
    {
        private readonly IMacro macro = new Nios2Compiler.Macros.PopMacro();

        [TestMethod]
        public void MatchTest()
        {
            MacroHelper.ShouldMatch(this.macro, "pop t1");
        }

        [TestMethod]
        public void InvalidMatchTest()
        {
            MacroHelper.ShouldNotMatch(this.macro, "push t1");
        }
    }
}
