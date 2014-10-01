// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandleTests.cs" company="Oswald Maskens">
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

namespace Nios2Compiler.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HandleTests
    {
        /*
         * ---------- Push & Pop Tests ------------ 
         */
        [TestMethod]
        public void HandlePushTest()
        {
            this.HandleOne("push ra", "addi sp, sp, -4", "stw ra, 0(sp)");
        }

        [TestMethod]
        public void HandlePopTest()
        {
            this.HandleOne("pop ra", "ldw ra, 0(sp)", "addi sp, sp, 4");
        }

        /*
         * ---------- Set Tests ------------ 
         */
        [TestMethod]
        public void HandleSetTest1()
        {
            this.HandleOne("t0 = 7", "addi t0, zero, 7");
        }

        [TestMethod]
        public void HandleSetTest2()
        {
            this.HandleOne("t4 = 5", "addi t4, zero, 5");
        }

        [TestMethod]
        public void HandleSetTest3()
        {
            this.HandleOne("t0 = t4", "add t0, t4, zero");
        }

        [TestMethod]
        public void HandleSetTest4()
        {
            this.HandleOne("t0 = a3", "add t0, a3, zero");
        }

        /*
         * ---------- Reg Tests ------------ 
         */
        [TestMethod]
        public void HandleRegTest1()
        {
            this.HandleOne("t0 = t0 << t1", "sll t0, t0, t1");
        }

        [TestMethod]
        public void HandleRegTest2()
        {
            this.HandleOne("t0 = t0 or t1", "or t0, t0, t1");
        }

        [TestMethod]
        public void HandleRegTest3()
        {
            this.HandleOne("t0 = t5 and t1", "and t0, t5, t1");
        }

        /*
         * ---------- Imm Tests ------------ 
         */
        [TestMethod]
        public void HandleImmTest1()
        {
            this.HandleOne("t0 = t0 << 24", "slli t0, t0, 24");
        }

        [TestMethod]
        public void HandleImmTest2()
        {
            this.HandleOne("t0 = t5 or 0XF6", "ori t0, t5, 0XF6");
        }

        [TestMethod]
        public void HandleImmTest3()
        {
            this.HandleOne("t0 = t0 + 7", "addi t0, t0, 7");
        }

        [TestMethod]
        public void HandleImmTest4()
        {
            this.HandleOne("t0 = t0 + -8", "addi t0, t0, -8");
        }

        /*
         * ---------- Mem Tests ------------ 
         */
        [TestMethod]
        public void HandleMemTest1()
        {
            this.HandleOne("t1 = M[PADDLE]", "ldw t1, PADDLE(zero)");
        }

        [TestMethod]
        public void HandleMemTest2()
        {
            this.HandleOne("t1 = M[LED]", "ldw t1, LED(zero)");
        }

        [TestMethod]
        public void HandleMemTest3()
        {
            this.HandleOne("t1 = M[LED+8]", "ldw t1, LED+8(zero)");
        }

        [TestMethod]
        public void HandleMemTest4()
        {
            this.HandleOne("M[LED+8] = 0", "stw zero, LED+8(zero)");
        }

        [TestMethod]
        public void HandleMemTest5()
        {
            this.HandleOne("M[LED+8] = 7", "addi t7, zero, 7", "stw t7, LED+8(zero)");
        }

        /*
         * ---------- Branch Tests ------------ 
         */
        [TestMethod]
        public void HandleBranchTest1()
        {
            this.HandleOne("if t0 == 0 goto alpha", "beq t0, zero, alpha");
        }

        [TestMethod]
        public void HandleBranchTest2()
        {
            this.HandleOne("if t0 >= a3 goto beta", "bge t0, a3, beta");
        }

        [TestMethod]
        public void HandleBranchTest3()
        {
            this.HandleOne("if t0 != 8 goto gamma", "addi t7, zero, 8", "bne t0, t7, gamma");
        }

        private void HandleOne(string instr, params string[] expected)
        {
            var macro = MacrosList.All.Find(m => m.Match(new SourceLine { Instruction = instr }));
            if (macro != null)
            {
                var output = macro.Handle(new SourceLine { Instruction = instr });

                Assert.AreEqual(expected.Length, output.Count);
                for (var i = 0; i < expected.Length; i++)
                {
                    Assert.AreEqual(expected[i], output[i]);
                }
            }
            else
            {
                throw new AssertFailedException("No match");
            }
        }
    }
}
