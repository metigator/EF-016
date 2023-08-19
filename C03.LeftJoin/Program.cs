using C03.LeftJoin.QueryData.Data;

namespace C03.LeftJoin
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                // Query syntax

                //var officeOccupancyQuerySyntax =
                //    (from o in context.Offices
                //     join i in context.Instructors
                //     on o.Id equals i.OfficeId into officeVacancy
                //     from ov in officeVacancy.DefaultIfEmpty()
                //     select new
                //     {
                //         OfficeId = o.Id,
                //         Name = o.OfficeName,
                //         Location = o.OfficeLocation,
                //         Instructor = ov != null ? ov.FullName : "<<EMPTY>>"
                //     }).ToList();


                //foreach (var office in officeOccupancyQuerySyntax)
                //{
                //    Console.WriteLine($"{office.Name} -> {office.Instructor}");
                //}

                // method syntax
                var officeOccupancyMethodSyntax = context.Offices
                .GroupJoin(
                    context.Instructors,
                    o => o.Id,
                    i => i.OfficeId,
                    (office, instructor) => new { office, instructor }
                )
                .SelectMany(
                    ov => ov.instructor.DefaultIfEmpty(),
                    (ov, instructor) => new
                    {
                        OfficeId = ov.office.Id,
                        Name = ov.office.OfficeName,
                        Location = ov.office.OfficeLocation,
                        Instructor = instructor != null ? instructor.FullName : "<<EMPTY>>"
                    }
                ).ToList();

                foreach (var office in officeOccupancyMethodSyntax)
                {
                    Console.WriteLine($"{office.Name} -> {office.Instructor}");
                }
            }
            Console.ReadKey();
        }
    }
}