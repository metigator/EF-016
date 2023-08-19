using C04.CrossJoin.QueryData.Data;

namespace C04.CrossJoin
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                // Query syntax

                // ربط كل مدرس مع كل قسم بغض النظر اذا كان يعطيه او لا
                //var sectionInstructorQuerySyntax =
                //        (from s in context.Sections // 200
                //         from i in context.Instructors // 100
                //         select new
                //         {
                //             s.SectionName,
                //             i.FullName
                //         }).ToList();


                //Console.WriteLine(sectionInstructorQuerySyntax.Count()); // 20000

                // method syntax
                var sectionInstructorMethodSyntax = context.Sections
                 .SelectMany(
                     s => context.Instructors,
                     (s, i) => new { s.SectionName, i.FullName }
                 ).ToList();

                Console.WriteLine(sectionInstructorMethodSyntax.Count());

                Console.ReadKey();
            }
        }
    }
}