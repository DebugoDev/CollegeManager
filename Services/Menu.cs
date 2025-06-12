namespace CollegeManager.Services;
using System;

class Menu
{
    static CollegeService service = new();

    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            DrawHeader();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Cadastrar Curso");
            Console.WriteLine("2. Cadastrar Aluno");
            Console.WriteLine("3. Cadastrar Professor");
            Console.WriteLine("4. Matricular Aluno em Curso");
            Console.WriteLine("5. Listar Cursos, Alunos Matriculados e Professores");
            Console.WriteLine("0. Sair");
            Console.ResetColor();
            Console.Write("\nEscolha uma opção: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterCourse();
                    break;
                case "2":
                    RegisterStudent();
                    break;
                case "3":
                    RegisterTeacher();
                    break;
                case "4":
                    EnrollStudentInCourse();
                    break;
                case "5":
                    ListCoursesAndEnrollments();
                    break;
                case "0":
                    WriteColoredLine("\nSaindo... Obrigado por usar o College Manager!", ConsoleColor.Cyan);
                    System.Threading.Thread.Sleep(1500);
                    return;
                default:
                    WriteColoredLine("\nOpção inválida! Pressione qualquer tecla para continuar...", ConsoleColor.Red);
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void DrawHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("===========================================");
        Console.WriteLine("          COLLEGE MANAGER SYSTEM            ");
        Console.WriteLine("===========================================\n");
        Console.ResetColor();
    }

    static void WriteColoredLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    static void RegisterCourse()
    {
        Console.Clear();
        DrawHeader();
        WriteColoredLine("=== Cadastrar Curso ===\n", ConsoleColor.Green);

        Console.Write("Código do Curso: ");
        var code = Console.ReadLine();

        Console.Write("Nome do Curso: ");
        var name = Console.ReadLine();

        Console.Write("Descrição do Curso: ");
        var description = Console.ReadLine();

        if (service.RegisterCourse(code, name, description, out string message))
        {
            WriteColoredLine($"\n{message}", ConsoleColor.Green);
        }
        else
        {
            WriteColoredLine($"\nErro: {message}", ConsoleColor.Red);
        }
        Pause();
    }

    static void RegisterStudent()
    {
        Console.Clear();
        DrawHeader();
        WriteColoredLine("=== Cadastrar Aluno ===\n", ConsoleColor.Green);

        Console.Write("Matrícula do Aluno: ");
        var registration = Console.ReadLine();

        Console.Write("Nome do Aluno: ");
        var name = Console.ReadLine();

        if (service.RegisterStudent(registration, name, out string message))
        {
            WriteColoredLine($"\n{message}", ConsoleColor.Green);
        }
        else
        {
            WriteColoredLine($"\nErro: {message}", ConsoleColor.Red);
        }
        Pause();
    }

    static void RegisterTeacher()
    {
        Console.Clear();
        DrawHeader();
        WriteColoredLine("=== Cadastrar Professor ===\n", ConsoleColor.Green);

        Console.Write("Nome do Professor: ");
        var name = Console.ReadLine();

        Console.Write("Titulo do Professor: ");
        var title = Console.ReadLine();

        Console.Write("Departamento do Professor: ");
        var department = Console.ReadLine();

        if (service.RegisterTeacher(name, title, department, out string message))
        {
            WriteColoredLine($"\n{message}", ConsoleColor.Green);
        }
        else
        {
            WriteColoredLine($"\nErro: {message}", ConsoleColor.Red);
        }
        Pause();
    }

    static void EnrollStudentInCourse()
    {
        Console.Clear();
        DrawHeader();
        WriteColoredLine("=== Matricular Aluno em Curso ===\n", ConsoleColor.Green);

        if (service.Students.Count == 0)
        {
            WriteColoredLine("Nenhum aluno cadastrado!", ConsoleColor.Red);
            Pause();
            return;
        }
        if (service.Courses.Count == 0)
        {
            WriteColoredLine("Nenhum curso cadastrado!", ConsoleColor.Red);
            Pause();
            return;
        }

        WriteColoredLine("Alunos:", ConsoleColor.Yellow);
        foreach (var student in service.Students)
        {
            Console.WriteLine($"  - {student}");
        }
        Console.Write("\nDigite a matrícula do aluno: ");
        string reg = Console.ReadLine();

        WriteColoredLine("\nCursos:", ConsoleColor.Yellow);
        foreach (var course in service.Courses)
        {
            Console.WriteLine($"  - {course}");
        }
        Console.Write("Digite o código do curso: ");
        string code = Console.ReadLine();

        if (service.EnrollStudent(reg, code, out string message))
        {
            WriteColoredLine($"\n{message}", ConsoleColor.Green);
        }
        else
        {
            WriteColoredLine($"\nErro: {message}", ConsoleColor.Red);
        }
        Pause();
    }

    static void ListCoursesAndEnrollments()
    {
        Console.Clear();
        DrawHeader();
        WriteColoredLine("=== Cursos e Alunos Matriculados ===\n", ConsoleColor.Green);

        if (service.Courses.Count == 0)
        {
            WriteColoredLine("Nenhum curso cadastrado!", ConsoleColor.Red);
            Pause();
            return;
        }

        foreach (var course in service.Courses)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nCurso: {course.Name} ({course.Code})");
            Console.ResetColor();

            if (course.Enrollments.Count == 0)
            {
                WriteColoredLine("  Nenhum aluno matriculado.", ConsoleColor.DarkGray);
            }
            else
            {
                foreach (var enrollment in course.Enrollments)
                {
                    Console.WriteLine($"  - {enrollment.Student.Name} ({enrollment.Student.Registration})");
                }
            }
        }

        Console.WriteLine("\n=== Professores Cadastrados ===\n");

        if (service.Teachers.Count == 0)
        {
            WriteColoredLine("Nenhum professor cadastrado!", ConsoleColor.Red);
        }
        else
        {
            foreach (var teacher in service.Teachers)
            {
                Console.WriteLine($"- {teacher.Name} ({teacher.Title}, {teacher.Department})");
            }
        }

        Pause();
    }

    static void Pause()
    {
        WriteColoredLine("\nPressione qualquer tecla para continuar...", ConsoleColor.Cyan);
        Console.ReadKey();
    }
}