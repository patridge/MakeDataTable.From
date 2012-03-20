namespace MakeDataTable.From.Tests {
    using NUnit.Framework;
    using System.Data;
    using System;

    //class Program {
    //    enum TestEnum {
    //        y = 2,
    //        z = 3
    //    }
    //    static void Main(string[] args) {
    //        // enum value
    //        DataTable test3 = MakeDataTable.From(new { x = (TestEnum?)null, y = TestEnum.y, z = TestEnum.z });
    //        Console.ReadKey();
    //    }
    //}
    public class MakeDataTableTests {
        private enum TestEnumInt {
            x = 1,
            y = 2,
            z = 3
        }
        private enum TestEnumLong : long {
            x = 1,
            y = 2,
            z = 3
        }
        [TestFixture]
        public class From {
            [Test]
            public void AnonymousObjectWithNullNullable_ReturnsDataTableWithDBNullValue() {
                DataTable test = MakeDataTable.From(new { NullNullable = (int?)null });
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Rows.Count);
                Assert.AreEqual(typeof(int), test.Columns["NullNullable"].DataType);
                Assert.AreEqual(DBNull.Value, test.Rows[0]["NullNullable"]);
            }
            [Test]
            public void AnonymousObjectWithNullString_ReturnsDataTableWithDBNullValue() {
                DataTable test = MakeDataTable.From(new { NullString = (string)null });
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Rows.Count);
                Assert.AreEqual(typeof(string), test.Columns["NullString"].DataType);
                Assert.AreEqual(DBNull.Value, test.Rows[0]["NullString"]);
            }
            [Test]
            public void AnonymousObjectWithEnum_ReturnsDataTableWithEnumUnderlyingValue() {
                DataTable test = MakeDataTable.From(new { EnumValue = TestEnumInt.x });
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Rows.Count);
                Assert.AreEqual(typeof(int), test.Columns["EnumValue"].DataType);
                Assert.AreEqual(1, test.Rows[0]["EnumValue"]);
            }
            [Test]
            public void AnonymousObjectWithNullNullableEnum_ReturnsDataTableWithDBNullValue() {
                DataTable test = MakeDataTable.From(new { NullEnum = (TestEnumInt?)null });
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Rows.Count);
                Assert.AreEqual(typeof(int), test.Columns["NullEnum"].DataType);
                Assert.AreEqual(DBNull.Value, test.Rows[0]["NullEnum"]);
            }
            [Test]
            public void AnonymousObjectWithLongEnum_ReturnsDataTableWithEnumUnderlyingValue() {
                DataTable test = MakeDataTable.From(new { EnumValue = TestEnumLong.x });
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Rows.Count);
                Assert.AreEqual(typeof(long), test.Columns["EnumValue"].DataType);
                Assert.AreEqual(1, test.Rows[0]["EnumValue"]);
            }
            [Test]
            public void AnonymousObjectWithNullNullableLongEnum_ReturnsDataTableWithDBNullValue() {
                DataTable test = MakeDataTable.From(new { NullEnum = (TestEnumLong?)null });
                Assert.IsNotNull(test);
                Assert.AreEqual(1, test.Rows.Count);
                Assert.AreEqual(typeof(long), test.Columns["NullEnum"].DataType);
                Assert.AreEqual(DBNull.Value, test.Rows[0]["NullEnum"]);
            }
            [Test]
            public void ArrayOfAnonymousObjects_ReturnsDataTableWithRowForEach() {
                DataTable test = MakeDataTable.From(new object[] {
                    new { x = 1, y = "test1" },
                    new { x = 2, y = "test2" }
                });
                Assert.IsNotNull(test);
                Assert.AreEqual(2, test.Rows.Count);
                Assert.AreEqual(typeof(int), test.Columns["x"].DataType);
                Assert.AreEqual(typeof(string), test.Columns["y"].DataType);
                Assert.AreEqual(1, test.Rows[0]["x"]);
                Assert.AreEqual("test1", test.Rows[0]["y"]);
                Assert.AreEqual(2, test.Rows[1]["x"]);
                Assert.AreEqual("test2", test.Rows[1]["y"]);
            }
        }
    }
}
