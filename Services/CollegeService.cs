namespace CollegeManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using CollegeManager.Models;


public class CollegeService
{
    private List<Course> courses = new();
    private List<Student> students = new();
    private List<Teacher> teachers = new();

    public IReadOnlyList<Course> Courses => courses.AsReadOnly();
    public IReadOnlyList<Student> Students => students.AsReadOnly();
    public IReadOnlyList<Teacher> Teachers => teachers.AsReadOnly();

    public bool RegisterCourse(string? code, string? name, string? description, out string message)
    {
        if (courses.Any(c => c.Code == code))
        {
            message = "Curso com esse código já existe!";
            return false;
        }

        var course = new Course { Code = code, Name = name, Description = description };
        courses.Add(course);

        message = "Curso cadastrado com sucesso!";
        return true;
    }

    public bool RegisterStudent(string? registration, string? name, out string message)
    {
        if (students.Any(s => s.Registration == registration))
        {
            message = "Aluno com essa matrícula já existe!";
            return false;
        }

        var student = new Student { Registration = registration, Name = name };
        students.Add(student);

        message = "Aluno cadastrado com sucesso!";
        return true;
    }

    public bool RegisterTeacher(string? name, string? title, string? department, out string message)
    {
        if (teachers.Any(t => t.Name == name && t.Department == department))
        {
            message = "Professor já cadastrado neste departamento!";
            return false;
        }

        teachers.Add(new Teacher { Name = name, Title = title, Department = department });
        message = "Professor cadastrado com sucesso!";
        return true;
    }

    public bool EnrollStudent(string registration, string courseCode, out string message)
    {
        var student = students.FirstOrDefault(s => s.Registration == registration);
        if (student == null)
        {
            message = "Aluno não encontrado!";
            return false;
        }

        var course = courses.FirstOrDefault(c => c.Code == courseCode);
        if (course == null)
        {
            message = "Curso não encontrado!";
            return false;
        }

        if (student.Enrollments.Any(e => e.Course?.Code == courseCode))
        {
            message = "Aluno já está matriculado nesse curso!";
            return false;
        }

        var enrollment = new Enrollment { Student = student, Course = course };
        student.Enrollments.Add(enrollment);
        course.Enrollments.Add(enrollment);

        message = "Aluno matriculado com sucesso!";
        return true;
    }
}