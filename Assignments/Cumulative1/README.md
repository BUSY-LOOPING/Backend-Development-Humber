# Cumulative1 Project

This project was developed as part of the C# Cumulative Project at Humber College. It is designed to manage and display information about teachers, students, and courses within an educational institution.

## Features

- **Teacher Management:** View detailed information about instructors, including employee number, salary, and hire date.
- **Student Management:** Access comprehensive details about students enrolled in the institution.
- **Course Management:** Retrieve and display information about courses offered, including associated teachers and enrolled students.

## API Endpoints

The project exposes several API endpoints to interact with the data:

### Teachers
- `GET /api/Teacher/ListAllTeacherInfo` - Retrieves a list of all teachers.
- `GET /api/Teacher/ListTeacherInfo?TeacherId={id}` - Retrieves information about a specific teacher by ID.
- `GET /api/Teacher/ListAllTeacherInfoBtwDates?FilterDateStart={startDate}&FilterDateEnd={endDate}` - Retrieves teachers hired between specified dates.

### Students
- `GET /api/Student/ListAllStudentInfo` - Retrieves a list of all students.
- `GET /api/Student/ListStudentInfo?StudentId={id}` - Retrieves information about a specific student by ID.

### Courses
- `GET /api/Course/ListAllCourseInfo` - Retrieves a list of all courses.
- `GET /api/Course/ListCourseInfo?CourseId={id}` - Retrieves information about a specific course by ID.
- `GET /api/Course/ListCoursesByTeacherId?TeacherId={id}` - Retrieves courses taught by a specific teacher.

## Web Interface

The application provides a user-friendly web interface to interact with the data:

### Teachers
- **List:** Displays all teachers, with optional filtering by hire date range.
- **Show:** View detailed information about a specific teacher, including courses they teach.

### Students
- **List:** Displays all students.
- **Show:** View detailed information about a specific student.

### Courses
- **List:** Displays all courses.
- **Show:** View detailed information about a specific course.

## Security Considerations

The application employs parameterized queries to prevent SQL injection attacks. For example, in the `TeacherAPIController`, the `ListTeacherInfo` method uses parameterized queries to safely retrieve data:

```csharp
Command.CommandText = "SELECT * FROM teachers WHERE teacherid = @TeacherId";
Command.Parameters.AddWithValue("@TeacherId", TeacherId);
```


## Getting Started

1. Clone the Repository:

```bash
git clone https://github.com/BUSY-LOOPING/Backend-Development-Humber.git
```

2. Navigate to the Project Directory:

```bash
cd Backend-Development-Humber/Assignments/Cumulative1
```

3. Set Up the Database:
- Ensure MySQL is installed and running.
- Create a database named ```schooldb```.
- Execute the provided SQL script (```schooldb.sql```) to create necessary tables and seed data.

4. Configure the model:
Write the appropriate user, password and port in ```SchoolDBContext.cs```

5. Run the Application:
- Build and run the project using Visual Studio or the .NET CLI.
- Access the web interface at https://localhost:{port}/


