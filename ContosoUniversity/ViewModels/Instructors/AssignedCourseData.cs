namespace ContosoUniversity.ViewModels.Instructors
{
    public class AssignedCourseData
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public bool Assigned { get; set; }

        public string OptionText
        {
            get { return string.Format("[{0}] {1}", CourseId, Title); }
        }

        public string OptionCheckedAttr
        {
            get { return Assigned ? "checked=\"checked\"" : ""; }
        }
    }
}