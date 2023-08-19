using C07.Pagination.QueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace C07.Pagination
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                var pageNumber = 1;
                var pageSize = 10;
                var totalSections = context.Sections.Count();
                var totalPages = (int)Math.Ceiling((double)totalSections / pageSize);

                var query = context.Sections.AsNoTracking()
                    .Include(x => x.Course)
                    .Include(x => x.Instructor)
                    .Include(x => x.Schedule)
                    .Select(x =>
                    new
                    {
                        Course = x.Course.CourseName,
                        Instructor = x.Instructor.FullName,
                        DateRange = x.DateRange.ToString(),
                        TimeSlot = x.TimeSlot.ToString(),
                        Days = string.Join(" ",   // "SAT SUN MON"
                               x.Schedule.SUN ? "SUN" : "",
                               x.Schedule.SAT ? "SAT" : "",
                               x.Schedule.MON ? "MON" : "",
                               x.Schedule.TUE ? "TUE" : "",
                               x.Schedule.WED ? "WED" : "",
                               x.Schedule.THU ? "THU" : "",
                               x.Schedule.FRI ? "FRI" : "")
                    });


                Console.WriteLine("|           Course                   |          Instructor            |       Date Range        |   Time Slot   |            Days                |");
                Console.WriteLine("|------------------------------------|--------------------------------|-------------------------|---------------|--------------------------------|");


                while (pageNumber <= totalPages)
                {
                    // actual paging
                    var pageResult = query.Skip(pageNumber - 1).Take(pageSize);


                    foreach (var section in pageResult)
                    {
                        Console.WriteLine($"| {section.Course,-34} | {section.Instructor,-30} | {section.DateRange.ToString(),-23} | {section.TimeSlot.ToString(),-12} | {section.Days,-30} |");
                    }

                    Console.WriteLine();

                    for (int p = 1; p <= totalPages; p++)
                    {
                        Console.ForegroundColor = p == pageNumber ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
                        Console.Write($"{p} "); // 1 2 3 4 5 .... 20
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    ++pageNumber;

                    Console.ReadKey();
                    Console.Clear();
                }
                Console.ReadKey();
            }
        }
    }
}