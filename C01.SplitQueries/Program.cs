using C01.SplitQuery.QueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace C01.SplitQueries
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                //// proper projection (select) reduce network traffic
                //// and reduce the effect on app performance
                //var coursesProjection = context.Courses.AsNoTracking()
                //    .Select(c =>
                //    new
                //    {
                //        CourseId = c.Id,
                //        CourseName = c.CourseName,
                //        Hours = c.HoursToComplete,
                //        Section = c.Sections.Select(s =>
                //        new
                //        {
                //            SectionId = s.Id,
                //            SectionName = s.SectionName,
                //            DateRate = s.DateRange.ToString(),
                //            TimeSlot = s.TimeSlot.ToString()
                //        }),
                //        Reviews = c.Reviews.Select(r =>
                //        new
                //        {
                //            FeedBack = r.Feedback,
                //            CreateAt = r.CreatedAt
                //        })
                //    }).ToList();
            }

            using (var context = new AppDbContext())
            {
                //var courses1 = context.Courses
                //    .Include(x => x.Sections)
                //    .Include(x => x.Reviews)
                //    .AsSplitQuery() // explicit
                //    .ToList();

                //var courses2 = context.Courses
                //  .Include(x => x.Sections)
                //  .Include(x => x.Reviews) // split by config
                //  .ToList();

                var courses3 = context.Courses
                .Include(x => x.Sections)
                .Include(x => x.Reviews) // split by config
                .AsSingleQuery()
                .ToList();
            }
            Console.ReadKey();
        }
    }
}