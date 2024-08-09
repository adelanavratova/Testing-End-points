using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using TodoConsoleApp;

namespace MyFirstNUnitTest
{
    public class Tests
    {
        private TodoClient client = new TodoClient("http://localhost:5000/");

        private static readonly Todo[] SampleTodos = new Todo[]
        {
            new(1, "Walk the dog", "Outside", "Friday"),
            new(2, "Do the dishes", "Kitchen", "Monday"),
            new(3, "Do the laundry", "Bathroom", "Tuesday"),
            new(4, "Clean the bathroom", "Bathroom", "Monday"),
            new(5, "Clean the car", "Outside", "Friday"),
            new(6, "Cook lunch", "Kitchen", "Friday"),
            new(7, "Rest", "Bedroom", "Monday")
        };

        private static readonly string[] DaysOfWeek = new string[]
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };

        private static readonly string[] RoomsInHouse = new string[]
        {
            "Kitchen", "Bathroom", "Bedroom", "Living Room", "Outside"
        };

        [Test]
        public void Get_All_Todos()
        {
            var actual = client.GetAll();

            Assert.NotNull(actual);
            Assert.AreEqual(SampleTodos.Length, actual.Length);

            for (int i = 0; i < actual.Length; i++)
            {
                var todo = actual[i];
                var expected = SampleTodos[i];
                Assert.IsNotNull(todo);
                Assert.AreEqual(expected.Title, todo.Title);
                Assert.AreEqual(expected.Room, todo.Room);
                Assert.AreEqual(expected.FinalDate, todo.FinalDate);
            }
        }

        [Test]
        public void Get_By_Id_Todos()
        {
            for (int i = 0; i < SampleTodos.Length; i++)
            {
                var actual = client.GetById(i + 1);
                Assert.NotNull(actual);
                var expected = SampleTodos[i];
                Assert.AreEqual(expected.Title, actual.Title);
                Assert.AreEqual(expected.Room, actual.Room);
                Assert.AreEqual(expected.FinalDate, actual.FinalDate);
            }
        }

        [Test]
        public void Get_By_Day_Todos()
        {
            for(int i = 0;i < DaysOfWeek.Length; i++)
            {
                var day = DaysOfWeek[i];
                var expectedTodos = SampleTodos.Where(t => t.FinalDate.Equals(day, StringComparison.OrdinalIgnoreCase)).ToArray();
                var actual = client.GetByDay(day);
                Assert.NotNull(actual);
                Assert.AreEqual(expectedTodos.Length, actual.Length);

                for (int j = 0; j < expectedTodos.Length; j++)
                {
                    var expected = expectedTodos[j];
                    var todo = actual[j];
                    Assert.AreEqual(expected.Id, todo.Id);
                    Assert.AreEqual(expected.Title, todo.Title);
                    Assert.AreEqual(expected.Room, todo.Room);
                    Assert.AreEqual(expected.FinalDate, todo.FinalDate);
                }
            }
        }

        [Test]
        public void Get_By_Location_NonExistent()
        {
            var actual = client.GetByLocation("NonExistentLocation");
            Assert.NotNull(actual);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void Get_By_Day_NonExistent()
        {
            var actual = client.GetByDay("NonExistentDay");
            Assert.NotNull(actual);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void Get_By_Location_Todos()
        {
            for (int i = 0; i < RoomsInHouse.Length; i++)
            {
                var location = RoomsInHouse[i];
                var expectedTodos = SampleTodos.Where(t => t.Room.Equals(location, StringComparison.OrdinalIgnoreCase)).ToArray();
                var actual = client.GetByLocation(location);
                Assert.NotNull(actual);
                Assert.AreEqual(expectedTodos.Length, actual.Length);

                for (int j = 0; j < expectedTodos.Length; j++)
                {
                    var expected = expectedTodos[j];
                    var todo = actual[j];
                    Assert.AreEqual(expected.Id, todo.Id);
                    Assert.AreEqual(expected.Title, todo.Title);
                    Assert.AreEqual(expected.Room, todo.Room);
                    Assert.AreEqual(expected.FinalDate, todo.FinalDate);
                }

            }
        }

        [Test]
        public void Get_Count()
        {
            var count = client.GetTodosCount();
            Assert.AreEqual(SampleTodos.Length, count);
        }
    }
}