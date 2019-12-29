using Microsoft.VisualStudio.TestTools.UnitTesting;
using sExpressionCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sExpressionCalc.Tests
{
    [TestClass()]
    public class SExpressionTests
    {
        [TestMethod()]
        public void calcTest()
        {
           
         
            for (int i = 0; i < 1000; i++)
            {
                Assert.AreEqual(SExpression.calc(i.ToString()), i);
                Assert.AreEqual(SExpression.calc($"(Add {i} {i})"), (i * 2));
                Assert.AreEqual(SExpression.calc($"(Multiply {i} {i})"), (i * i));
            }

            Assert.AreEqual(SExpression.calc("(add (add 1 2) 5)"), 8);
            Assert.AreEqual(SExpression.calc("(add (multiply 2 3) 7)"), 13);

            Assert.AreEqual(SExpression.calc("(add 3 (add 1 2))"), 6);
            Assert.AreEqual(SExpression.calc("(add 3 (multiply 1 2))"), 5);

            Assert.AreEqual(SExpression.calc("(add (add 1 2) (add 3 4))"), 10);
            Assert.AreEqual(SExpression.calc("(add (add 1 2) (multiply 1 2))"), 5);
            Assert.AreEqual(SExpression.calc("(add (multiply 5 3) (add 3 4))"), 22);
            Assert.AreEqual(SExpression.calc("(add (multiply 5 3) (multiply 3 4))"), 27);

            Assert.AreEqual(SExpression.calc("(multiply (add 1 2) 5)"), 15);
            Assert.AreEqual(SExpression.calc("(multiply (multiply 2 3) 7)"), 42);

            Assert.AreEqual(SExpression.calc("(multiply 3 (add 1 2))"), 9);
            Assert.AreEqual(SExpression.calc("(multiply 3 (multiply 1 2))"), 6);

            Assert.AreEqual(SExpression.calc("(multiply (add 1 2) (add 3 4))"), 21);
            Assert.AreEqual(SExpression.calc("(multiply (add 1 2) (multiply 1 2))"), 6);
            Assert.AreEqual(SExpression.calc("(multiply (multiply 5 3) (add 3 4))"), 105);
            Assert.AreEqual(SExpression.calc("(multiply (multiply 5 3) (multiply 3 4))"), 180);

            Assert.AreEqual(SExpression.calc("(multiply (add (multiply 1 2) 3) (add 1 2))"), 15);
            Assert.AreEqual(SExpression.calc("(multiply (add (multiply 1 2) (add 3 4)) (multiply (add 5 6) (multiply 7 8)))"), 5544);


        }
    }
}