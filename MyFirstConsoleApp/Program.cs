using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;

namespace TodoConsoleApp
{
    public class TodoClient
    {
        private static readonly HttpClient client = new HttpClient();

        public TodoClient(string URL)
        {
            client.BaseAddress = new Uri(URL);
        }

        public Todo[]? GetAll()
        {
            var todos = client.GetFromJsonAsync<Todo[]>("/todos").Result;
            return todos;
        }

        public Todo? GetById(int id)
        {
            var todo = client.GetFromJsonAsync<Todo>($"/todos/id/{id}").Result;
            return todo;
        }


        public Todo[]? GetByDay(string day)
        {
            try
            {
                var todo = client.GetFromJsonAsync<Todo[]>($"/todos/day/{day}");
                return todo.Result;
            }
            catch (System.AggregateException ex)
            {
                return Array.Empty<Todo>();
            }
        }

        public Todo[]? GetByLocation(string location)
        {
            try
            {
                var todo = client.GetFromJsonAsync<Todo[]>($"/todos/location/{location}").Result;
                return todo;
            }
            catch (System.AggregateException ex)
            {
                return Array.Empty<Todo>();
            }
        }

        public int GetTodosCount()
        {
            var todo = client.GetFromJsonAsync<int>("/todos/count").Result;
            return todo;
        }

        public void Ende()
        {
            client.Dispose();
        }
    }

    public record Todo(int Id, string Title, string Room, string FinalDate);

    class Program
    {
        static void Main(string[] args)
        {
            var URL = "http://localhost:5000";

            TodoClient client = new TodoClient(URL);

            Console.WriteLine(client.GetAll());
            Console.WriteLine(client.GetById(2));
            Console.WriteLine(client.GetByDay("Friday"));
            Console.WriteLine(client.GetByLocation("Kitchen"));
            Console.WriteLine(client.GetTodosCount());
        }
    }
}
