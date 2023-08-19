using C05.SelectMany.QueryData.Data;

namespace C05.SelectMany
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                // Query syntax

                // front end (angular, react)
                //  اسماء الطلاب اللي بيدرسوا فرونت اند

                //var frontendParticipants =
                //    from c in context.Courses
                //    where c.CourseName.Contains("frontend") // angular, react
                //    from s in c.Sections
                //    from p in s.Participants
                //    select new
                //    {
                //        ParticipantName = p.FullName
                //    };


                // method syntax

                var frontendParticipants =
                    context.Courses
                    .Where(x => x.CourseName.Contains("frontend")) //  angular, react
                    .SelectMany(x => x.Sections) // s1, s2, s...
                    .SelectMany(x => x.Participants)
                    .Select(p =>
                        new
                        {
                            ParticipantName = p.FullName
                        });



                foreach (var pName in frontendParticipants)
                    Console.WriteLine(pName);



                Console.ReadKey();

            }
        }
    }
}