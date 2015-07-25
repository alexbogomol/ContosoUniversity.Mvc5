namespace ContosoUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelsAfterViewModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CourseInstructor", newName: "InstructorCourse");
            RenameColumn(table: "dbo.InstructorCourse", name: "CourseId", newName: "Course_Id");
            RenameColumn(table: "dbo.InstructorCourse", name: "InstructorId", newName: "Instructor_Id");
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_InstructorId", newName: "IX_Instructor_Id");
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_CourseId", newName: "IX_Course_Id");
            DropPrimaryKey("dbo.InstructorCourse");
            AddPrimaryKey("dbo.InstructorCourse", new[] { "Instructor_Id", "Course_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.InstructorCourse");
            AddPrimaryKey("dbo.InstructorCourse", new[] { "CourseId", "InstructorId" });
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_Course_Id", newName: "IX_CourseId");
            RenameIndex(table: "dbo.InstructorCourse", name: "IX_Instructor_Id", newName: "IX_InstructorId");
            RenameColumn(table: "dbo.InstructorCourse", name: "Instructor_Id", newName: "InstructorId");
            RenameColumn(table: "dbo.InstructorCourse", name: "Course_Id", newName: "CourseId");
            RenameTable(name: "dbo.InstructorCourse", newName: "CourseInstructor");
        }
    }
}
