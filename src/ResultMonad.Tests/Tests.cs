using System;
using Xunit;
using ResultMonad;

namespace ResultMonad.Tests {


    public class Tests {

        private Result<int> TryAddTwo(int a) {
            return Result<int>.FromData(a + 2);
        }

        private Result<int> TryDivideByZero(int a) {
            return Result<int>.FromError("Cannot divide by zero");
        }

        [Fact]
        public void Test1() {
            var rs =
                from i1 in TryAddTwo(100)
                from i2 in TryDivideByZero(i1)
                select i1 + i2;

            Assert.Equal(rs.Success, false);
            Assert.Equal(rs.Error, "Cannot divide by zero");
        }

        [Fact]
        public void T2() {
            var rs =
                from i1 in TryAddTwo(100)
                from i2 in TryAddTwo(i1)
                from i3 in TryAddTwo(i2)
                select i3;

            Assert.Equal(true, rs.Success);
            Assert.Equal(106, rs.Data);
        }
    }
}
