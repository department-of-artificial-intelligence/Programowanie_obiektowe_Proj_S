using Project.Model;

namespace Project.ConsoleApp
{
    internal class Program
    {
        private static Hotel _hotel = MiniMock<Hotel>();

        private static object MiniMock(Type type)
        {
            // if Int32
            if (type == typeof(int))
            {
                return Random.Shared.Next(1, 1000);
            }

            // if string
            if (type == typeof(string))
            {
                return string.Join("", Enumerable.Range(1, 10).Select(_ => (char)Random.Shared.Next(97, 123)));
            }

            // if DateTime
            if (type == typeof(DateTime))
            {
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dateTime.AddSeconds(Random.Shared.Next(1287305095 /* 2010 */, 1760697905 /* 2025 */));
            }

            // if Guid
            if (type == typeof(Guid))
            {
                return Guid.NewGuid();
            }

            // if IEnumerable
            if (type.Name == "IEnumerable`1" /* ugly but idc */)
            {
                var genericType = type.GenericTypeArguments[0];

                var instanceType = typeof(List<>).MakeGenericType(genericType);
                var list = Activator.CreateInstance(instanceType)!;
                var method = instanceType.GetMethod("Add")!;

                foreach (var _ in Enumerable.Range(1, 10))
                {
                    method.Invoke(list, [MiniMock(genericType)]);
                }

                return list;
            }

            // if object
            if (type.BaseType == typeof(object))
            {
                var instance = Activator.CreateInstance(type)!;

                foreach (var property in type.GetProperties())
                {
                    property.SetValue(instance, MiniMock(property.PropertyType));
                }

                return instance;
            }

            throw new NotImplementedException($"Type {type} is not implemented yet!");
        }

        private static T MiniMock<T>() where T : class => (T) MiniMock(typeof(T));

        private static void Main()
        {
            var colorToRestore = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(_hotel);

            foreach (var room in _hotel.Rooms) 
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"    {room}");

                foreach (var entry in room.History)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"        {entry}");
                }
            }

            Console.ForegroundColor = colorToRestore;
        }
    }
}
